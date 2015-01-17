using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokedGB.MemoryBankControllers
{
	class MbcNone : MemoryController 
	{
		public MbcNone(Rom rom)
			: base(rom)
		{ }


		protected override void WriteToRom(int address, byte value)
		{
		}
	}
}
