using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.Description
{
	class Info
	{
		public Info()
		{
			CodePages = new List<string>();
		}

		public Register ProgramCounter { get; set; }
		public Register StackPointer { get; set; }
		public int Addressing { get; set; }
		public string BaseSize { get; set; }
		public List<string> CodePages { get; set; }
	}


}
