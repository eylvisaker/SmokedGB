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
	class Mbc5 : MemoryController
	{
		int romBankSelect = 1;

		public Mbc5(Rom rom)
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