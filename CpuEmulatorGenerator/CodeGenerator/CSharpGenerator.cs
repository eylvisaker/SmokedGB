//    This file is part of SmokedGB.
//
//    SmokedGB is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    SmokedGB is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with SmokedGB.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.CodeGenerator 
{
	class CSharpGenerator : IDisposable 
	{
		StreamWriter w;
		bool beganNamespace = false;
		bool beginLine = true;
		int braceLevel;
		int indentLevel;

		public int IndentLevel
		{
			get { return indentLevel; }
			set {
				if (value < 0)
					throw new ArgumentException();

				indentLevel = value; }
		}

		const int regionThreshold = 10;

		public CSharpGenerator(string filename)
		{
			w = new StreamWriter(filename);
			w.WriteLine("// This file was generated automatically by a code-generating tool.");
			w.WriteLine("// Do not edit manually, or your changes will probably be overwritten");
			w.WriteLine("// when the tool is rerun.");
		}

		public void Dispose()
		{
			int bl = braceLevel;

			for (int i = 0; i < bl; i++)
			{
				EndBrace();
			}

			w.Close();
		}

		void Write(AccessModifier access)
		{
			Write(AccessModifierStr(access));
			Write(" ");
		}
		public void WriteLine()
		{
			WriteLine("");
		}
		void WriteLine(string x)
		{
			Write(x);
			w.WriteLine();
			beginLine = true;
		}
		void Write(string x)
		{
			if (string.IsNullOrEmpty(x))
				return;

			if (beginLine)
			{
				x = x.TrimStart();
			}

			if (x.Length > 0)
			{
				if (beginLine)
					w.Write(new string('\t', braceLevel + IndentLevel));

				w.Write(x);
				beginLine = false;
			}
		}

		internal void AddNamespace(string p)
		{
			if (beganNamespace)
				throw new InvalidOperationException("Already began a namespace.");

			WriteLine("using " + p + ";");
		}

		internal void BeginNamespace(string p)
		{
			WriteLine();
			WriteLine("namespace " + p);
			WriteLine("{");

			braceLevel++;
		}
		public void EndNamespace()
		{
			EndBrace();
		}


		private void AddBrace()
		{
			if (!beginLine)
				WriteLine();
			WriteLine("{");
			braceLevel++;
		}
		void EndBrace()
		{
			EndBrace(false);
		}
		private void EndBrace(bool semicolon)
		{
			braceLevel--;
			
			if (!beginLine)
				WriteLine();

			if (semicolon)
				WriteLine("};");
			else
				WriteLine("}");
		}

		public void BeginPartialClass(string p)
		{
			Write(AccessModifierStr(AccessModifier.Public));
			Write(" partial class ");
			Write(p);
			AddBrace();
		}
		internal void BeginClass(string p)
		{
			BeginClass(p, AccessModifier.Public);
		}
		internal void BeginClass(string p, AccessModifier accessModifier)
		{
			Write(AccessModifierStr(accessModifier));
			Write(" class ");
			Write(p);
			AddBrace();
		}

		string AccessModifierStr(AccessModifier accessModifier)
		{
			switch (accessModifier)
			{
				case AccessModifier.Default: return "";
				case AccessModifier.ProtectedInternal: return "protected internal";
				default: return accessModifier.ToString().ToLowerInvariant();
			}
		}

		internal void EndClass()
		{
			EndBrace();
		}

		private void EndRegion()
		{
			WriteLine();
			WriteLine("#endregion");
		}

		private void BeginRegion(string p)
		{
			WriteLine();
			WriteLine("#region --- " + p + " ---");
			WriteLine();
		}

		internal void WriteEnum(EnumGen enm)
		{
			if (enm.Values.Count > regionThreshold)
			{
				BeginRegion(enm.Name);
			}

			Write(enm.Access);
			Write(" enum ");
			Write(enm.Name);
			AddBrace();

			foreach (var val in enm.Values)
			{
				Write(val.Name);
				Write(" = ");
				if (val.HexValue)
					Write("0x");
				Write(val.Value);
				WriteLine(",");					
			}

			EndBrace();

			if (enm.Values.Count > regionThreshold)
			{
				EndRegion();
			}
		}

		internal void WriteTable(Table tbl)
		{
			if (tbl.Values.Count > regionThreshold)
			{
				BeginRegion(tbl.Name);
			}

			Write("static ");
			Write(AccessModifierStr(tbl.Access));
			Write(" ");
			Write(tbl.DataType);
			Write("[] ");
			Write(tbl.Name);
			Write(" = new ");
			Write(tbl.DataType);
			Write("[]");

			AddBrace();


			string prefix = "", postfix = "";
			if (tbl.DataType == "string")
			{
				prefix = "\"";
				postfix = "\"";
			}


			int largest = tbl.Values.Keys.Max();

			for (int i = 0; i <= largest; i++)
			{
				int length;

				Write(prefix);
				if (tbl.Values.ContainsKey(i) == false)
				{
					if (string.IsNullOrEmpty(tbl.Missing))
						throw new InvalidOperationException("Missing value for table is not set.");

					Write(tbl.Missing);
					length = tbl.Missing.Length;
				}
				else
				{
					Write(tbl.Values[i]);
					length = tbl.Values[i].Length;
				}

				Write(postfix);
				Write(",");

				Write(new string(' ', 20 - length));
				Write("// " + i.ToString());
				Write(new string(' ', 6 - i.ToString().Length));
				Write(i.ToString("X4"));

				WriteLine();
			}


			EndBrace(true);

			if (tbl.Values.Count > regionThreshold)
			{
				EndRegion();
			}
		}

		public string HexString(string value)
		{
			return "0x" + value;
		}

		internal void VariableDecl(string type, string name)
		{
			Write(" ");
			Write(type);
			Write(" ");
			Write(name);
			WriteLine(";");
		}
		internal void VariableDecl(string type, string name, AccessModifier accessModifier)
		{
			Write(AccessModifierStr(accessModifier));
			VariableDecl(type, name);
		}
		internal void VariableDecl(string type, string name, string init)
		{
			Write(" ");
			Write(type);
			Write(" ");
			Write(name);
			Write(" = (");
			Write(type);
			Write(")(");
			Write(init);
			WriteLine(");");
		}

		public void SetVariable(string name, string type, string expr)
		{
			Write(name);
			Write(" = (");
			Write(type);
			Write(")(");
			Write(expr);
			Write(");");
			WriteLine();
		}

		internal void WriteAttribute(string name, params string[] args)
		{
			Write("[");
			Write(name);

			if (args.Length > 0)
			{
				Write("(");
				bool firstOne = false;

				foreach (var x in args)
				{
					if (firstOne)
						Write(",");

					Write(x);

					firstOne = true;
				}

				Write(")");
			}

			WriteLine("]");
		}


		internal void BeginMethod(string returnType, string name, AccessModifier accessModifier, params MethodParameter[] arguments)
		{
			WriteLine();
			Write(accessModifier);
			Write(returnType);
			Write(" ");
			Write(name);
			Write("(");

			for (int i = 0; i < arguments.Length; i++)
			{
				if (i > 0)
					Write(", ");

				if (arguments[i].IsOut)
					Write("out ");
				else if (arguments[i].IsRef)
					Write("ref ");

				Write(arguments[i].Type);
				Write(" ");
				Write(arguments[i].Name);
			}

			WriteLine(")");
			AddBrace();
		}
		public void EndMethod()
		{
			EndBrace();
		}

		public void BeginSwitch(string variable)
		{
			Write("switch (");
			Write(variable);
			Write(")");
			AddBrace();
		}

		public void BeginSwitch(string variable, string type)
		{
			Write("switch ((");
			Write(type);
			Write(")");
			Write(variable);
			Write(")");
			AddBrace();
		}
		public void BeginCase(string value)
		{
			Write("case ");
			Write(value);
			Write(":");
			WriteLine();
			IndentLevel++;
			AddBrace();
		}

		public void EndCase()
		{
			EndBrace();
			WriteLine("break;");
			IndentLevel--;
		}
		public void EndSwitch()
		{
			EndBrace();
		}

		public string Array(string name, string index)
		{
			return name + "[" + index + "]";
		}
		public string Array(string name, string index, bool cast)
		{
			return name + "[(int)" + index + "]";
		}

		public void IncrementVariable(string name, string amount)
		{
			Write(name);
			Write(" += " + amount);
			WriteLine(";");
		}
		public void IncrementVariable(string name, int amount)
		{
			Write(name);
			Write(" += " + amount.ToString());
			WriteLine(";");
		}
		public void IncrementVariable(string name)
		{
			Write(name);
			Write("++");
			WriteLine(";");
		}
		public void DecrementVariable(string name)
		{
			Write(name);
			Write("--");
			WriteLine(";");
		}

		internal void MethodCall(string methodName, params MethodParameter[] methodParameter)
		{
			Write(methodName);
			Write("(");

			if (methodParameter != null)
			{
				for (int i = 0; i < methodParameter.Length; i++)
				{
					if (i > 0) Write(", ");

					if (methodParameter[i].IsOut) Write("out ");
					else if (methodParameter[i].IsRef) Write("ref ");

					Write(methodParameter[i].Name);
				}
			}

			Write(")");
			WriteLine(";");
		}

		internal void WriteStatementLiteral(string code)
		{
			Write(code);
			Write(";");
			WriteLine();
		}


		internal string OperatorString(string matchText)
		{
			switch (matchText)
			{
				case ".not.": return "~";
				case ".and.": return "&";
				case ".xor.": return "^";
				case ".or.": return "|";

				default:
					return matchText;
			}
		}

		public string BitSetCondition(string variable, int bit)
		{
			return "0 != (0x01 & (" + variable + " >> " + bit.ToString() + "))";
		}
		public string BitResetCondition(string variable, int bit)
		{
			return "0 == (0x01 & (" + variable + " >> " + bit.ToString() + "))";
		}

		internal void BeginIfStatement(string condition, string conditionType)
		{
			if (conditionType != "bool")
			{
				Write("if (0 != (");
				Write(condition);
				Write("))");
			}
			else
			{
				Write("if (");
				Write(condition);
				Write(")");
			}
			
			AddBrace();
		}

		internal void BeginIfStatementBitSet(string variable, int bit)
		{
			BeginIfStatement(BitSetCondition(variable, bit), "bool");
		}

		internal void BeginIfStatementBitReset(string variable, int bit)
		{
			BeginIfStatement(BitResetCondition(variable, bit), "bool");
		}

		internal void ElseStatement()
		{
			EndBrace();
			Write("else");
			AddBrace();
		}

		internal void ElseIfStatement(string expr, string p)
		{
			EndBrace();
			Write("else ");
			BeginIfStatement(expr, p);
		}

		internal void EndIfStatement()
		{
			EndBrace();
		}

		internal void WriteConstant(string type, string name, string value)
		{
			Write("public const ");
			Write(type);
			Write(" ");
			Write(name);
			Write(" = ");
			Write(value);
			WriteLine(";");
		}


		internal void AddSetVariable(string varName, string addAmount)
		{
			Write(varName);
			Write(" += ");
			Write(addAmount);
			WriteLine(";");
		}


		internal void Return(string p)
		{
			WriteLine("return " + p + ";");
		}

		internal void Return()
		{
			WriteLine("return;");
		}
	}

}
