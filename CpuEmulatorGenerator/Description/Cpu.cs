using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CpuEmulatorGenerator.Description
{
	class Cpu
	{
		public Cpu()
		{
			Registers = new List<Register>();
			Flags = new List<Flag>();
			Operations = new List<Operation>();
			OpCodes = new List<OpCode>();
		}

		public string Name { get; set; }
		public Output Output { get; set; }
		public Info Info { get; set; }
		public List<Register> Registers { get; private set; }
		public List<Flag> Flags { get; private set; }
		public List<Operation> Operations { get; private set; }
		public List<OpCode> OpCodes { get; private set; }


		public Flag FindFlag(string flag)
		{
			return Flags.Find(x => x.Name == flag);
		}
		public Register FindRegister(string name)
		{
			return Registers.Find(x => x.Name == name);
		}
		public Register FindRegisterOrThrow(string name)
		{
			var retval = Registers.Find(x => x.Name == name);

			if (retval == null)
				throw new Exception("Register " + name + " was not found.");

			return retval;
		}
		public Operation FindOperation(string name)
		{
			var retval = Operations.Find(x => x.Name == name);

			if (retval == null)
				throw new Exception("Operation " + name + " was not found.");

			return retval;
		}

		internal static Cpu Load(string filename)
		{
			Cpu retval = new Cpu();

			XDocument xdoc = XDocument.Load(filename);

			retval.Name = xdoc.Element("Cpu").Attribute("Name").Value;
			retval.ReadOutput(xdoc);
			retval.ReadRegisters(xdoc);
			retval.ReadInfo(xdoc);
			retval.ReadOperations(xdoc);
			retval.ReadOpCodes(xdoc);
			retval.ReadFlags(xdoc);

			return retval;
		}

		private void ReadFlags(XDocument xdoc)
		{
			Flags.AddRange(from flag in xdoc.Descendants("Flags").Elements("Flag")
						   select new Flag
						   {
							   Name = flag.Attribute("Name").Value,
							   Bit = int.Parse(flag.Attribute("Bit").Value),
							   Register = FindRegister(flag.Attribute("Register").Value),
						   });
		}

		private void ReadOpCodes(XDocument xdoc)
		{
			foreach (var opcode in xdoc.Descendants("OpCodes").Elements("OpCode"))
			{
				OpCode oc = new OpCode { Name = opcode.Attribute("Name").Value };

				string defaultOperation = 
					opcode.Attribute("Operation") != null ? opcode.Attribute("Operation").Value : null;

				oc.Ops.AddRange(
					from op in opcode.Elements("Op")
					select new Op
					{
						Code = op.Attribute("Code").Value,
						Cycles = int.Parse(op.Attribute("Cycles").Value),
						Parameters = op.Attribute("Parameters").Value,
						Operation = op.Attribute("Operation") != null ? op.Attribute("Operation").Value : defaultOperation,
						Name = op.Attribute("Name") != null ? op.Attribute("Name").Value : null,
						IfFlagSet = op.Attribute("IfFlagSet") != null ? op.Attribute("IfFlagSet").Value : null,
						IfFlagReset = op.Attribute("IfFlagReset") != null ? op.Attribute("IfFlagReset").Value : null,
					});

				oc.Ops.ForEach(op => op.Validate());

				OpCodes.Add(oc);
			}
		}

		private void ReadOperations(XDocument xdoc)
		{
			foreach (var operation in xdoc.Descendants("Operations").Elements("Operation"))
			{
				Operation v = new Operation
				{
					Name = operation.Attribute("Name").Value,
					Inline = operation.Attribute("Inline") != null ? operation.Attribute("Inline").Value == "true" : false,
				};
				
				v.Variables.AddRange(from variable in operation.Elements("Variable")
									 select new Variable
									 {
										 Name = variable.Attribute("Name").Value,
										 Type = variable.Attribute("Type").Value,
										 InitialValue = variable.Value,
									 });

				//if (v.Variables.Count != 0 && v.Inline)
				//{
				//    throw new Exception("Cannot have local variables in inline operation " + v.Name + ".");
				//}

				foreach (var code in operation.Elements("Code"))
				{
					Code c = new Code
					{
						Expression = code.Value,
					};

					if (code.Attribute("WriteTo") != null)
					{
						c.Type = CodeType.WriteTo;
						c.Dest = code.Attribute("WriteTo").Value;
					}
					if (code.Attribute("Flag") != null)
					{
						c.Type = CodeType.Flag;
						c.Dest = code.Attribute("Flag").Value;
					}
					if (code.Attribute("SetFlag") != null)
					{
						if (c.Dest != null)
							throw new Exception("Cannot use more than one of WriteTo, SetFlag, ResetFlag.");

						c.Type = CodeType.SetFlag;
						c.Dest = code.Attribute("SetFlag").Value;
					}
					if (code.Attribute("ResetFlag") != null)
					{
						if (c.Dest != null)
							throw new Exception("Cannot use more than one of WriteTo, SetFlag, ResetFlag.");

						c.Type = CodeType.ResetFlag;
						c.Dest = code.Attribute("ResetFlag").Value;
					}
					if (code.Attribute("FlipFlag") != null)
					{
						if (c.Dest != null)
							throw new Exception("Cannot use more than one of WriteTo, SetFlag, ResetFlag.");

						c.Type = CodeType.FlipFlag;
						c.Dest = code.Attribute("FlipFlag").Value;
					}
					if (code.Attribute("If") != null)
					{
						c.Type = CodeType.If;
						c.Expression = code.Attribute("If").Value;
					}
					if (code.Attribute("ElseIf") != null)
					{
						c.Type = CodeType.ElseIf;
						c.Expression = code.Attribute("ElseIf").Value;
					}
					if (code.Attribute("EndIf") != null)
					{
						c.Type = CodeType.EndIf;
					}
					

					v.Code.Add(c);
				}

				if (operation.Attribute("Parameters") != null)
				{
					int paramCount = int.Parse(operation.Attribute("Parameters").Value);

					for (int i = 0; i < paramCount; i++)
						v.Parameters.Add(new Parameter { Name = "%" + (i + 1).ToString() });
				}

				foreach (Parameter p in v.Parameters)
				{
					foreach (var code in v.Code)
					{
						if (code.Type == CodeType.WriteTo && code.Dest == p.Name)
							p.IsRef = true;
					}
				}

				Operations.Add(v);
			}
		}

		private void ReadInfo(XDocument xdoc)
		{
			var info = (from x in xdoc.Descendants("Info") select x).Single();

			Info = new Info
			{
				ProgramCounter = FindRegisterOrThrow(info.Element("ProgramCounter").Attribute("Register").Value),
				StackPointer = FindRegisterOrThrow(info.Element("StackPointer").Attribute("Register").Value),
				Addressing = int.Parse(info.Element("Addressing").Value),
				BaseSize = info.Element("BaseSize").Value,
			};

			Info.CodePages.AddRange(from cp in xdoc.Descendants("Info").Elements("CodePages").Elements("CodePage")
									select cp.Value);
		}

		private void ReadRegisters(XDocument xdoc)
		{
			Registers.AddRange(
						 from reg in xdoc.Descendants("Registers").Elements("Register")
						 let composite = reg.Attribute("Composite") != null ? reg.Attribute("Composite").Value : null
						 let initialValue = reg.Attribute("InitialValue") != null ? reg.Attribute("InitialValue").Value : null
						 let decrement = reg.Attribute("Decrement") != null ? reg.Attribute("Decrement").Value == "true" : false
						 select new Register
						 {
							 Name = reg.Attribute("Name").Value,
							 Type = reg.Attribute("Type").Value,
							 Composite = composite,
							 Initial = initialValue,
							 Decrement = decrement,
						 });
		}

		private void ReadOutput(XDocument xdoc)
		{
			Output = (from op in xdoc.Descendants("Output")
					  select new Output
					  {
						  File = op.Attribute("File").Value,
						  Language = op.Attribute("Language").Value,
						  Namespace = op.Attribute("Namespace").Value,
					  }).Single();
		}

	}
}
