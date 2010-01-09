using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator
{
	class PassedParameter
	{
		public string Name { get; set; }
		public string Substitution { get; set; }
		public bool IsRef { get; set; }
	}
}
