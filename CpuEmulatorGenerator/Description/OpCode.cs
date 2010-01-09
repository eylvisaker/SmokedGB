using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.Description
{
	class OpCode
	{
		public OpCode() { Ops = new List<Op>(); }

		public string Name { get; set; }
		public List<Op> Ops { get; private set; }
	}
}
