using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.Description
{
	public class Code
	{
		public Code()
		{
			Type = CodeType.Literal;
		}

		public CodeType Type { get; set; }
		public string Dest { get; set; }
		public string Expression { get; set; }
	}

	public enum CodeType
	{
		Literal,
		If,
		ElseIf,
		EndIf,
		WriteTo,
		Flag,
		SetFlag,
		ResetFlag,
		FlipFlag,
	}
}
