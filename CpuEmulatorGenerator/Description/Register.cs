//    This file is part of SmokedGB.
//
//    SmokedGB is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    SmokedGB is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with SmokedGB.  If not, see <https://www.gnu.org/licenses/>.

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
