using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.CodeGenerator
{
	class MethodParameter
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public bool IsRef { get; set; }
		public bool IsOut { get; set; }
	}
}
