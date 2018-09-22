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

namespace SmokedGB.MemoryBankControllers
{
	class Mbc1 : MemoryController
	{
		int romBankSelect;

		public Mbc1(Rom rom)
			: base(rom)
		{ }

		protected override void WriteToRom(int address, byte value)
		{
			int currentPage = address & 0xE000;

			if (currentPage == 0x0000)
			{
				if (MBC1Mode == 1)
				{
					if (value == 0x0a)
						ramBankEnable = true;
					else
						ramBankEnable = false;
				}
			}
			else if (currentPage == 0x2000)
			{
				value &= 0x1f;

				if (value == 0)
					value = 1;

				value = (byte)(RomBank & 0x60 | value);

				RomBank = value;
			}
			else if (currentPage == 0x4000)
			{
				if (MBC1Mode == 0)
				{
					value &= 0x03;

					if (value == 0)
						value = 1;

					value = (byte)(romBankSelect & 0x1f | value << 5);

					if (value == romBankSelect || value >= mRom.RomBanks)
						return;

					//memcpy(theCPU.mem + 0x4000,
					//	theCPU.theRom.romData + value * 0x4000, 0x4000);

					Array.Copy(mRom.RomData, value * 0x4000, mem, 0x4000, 0x4000);
					romBankSelect = value;

				}
				else if (MBC1Mode == 1)
				{
					value &= 0x3;

					if (value == romBankSelect)
						return;

					if (value >= mRom.RamBanks)
						return;

					CopyMemoryToSaveRam();

					ramBankSelect = value;

					Array.Copy(mRom.SaveRam, ramBankSelect * 0x2000, mem, 0xa000, 0x2000);
				}

			}
			else if (currentPage == 0x6000)
			{
				MBC1Mode = value & 0x01;
			}

		}
	}
}