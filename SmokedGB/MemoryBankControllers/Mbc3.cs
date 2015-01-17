using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokedGB.MemoryBankControllers
{
	class Mbc3 : MemoryController 
	{
		int romBankSelect;
		
		public Mbc3(Rom rom)
			: base(rom)
		{ }

		protected override void WriteToRom(int address, byte value)
		{
			int currentPage = address & 0xE000;

			if (currentPage == 0x2000)
			{
				if (address < 0x3000)
				{
					value = (byte)(value | romBankSelect & 0x100);
				}
				else
				{
					value = (byte)((value & 0x01) << 8);
					value = (byte)(value | romBankSelect & 0xff);
				}

				if (value == romBankSelect || value >= mRom.RomBanks)
					return;

				//memcpy(theCPU.mem + 0x4000,
				//	theCPU.theRom.romData + value * 0x4000, 0x4000);
				Array.Copy(mRom.RomData, value * 0x4000, mem, 0x4000, 0x4000);

				romBankSelect = value;
			}

		}
	}
}
