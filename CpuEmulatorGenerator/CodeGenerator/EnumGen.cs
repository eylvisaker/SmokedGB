using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpuEmulatorGenerator.CodeGenerator
{
	public class EnumGen
	{
		public AccessModifier Access;
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
