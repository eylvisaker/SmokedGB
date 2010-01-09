using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.Description
{
	public class Parameter
	{
		public Register Register { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		[Obsolete]
		public bool IsRef { get; set; }
		public ParameterDirection Direction { get; set; }
	}

	public enum ParameterDirection
	{
		In,
		Out,
	}
}
