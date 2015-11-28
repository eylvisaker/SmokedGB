using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CpuEmulatorGenerator
{
	using Description;
	using CodeGenerator;

	class Generator
	{
		Cpu cpu;
		CSharpGenerator gen; 
		public Generator(Cpu cpu)
		{
			this.cpu = cpu;
			gen = new CSharpGenerator(cpu.Output.File);
		}

		List<EnumGen> opCodeEnums = new List<EnumGen>();
		List<Table> tables = new List<Table>();

		string RegisterClassName { get { return "Registers"; } }
		string RegisterVariableName { get { return "registers"; } }
		string ProgramCounter
		{
			get
			{
				return RegisterVariableName + "." + cpu.Info.ProgramCounter.Name;
			}
		}
		string Memory { get { return "Memory"; } }
		int[] paramMemoryIndex = new int[40];

		public void Run()
		{
			GenerateOpcodeEnumAndTable();

			using (gen)
			{
				gen.AddNamespace("System");
				gen.AddNamespace("System.Collections.Generic");
				gen.AddNamespace("System.Runtime.InteropServices");

				gen.BeginNamespace(cpu.Output.Namespace);
				gen.BeginPartialClass(cpu.Name);
				gen.VariableDecl(RegisterClassName, RegisterVariableName, AccessModifier.Public);

				foreach (var enm in opCodeEnums)
					gen.WriteEnum(enm);

				foreach (Table tbl in tables)
					gen.WriteTable(tbl);

				WriteFlagDeclarations();

				gen.VariableDecl("int", "cpuCycles");

				WriteExecMethod("", opCodeEnums[0]);

				// hack: enums are in same order as code pages.
				int index = 1;
				foreach (string codePage in cpu.Info.CodePages)
				{
					WriteExecMethod(codePage, opCodeEnums[index]);
					index++;
				}

				WriteOperations();

				gen.EndClass();

				WriteRegistersClass();
			}
		}

		private void WriteFlagDeclarations()
		{
			string fmt = "X";

			switch (cpu.Info.BaseSize)
			{
				case "byte": fmt += "2"; break;
				case "ushort":
				case "short": fmt += "4"; break;
				case "uint":
				case "int": fmt += "8"; break;
			}

			gen.WriteLine();

			foreach (var flag in cpu.Flags)
			{
				int value = 1 << flag.Bit;

				gen.WriteConstant("uint", FlagSetMask(flag), 
					gen.HexString(value.ToString("X4")));
			}

			foreach (var flag in cpu.Flags)
			{
				int value = 1 << flag.Bit;
				value = ~value;

				gen.WriteConstant("uint", FlagResetMask(flag),
					gen.HexString(value.ToString("X4")));
			}
		}

		private void WriteOperations()
		{
			foreach (var operation in cpu.Operations)
			{
				if (operation.Inline)
					continue;

				WriteOperationMethod(operation);
			}
		}
		List<PassedParameter> localVariables = new List<PassedParameter>();

		private void WriteOperationMethod(Operation operation)
		{
			List<MethodParameter> mp = new List<MethodParameter>();
			List<PassedParameter> passed = new List<PassedParameter>();
			localVariables.Clear();

			for (int i = 0; i < operation.Parameters.Count; i++)
			{
				Parameter param_def = operation.Parameters[i];

				MethodParameter p = new MethodParameter();

				p.Name = param_def.Name.Replace("%", "p_");
				p.Type = param_def.Type ?? cpu.Info.BaseSize;
				p.IsRef = param_def.IsRef;

				mp.Add(p);

				PassedParameter pp = new PassedParameter();

				pp.Name = p.Name;
				pp.IsRef = param_def.IsRef;
				pp.Substitution = param_def.Name;

				passed.Add(pp);

				localVariables.Add(pp);
			}

			gen.BeginMethod("void", 
				GetOperationMethodName(operation.Name), 
				AccessModifier.Private, 
				mp.ToArray());



			WriteInlineMethodBody(operation, passed);

			gen.EndMethod();
		}

		private void WriteRegistersClass()
		{
			gen.WriteAttribute("StructLayout", "LayoutKind.Explicit");
			gen.BeginPartialClass("Registers");

			int loc = 0;

			// position composite registers first.
			foreach (var reg in from x in cpu.Registers
								where x.Composite != null
								select x)
			{
				reg.Location = loc;

				Register[] regs = reg.Composite.Split(',').Select(
										x => cpu.FindRegisterOrThrow(x)).ToArray();

				int thissize = 0;
				for (int i = 0; i < regs.Length; i++)
				{
					regs[i].Location = loc + thissize;

					thissize += regs[i].Size;	
				}

				if (thissize > reg.Size)
					throw new InvalidOperationException(
						"Register " + reg.Name + "is smaller than its pieces!");
				loc += reg.Size;
			}

			foreach (var reg in from x in cpu.Registers
								where x.Location < 0
								select x)
			{
				reg.Location = loc;

				loc += reg.Size;
			}

			foreach (var reg in cpu.Registers)
			{
				gen.WriteAttribute("FieldOffset", reg.Location.ToString());
				gen.VariableDecl(reg.Type, reg.Name, AccessModifier.Public);
			}

			gen.EndClass();
		}

		private void WriteExecMethod(string codePageName, EnumGen codes)
		{
			string instructionStorage = "instruction";

			gen.BeginMethod("void", "ExecOpCode" + codePageName.ToUpperInvariant(), AccessModifier.Public);

			gen.VariableDecl(codes.Name, "instruction");

			gen.WriteLine();
			gen.SetVariable(instructionStorage, codes.Name, Mem(ProgramCounter));
			gen.IncrementVariable(ProgramCounter);
			gen.IncrementVariable("cpuCycles",
				gen.Array("Cycles" + codePageName.ToUpperInvariant(), instructionStorage, true));

			gen.WriteLine();

			gen.BeginSwitch(instructionStorage);

			foreach (var code in codes.Values)
			{
				gen.BeginCase(codes.Name + "." + code.Name);

				if (IsCodePage(code.Value))
				{
					gen.MethodCall("ExecOpCode" + code.Value.ToUpperInvariant());
					gen.EndCase();
					continue;
				}

				OpCode opcode;
				Op op;
				FindOpCode(codePageName, code.Value, out opcode, out op);

				Operation operation = cpu.FindOperation(op.Operation);
				bool inIfStatement = false;
				operationSetsPC = false;

				int incrementPC = CalcIncrementPC(op);
				
				localVariables.Clear();
				inIfStatement = CheckCreateIfStatement(op);

				if (operation.Inline == false)
				{
					WriteMethodCall(op, operation);
				}
				else
				{
					WriteInlineMethodBody(operation, op.Parameters);
				}


				if (incrementPC != 0 && operationSetsPC == false)
				{
					string name = cpu.Info.ProgramCounter.Name;
					string varName = GetVariableName(name);

					gen.AddSetVariable(varName, incrementPC.ToString());
				}
				else if (incrementPC != 0)
				{
					if (inIfStatement)
					{
						if (operationSetsPC)
						{
							gen.ElseStatement();
						}

						gen.IncrementVariable(
							GetVariableName(cpu.Info.ProgramCounter.Name), incrementPC);

						gen.EndIfStatement();
					}
					else
					{
						if (operationSetsPC == false)
						{
							gen.IncrementVariable(
								GetVariableName(cpu.Info.ProgramCounter.Name), incrementPC);
						}
					}
				}
				else if (inIfStatement)
				{
					gen.EndIfStatement();
				}

				gen.EndCase();
				gen.WriteLine();
			}

			gen.EndSwitch();

			gen.EndMethod();

		}

		private int CalcIncrementPC(Op op)
		{
			return op.Parameters.ToCharArray().Count(x => x == '#');
		}

		private bool IsCodePage(string p)
		{
			foreach (string codepage in cpu.Info.CodePages)
			{
				if (codepage == p)
					return true;
			}

			return false;
		}

		private bool CheckCreateIfStatement(Op op)
		{
			if (op.IfFlagSet != null)
			{
				Flag f = FindFlag(op.IfFlagSet);

				gen.BeginIfStatementBitSet(
					GetVariableName(f.Register.Name),
					f.Bit);

				return true;
			}
			else if (op.IfFlagReset != null)
			{
				Flag f = FindFlag(op.IfFlagReset);

				gen.BeginIfStatementBitReset(
					GetVariableName(f.Register.Name),
					f.Bit);

				return true;
			}

			return false;
		}


		bool operationSetsPC;
		List<TempVariable> tempVariables = new List<TempVariable>();
			
		private void WriteMethodCall(Op op, Operation operation)
		{
			tempVariables.Clear();
			string methodName = GetOperationMethodName(op.Operation);
			var parameters =
				GetParameters(operation, op.Parameters, tempVariables);

			List<MethodParameter> mp = new List<MethodParameter>();

			foreach (var param in parameters)
			{
				MethodParameter p = new MethodParameter();

				p.Name = ParseCode(param.Name, parameters);
				p.IsRef = param.IsRef;

				mp.Add(p);
			}

			WriteTempVariables();
			gen.MethodCall(methodName, mp.ToArray());
		}

		private string GetOperationMethodName(string p)
		{
			return "Op_" + p;
		}

		private void WriteTempVariables()
		{
			foreach (TempVariable v in tempVariables)
			{
				gen.VariableDecl(v.Type, v.Name, v.Initialization);

				localVariables.Add(new PassedParameter { Name = v.Name, Substitution = v.Name });
			}

			if (tempVariables.Count > 0)
				gen.WriteLine();

			
			tempVariables.Clear();

		}

		private void WriteInlineMethodBody(Operation operation, string definedParameters)
		{
			var passedParams = GetParameters(operation, definedParameters, tempVariables);
			WriteInlineMethodBody(operation, passedParams);
		}
		private void WriteInlineMethodBody(Operation operation, List<PassedParameter> passedParams)
		{
			for (int i = 0; i < operation.Variables.Count; i++)
			{
				Variable vdef = operation.Variables[i];

				gen.VariableDecl(
					vdef.Type, vdef.Name,
					ParseCode(vdef.InitialValue, passedParams));

				localVariables.Add(
					new PassedParameter
					{
						Name = vdef.Name,
					});
			}

			if (operation.Variables.Count > 0)
				gen.WriteLine();

			WriteTempVariables();

			for (int j = 0; j < operation.Code.Count; j++)
			{
				Code code = operation.Code[j];
				tempVariables.Clear();
				string expr;

				switch (code.Type)
				{
					case CodeType.Literal:
						expr = ParseCode(code.Expression, passedParams);
						WriteTempVariables();
						gen.WriteStatementLiteral(expr);
						break;

					case CodeType.WriteTo:
						string name = ParseCode(code.Dest, passedParams);
						string type = GetType(code.Dest, passedParams);
						expr = ParseCode(code.Expression, passedParams);

						if (code.Dest == cpu.Info.ProgramCounter.Name)
						{
							operationSetsPC = true;
						}

						WriteTempVariables();
						gen.SetVariable(name, type, expr);

						break;

					case CodeType.Flag:
						WriteFlag(code.Dest, code.Expression, passedParams);
						break;

					case CodeType.SetFlag:
						WriteSetFlag(code.Dest, code.Expression, passedParams);
						break;

					case CodeType.ResetFlag:
						WriteResetFlag(code.Dest, code.Expression, passedParams);
						break;

					case CodeType.FlipFlag:
						WriteFlipFlag(code.Dest, code.Expression, passedParams);
						break;

					case CodeType.If:
					case CodeType.ElseIf:
					case CodeType.EndIf:
						WriteIf(code.Type, code.Expression, passedParams);
						break;

					default:
						throw new NotSupportedException();
				}
			}

		}


		private void WriteIf(CodeType codeType, string expr, List<PassedParameter> passedParams)
		{
			expr = ParseCode(expr, passedParams);

			if (codeType == CodeType.If)
			{
				gen.BeginIfStatement(expr, ConditionType(expr));
			}
			else if (codeType == CodeType.ElseIf && string.IsNullOrEmpty(expr ))
			{
				gen.ElseStatement();
			}
			else if (codeType == CodeType.ElseIf)
			{
				gen.ElseIfStatement(expr, ConditionType(expr));
			}
			else if (codeType == CodeType.EndIf)
			{
				gen.EndIfStatement();
			}
		}


		private void WriteFlipFlag(string flagName, string condition, List<PassedParameter> passedParams)
		{
			Flag f = FindFlag(flagName);
			Register r = f.Register;
			string rname = GetVariableName(r.Name);

			if (string.IsNullOrEmpty(condition))
			{
				gen.SetVariable(rname, r.Type,
					rname + " " + gen.OperatorString(".xor.") + " " + FlagSetMask(f));
			}
			else
			{
				gen.BeginIfStatement(ParseCode(condition, passedParams), ConditionType(condition));

				gen.SetVariable(rname, r.Type,
					rname + " " + gen.OperatorString(".xor.") + " " + FlagSetMask(f));

				gen.EndIfStatement();
			}
		}
		private void WriteResetFlag(string flagName, string condition, List<PassedParameter> passedParams)
		{
			Flag f = FindFlag(flagName);
			Register r = f.Register;
			string rname = GetVariableName(r.Name);


			if (string.IsNullOrEmpty(condition))
			{
				gen.SetVariable(rname, r.Type,
					rname + " " + gen.OperatorString(".and.") + " " + FlagResetMask(f));
			}
			else
			{
				gen.BeginIfStatement(ParseCode(condition, passedParams), ConditionType(condition));

				gen.SetVariable(rname, r.Type,
					rname + " " + gen.OperatorString(".and.") + " " + FlagResetMask(f));

				gen.EndIfStatement();

			}
		}
		private void WriteSetFlag(string flagName, string condition, List<PassedParameter> passedParams)
		{
			Flag f = FindFlag(flagName);
			Register r = f.Register;
			string rname = GetVariableName(r.Name);

			if (string.IsNullOrEmpty(condition))
			{
				gen.SetVariable(rname, r.Type,
					rname + " " + gen.OperatorString(".or.") + " " + FlagSetMask(f));
			}
			else
			{
				gen.BeginIfStatement(ParseCode(condition, passedParams), ConditionType(condition));

				gen.SetVariable(rname, r.Type,
					rname + " " + gen.OperatorString(".or.") + " " + FlagSetMask(f));

				gen.EndIfStatement();

			}
		}
		private void WriteFlag(string flagName, string condition, List<PassedParameter> passedParams)
		{
			Flag f = FindFlag(flagName);
			Register r = f.Register;
			string rname = GetVariableName(r.Name);

			if (string.IsNullOrEmpty(condition))
			{
				gen.SetVariable(rname, r.Type,
					rname + " " + gen.OperatorString(".or.") + " " + FlagSetMask(f));
			}
			else
			{
				gen.BeginIfStatement(ParseCode(condition, passedParams), ConditionType(condition));

				gen.SetVariable(rname, r.Type,
					rname + " " + gen.OperatorString(".or.") + " " + FlagSetMask(f));

				gen.ElseStatement();

				gen.SetVariable(rname, r.Type,
					rname + " " + gen.OperatorString(".and.") + " " + FlagResetMask(f));

				gen.EndIfStatement();

			}
		}

		Regex booleanOperators = new Regex(@"(==|\!=|\>=|\<=|[^\<]\<[^\<]|[^\>]\>[^\>])");

		private string ConditionType(string condition)
		{
			if (booleanOperators.Match(condition).Captures.Count > 0)
				return "bool";
			else
				return "int";
		}

		private string FlagSetMask(Flag f)
		{
			return "FlagSet_" + f.Name;
		}
		private string FlagResetMask(Flag f)
		{
			return "FlagReset_" + f.Name;
		}

		private Flag FindFlag(string flagName)
		{
			if (flagName.StartsWith("!"))
				flagName = flagName.Substring(1);

			Flag f = cpu.FindFlag(flagName);
			return f;
		}

		private string GetType(string text, List<PassedParameter> passed)
		{
			text = ReplaceParameter(text, passed);

			if (text.StartsWith("(") && text.EndsWith(")"))
				return cpu.Info.BaseSize;

			Register r = cpu.FindRegister(text);

			if (r != null)
				return r.Type;
			else
				return cpu.Info.BaseSize;
		}

		Regex tokenizer = new Regex(
			@"^(\(.*?\)|[\{\}]|![a-zA-Z0-9]|\@[a-zA-Z0-9]+|\%[a-zA-Z0-9]|\.[a-z]+?\.|[a-zA-Z_][a-zA-Z_0-9]*|[0-9]+|\$[0-9a-fA-F]+|\+\+|--|>>|<<|\+\=|-\=|\=\=|\!\=|\<\=|\>\=|[+\-/*=<>]|\#+|\.or\.|\.and\.|\.xor\.|\.not\.)", RegexOptions.Compiled);
		Regex identifier = new Regex(@"^[a-zA-Z_][a-zA-Z_0-9]*");

		private string ParseCode(string text, List<PassedParameter> passed)
		{
			string first = ReplaceParameter(text, passed);

			// tokenize the string
			MatchCollection matches = tokenizer.Matches(first);
			
			string retval = "";
			string current = first;

			while (current.Length > 0)
			{
				Match m = tokenizer.Match(current);
				string matchText = m.Value;

				if (matchText == "")
					throw new Exception("Could not understand text.");

                if (IsLibraryCall(matchText)) retval += matchText.Substring(1);
                else if (IsBrace(matchText)) retval += BraceConvert(matchText);
                else if (IsIdentifier(matchText)) retval += GetVariableName(matchText);
                else if (IsSubstitution(matchText)) retval += ReplaceParameter(matchText, passed);
                else if (IsHex(matchText)) retval += gen.HexString(matchText.Substring(1));
                else if (IsNumeric(matchText)) retval += matchText;
                else if (IsOperator(matchText)) retval += gen.OperatorString(matchText);
                else if (IsParenthesis(matchText)) retval += PointerText(matchText, passed);
                else if (IsHash(matchText)) retval += GetVariableName(matchText);
                else if (IsFlag(matchText)) retval += FlagString(matchText);
                else
                    throw new Exception("Could not understand text \"" + matchText + "\".");

				retval += " ";

				current = current.Substring(m.Length).TrimStart();
			}

			return retval.Trim();
		}

        private string BraceConvert(string matchText)
        {
            if (matchText == "{") return "(";
            if (matchText == "}") return ")";

            throw new InvalidOperationException();
        }

        private bool IsBrace(string matchText)
        {
            return matchText == "{" || matchText == "}";
        }

        private bool IsSubstitution(string matchText)
		{
			return matchText.StartsWith("%");
		}

		private bool IsFlag(string matchText)
		{
			return matchText.StartsWith("!");
		}

		private string FlagString(string matchText)
		{
			Flag f = FindFlag(matchText);
			string name = GetVariableName(f.Register.Name);
			string andStr = gen.OperatorString(".and.");
			string bitShift = gen.OperatorString(">>");
			string hex = gen.HexString("1");

			if (f.Bit > 0)
			{
				return "((" + name + " " + bitShift + " " + f.Bit + ") "
					+ andStr + " " + hex + ")";
			}
			else
			{
				return "(" + name + " " + andStr + " " + hex + ")";
			}
		}

		private bool IsHash(string matchText)
		{
			return matchText.StartsWith("#");
		}
		private string PointerText(string matchText, List<PassedParameter> passed)
		{
			if (matchText.StartsWith("(") && matchText.EndsWith(")"))
			{
				matchText = matchText.Substring(1, matchText.Length - 2);
			}
			else
				throw new ArgumentException("Pointer text should be surrounded in parenthesis.");

			if (matchText.Contains("("))
				throw new ArgumentException("Cannot nest parentheses.");

			return Mem(ParseCode(matchText, passed));
		}


		private bool IsParenthesis(string matchText)
		{
			return matchText.StartsWith("(");
		}

		private bool IsOperator(string matchText)
		{
			switch (matchText)
			{
				case "++":
				case "--":
				case "+=":
				case "-=":
				case "==":
				case "!=":
				case "<=":
				case ">=":
				case ">>":
				case "<<":
				case "+":
				case "-":
				case "=":
				case "/":
				case "*":
				case "<":
				case ">":
				case ".xor.":
				case ".and.":
				case ".or.":
				case ".not.":
					return true;
				default:
					return false;
			}
		}

		private bool IsNumeric(string matchText)
		{
			return "0123456789".Contains(matchText[0]);
		}

		private bool IsHex(string matchText)
		{
			return matchText.StartsWith("$");
		}

        private bool IsLibraryCall(string matchText)
        {
            return matchText.StartsWith("@");
        }
		private bool IsIdentifier(string matchText)
		{
			return identifier.Matches(matchText).Count == 1;
		}

		private static string ReplaceParameter(string code, List<PassedParameter> passed)
		{
			for (int i = 0; i < passed.Count; i++)
			{
				code = code.Replace(passed[i].Substitution, passed[i].Name);
			}
			return code;
		}

		char[] paramSplitter = new char[] { ',' };

		List<PassedParameter> GetParameters(Operation operation, string parameters, List<TempVariable> tempVariables)
		{
			List<PassedParameter> retval = new List<PassedParameter>();

			string[] pstring = parameters.Split(paramSplitter, StringSplitOptions.RemoveEmptyEntries);

			if (pstring.Length != operation.Parameters.Count)
				throw new InvalidOperationException($"OpCode {operation.Name} does not have right number of parameters.");

			int ptrValue = 0;
			for(int i = 0; i < operation.Parameters.Count; i++)
			{
				Parameter p = operation.Parameters[i];
				PassedParameter mp = new PassedParameter { IsRef = p.IsRef, Substitution = p.Name, };

				if (pstring[i].Contains("##"))
				{
					string inc = "", inc2;

					if (ptrValue > 0)
						inc = " + " + ptrValue.ToString();
					inc2 = " + " + (ptrValue + 1).ToString();

					TempVariable temp = GenerateTempVariable(tempVariables);
					temp.Initialization =
						Mem(RegisterVariableName + ".PC" + inc) + " + (" +
						Mem(RegisterVariableName + ".PC" + inc2) + " << 8)";

					pstring[i] = pstring[i].Replace("##", temp.Name);
					ptrValue += 2;
				}
				else if (pstring[i].Contains("#"))
				{
					string inc = "";
					if (ptrValue > 0)
						inc = " + " + ptrValue.ToString();

					pstring[i] = pstring[i].Replace("#",
								"(" + cpu.Info.ProgramCounter.Name + inc + ")");
					ptrValue += 1;
				}

				mp.Name = pstring[i];


				retval.Add(mp);
			}

			return retval;
		}

		Regex number = new Regex(@"\$[0-9A-Fa-f]+");
		Regex operators = new Regex(@"[+-]");
		
		private string GetVariableName(string sourceName)
		{
			bool isPtr = false;
			string name = sourceName;
			string offset = "";
			string op = "";

			for (int i = 0; i < localVariables.Count; i++)
			{
				if (localVariables[i].Name == sourceName)
					return sourceName;
			}

			string variable;
			Register r = cpu.FindRegister(name);

			if (r != null)
			{
				variable = RegisterVariableName + "." + r.Name;
			}
			else
			{
				throw new Exception("Could not find variable " + sourceName + ".");
			}

			if (isPtr)
				return Mem(offset + op + variable);
			else
				return offset + op + variable;
		}

		string Mem(string addr)
		{
			return gen.Array(Memory, addr);
		}

		private TempVariable GenerateTempVariable(List<TempVariable> tempVariables)
		{
			string name = "tmp" + tempVariables.Count.ToString();

			TempVariable t = new TempVariable
			{
				Name = name,
			};

			tempVariables.Add(t);
			return t;
		}

		private void FindOpCode(string codePageName, string code, out OpCode opcode, out Op op)
		{
			string myCode = codePageName + code;
			myCode = myCode.ToLowerInvariant();

			foreach(var _opcode in cpu.OpCodes)
			{
				foreach (var _op in _opcode.Ops)
				{
					string tmp = _op.Code.Replace(" ", "").ToLowerInvariant();

					if (tmp == myCode)
					{
						op = _op;
						opcode = _opcode;

						return;
					}
				}
			}

			throw new Exception("Could not find opcode.");
		}

		private void GenerateOpcodeEnumAndTable()
		{
			MultiTableOpcode();
		}

		private void MultiTableOpcode()
		{
			// check the number of multi-byte opcodes enums we need
			List<string> pages = new List<string>();

			foreach (var opcode in cpu.OpCodes)
			{
				foreach (var op in opcode.Ops)
				{
					if (op.Code.Length > 2)
					{
						string page = op.Code.Substring(0, op.Code.Length - 2).ToLowerInvariant();

						if (pages.Contains(page))
							continue;
						else
							pages.Add(page);
					}
				}
			}

			// first do single page opcodes.
			GenerateEnumAndTable("");

			foreach (string codePage in cpu.Info.CodePages)
			{
				GenerateEnumAndTable(codePage);
			}
		}

		private void GenerateEnumAndTable(string codePage)
		{
			EnumGen g = new EnumGen { Name = "OpCode" + codePage.ToUpperInvariant() };
			Table t = new Table { Name = "Mnemonics" + codePage.ToUpperInvariant(), DataType = "string", Missing = "bad opcode" };
			Table cycles = new Table { Name = "Cycles" + codePage.ToUpperInvariant(), DataType = "int", Missing = "99" };

			opCodeEnums.Add(g);
			tables.Add(t);
			tables.Add(cycles);

			foreach (var opcode in cpu.OpCodes)
			{
				foreach (var op in opcode.Ops)
				{
					if (codePage == "" && op.Code.Length > 2)
						continue;
					if (codePage != "" && op.Code.ToLowerInvariant().StartsWith(codePage) == false)
						continue;

					string opCodeValue = op.Code;
					if (codePage != "")
					{
						opCodeValue = opCodeValue.Substring(codePage.Length).Trim();
					}

					string name = opcode.Name + " " + op.Parameters;

					if (op.Name != null)
						name = op.Name;

					name = name.Trim();

					EnumValue v = new EnumValue
					{
						Name = PacifyEnumName(name),
						Value = opCodeValue,
						HexValue = true,
					};

					g.Values.Add(v);

					int codeVal = int.Parse(opCodeValue, System.Globalization.NumberStyles.HexNumber);

					t.Values[codeVal] = name.ToUpperInvariant();
					cycles.Values[codeVal] = op.Cycles.ToString();
				}
			}

			if (codePage == "")
			{
				foreach (string otherCodePage in cpu.Info.CodePages)
				{
					g.Values.Add(new EnumValue
					{
						Name = "OpCode" + otherCodePage.ToUpperInvariant(),
						HexValue = true,
						Value = otherCodePage,
					});

					int codeVal = int.Parse(otherCodePage, System.Globalization.NumberStyles.HexNumber);

					cycles.Values[codeVal] = "0";
				}
			}

			g.VerifyUniqueValues();
		}

		private string PacifyEnumName(string name)
		{
			string retval = name.ToUpperInvariant().Replace(" ", "_");
			retval = retval.Replace(".not.", "N");
			retval = retval.Replace(",", "_");
			retval = retval.Replace("#", "n");
			retval = retval.Replace("@", "s");
			retval = retval.Replace("$", "0x");
			retval = retval.Replace("+", "p");
			retval = retval.Replace("-", "m");
			retval = retval.Replace("(", "x");
			retval = retval.Replace(")", "");
			retval = retval.Replace("!", "flag");

			return retval;
		}
	}

	class TempVariable
	{
		public string Name { get; set; }
		public string Type = "int";
		public string Initialization { get; set; }

	}
}
