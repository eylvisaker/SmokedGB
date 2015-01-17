using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokedGB.MemoryBankControllers
{
	class Mbc2 : MemoryController
	{
		int romBankSelect;

		public Mbc2(Rom rom)
			: base(rom)
		{ }

		protected override void WriteToRom(int address, byte value)
		{
			int currentPage = address & 0xE000;

			// The least significant bit of the upper address
			// byte must be one to select a ROM bank.
			if ((address & 0x100) != 0 && currentPage == 0x2000)
			{
				value &= 0xf;

				if (value == 0)
					value = 1;

				if (value == romBankSelect)
					return;

				if (value >= mRom.RomBanks)
					return;

				//memcpy(theCPU.mem + 0x4000,
				//    theCPU.theRom.romData + value * 0x4000, 0x4000);
				Array.Copy(mRom.RomData, value * 0x4000, mem, 0x4000, 0x4000);

				romBankSelect = value;

			}
			else if (currentPage == 0x0000)
			{
				//The least significant bit of the upper address
				//byte must be zero to enable/disable cart RAM.

				// I'm not sure how to deal with enabling / disabling ram access
				// we should at least implement disabling of writing to ram 
			}
			if (currentPage == 0x0000)
			{

			}
			else if (currentPage == 0x2000)
			{
			}
			else if (currentPage == 0x4000)
			{
				// ram bank switching not available for MBC2
				/*
				memcpy(theCPU.mem + 0xa000, 
					theCPU.swMemory + 
					theCPU.ramBankSelect * 0x2000, 0x2000);

				theCPU.ramBankSelect = value;

				memcpy(theCPU.mem + 0xa000, 
					theCPU.swMemory + value * 0x2000, 0x2000);
				*/
			}
			else if (currentPage == 0x6000)
			{

			}

		}
	}
}