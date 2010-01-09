using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.Description
{
	public class Flag
	{
		public string Name { get; set; }
		public int Bit { get; set; }
		public Register Register { get; set; }
	}
}
