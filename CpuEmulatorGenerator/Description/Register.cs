using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.Description
{
	public class Register
	{
		public Register()
		{
			Location = -1;
		}

		public string Name { get; set; }
		public string Type { get; set; }
		public string Composite { get; set; }
		public string Initial { get; set; }
		public bool Decrement { get; set; }

		public int Location { get; set; }
		public int Size
		{
			get
			{
				switch (Type)
				{
					case "byte": return 1;
					case "short":
					case "ushort": return 2;
					case "int":
					case "uint": return 4;
					case "long":
					case "ulong": return 8;

					default:
						throw new NotSupportedException();
				}
			}
		}
	}
}
