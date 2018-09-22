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

namespace CpuEmulatorGenerator.CodeGenerator
{
    public class EnumGen
    {
        public AccessModifier Access { get; set; } = AccessModifier.Public;
        public string Name { get; set; }
        public List<EnumValue> Values = new List<EnumValue>();

        internal void VerifyUniqueValues()
        {
            for (int i = 0; i < Values.Count; i++)
            {
                for (int j = 0; j < Values.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (Values[i].Value == Values[j].Value)
                        throw new InvalidOperationException(
                            "The values " + Values[i].Name + " and " +
                            Values[j].Name + " are the same numeric value.");
                }
            }
        }
    }
}
