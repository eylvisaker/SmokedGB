using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.CodeGenerator
{
	public class Table
	{
		public AccessModifier Access = AccessModifier.Default;
		public string Name { get; set; }
		public string DataType { get; set; }
		public string Missing { get; set; }
		public Dictionary<int, string> Values = new Dictionary<int, string>();
	}
}
