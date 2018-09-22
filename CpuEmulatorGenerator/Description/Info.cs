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
	class Info
	{
		public Info()
		{
			CodePages = new List<string>();
		}

		public Register ProgramCounter { get; set; }
		public Register StackPointer { get; set; }
		public int Addressing { get; set; }
		public string BaseSize { get; set; }
		public List<string> CodePages { get; set; }
	}


}
