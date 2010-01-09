using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.Description
{
	public class Op
	{
		public string Code { get; set; }
		public int Cycles { get; set; }
		public string Parameters { get; set; }
		public string Operation { get; set; }
		public string Name { get; set; }

		public string IfFlagSet { get; set; }
		public string IfFlagReset { get; set; }

		public void Validate()
		{
			if (IfFlagSet != null && IfFlagReset != null)
				throw new Exception(string.Format(
					"Op {0}: Cannot have both IfFlagSet and IfFlagReset with values.", Code));
		}
	}
}
