﻿//    This file is part of SmokedGB.
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
	public class Op
	{
		public string Code { get; set; }
		public int Cycles { get; set; }
		public string Parameters { get; set; }
		public string Operation { get; set; }
		public string Name { get; set; }

		public string IfFlagSet { get; set; }
		public string IfFlagReset { get; set; }

		public void Validate()
		{
			if (IfFlagSet != null && IfFlagReset != null)
				throw new Exception(string.Format(
					"Op {0}: Cannot have both IfFlagSet and IfFlagReset with values.", Code));
		}
	}
}
