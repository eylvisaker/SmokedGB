using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.Description
{
	public class Subroutine
	{
		const string invalidChars = " []{}()-+=\\|/?.>,<`~!@#$%^&*";

		public Subroutine()
		{
			Parameters = new List<Parameter>();
			Lines = new List<string>();
		}
		public void Parse(string text)
		{
			string[] lines = text.Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				lines[i] = lines[i].Trim();

				if (lines[i].Contains("//"))
					lines[i] = lines[i].Substring(0, lines[i].IndexOf("//"));

			}

			if (lines[lines.Length - 1] != "end")
				throw new ArgumentException("Subroutine is not complete.");

			Parameters = new List<Parameter>();

			string first = lines[0];

			if (first.StartsWith("sub ") == false)
				throw new ArgumentException("Could not parse subroutine definition.");

			first = first.Substring(4);

			if (first.Contains("(") == false || first.Contains(")") == false)
				throw new ArgumentException("Subroutine definition needs parentheses.");

			int paren = first.IndexOf("(");
			int close = first.IndexOf(")");

			Name = first.Substring(0, paren).Trim();

			foreach (char x in invalidChars)
			{
				if (Name.Contains(x))
				{
					throw new Exception("Invalid chars in name \"" + Name + "\"");
				}
			}


			string paramString = first.Substring(paren + 1, close - paren - 2);

			string[] ps = paramString.Split(',');

			foreach (string p in ps)
			{
				string[] args = p.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
				if (args.Length != 3)
				{
					throw new Exception("Missing have in/out, type, or name for parameters in subroutine " + Name + ".");
				}

				Parameter xp = new Parameter();

				xp.Name = args[2];

				switch (args[0])
				{
					case "in": xp.Direction = ParameterDirection.In; break;
					case "out": xp.Direction = ParameterDirection.Out; break;
					default: throw new Exception("Expected in or out in subroutine definition " + Name + ".");
				}

				xp.Type = args[1];
			}
		}

		public string Name { get; set; }
		public List<Parameter> Parameters { get; set; }
		public List<string> Lines { get; set; }
	}
}
