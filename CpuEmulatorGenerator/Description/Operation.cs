using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.Description
{
	public class Operation
	{
		public Operation()
		{
			Parameters = new List<Parameter>();
			Code = new List<Code>();
			Variables = new List<Variable>();
			Inline = true;
		}

		public string Name { get; set; }
		public List<Parameter> Parameters { get; set; }
		public List<Variable> Variables { get; set; }
		public List<Code> Code { get; set; }
		public bool Inline { get; set; }

	}
}
