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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AgateLib.Quality;

/*
00- ROM                      01- MBC1                02- MBC1+RAM
03- MBC1+RAM+BATTERY         05- MBC2                06- MBC2+BATTERY
08- ROM+RAM                  09- ROM+RAM+BATTERY     0B- MMM01
0C- MMM01+RAM                0D- MMM01+RAM+BATTERY   0F- MBC3+TIMER+BATTERY
10- MBC3+TIMER+RAM+BATTERY   11- MBC3                12- MBC3+RAM
13- MBC3+RAM+BATTERY         15- MBC4                16- MBC4+RAM
17- MBC4+RAM+BATTERY         19- MBC5                1A- MBC5+RAM
1B- MBC5+RAM+BATTERY         1C- MBC5+RUMBLE         1D- MBC5+RUMBLE+RAM
1E- MBC5+RUMBLE+RAM+BATTERY  FC- POCKET CAMERA       FD- Bandai TAMA5
FE- HuC3                     FF- HuC1+RAM+BATTERY
*/
namespace SmokedGB
{
	public class Rom
	{
		public static Rom OpenROM(string fileName)
		{
			byte[] buffer, sram;
			string sramFileName;

			buffer = File.ReadAllBytes(fileName);

			sramFileName = Path.GetFileNameWithoutExtension(fileName) + ".sav";
			sramFileName = Path.Combine(Path.GetDirectoryName(fileName), sramFileName);

			try
			{
				sram = File.ReadAllBytes(sramFileName);
			}
			catch (FileNotFoundException)
			{
				sram = null;
			}

			return new Rom(buffer, sram, fileName);
		}

		public Rom(byte[] buffer, byte[] sRam, string FileName)
		{
			Condition.Requires<ArgumentNullException>(buffer != null);
			Condition.Requires<ArgumentNullException>(FileName != null);

			int currentPos = 0x0134;
			string crlf = Environment.NewLine;

			CartTitle = ASCIIEncoding.ASCII.GetString(buffer, currentPos, 15);
			CartTitle = CartTitle.Replace('\0', ' ');
			currentPos += 15;

			CartFileName = FileName;
			SRamFileName = Path.GetDirectoryName(FileName) + "/" + 
							 Path.GetFileNameWithoutExtension(FileName) + ".sav";

			GameboyColorRom = buffer[currentPos++];
			NewLicensee = new pair(buffer, currentPos).W;
			currentPos += 2;

			Sgb = buffer[currentPos++];
			RomType = buffer[currentPos++];
			ActualRomSize = GetRomSize(buffer[currentPos++]);
			ActualSaveRamSize = GetSRamSize(buffer[currentPos++]);
			CountryCode = buffer[currentPos++];
			Licensee = buffer[currentPos++];
			HeaderCheck = buffer[currentPos++];
			GlobalCheck = buffer[currentPos++];

			RomData = buffer;

			RomBanks = ActualRomSize / 0x4000;


			if (ActualSaveRamSize == 0 && MbcType(RomType) == typeof(MemoryBankControllers.Mbc2))
			{
				ActualSaveRamSize = 8192;
			}

			if (ActualSaveRamSize != 0)
			{
				if (sRam != null && sRam.Length == ActualSaveRamSize)
				{
					SaveRam = sRam;
				}
				else
				{
					SaveRam = new byte[ActualSaveRamSize];
					if (sRam != null)
					{
						Array.Copy(sRam, SaveRam, Math.Min(sRam.Length, SaveRam.Length));
					}

					Trace.WriteLine("Warning: Bad input .sav size");
				}
			}
			else
			{
				SaveRam = null;
			}

			MemoryController = CreateMbc(RomType);

			RamBanks = ActualSaveRamSize / 0x2000;

			System = (GameboyColorRom == 0xc0) ? GameboySystemTypes.ColorGameboy : GameboySystemTypes.Gameboy;
			System |= (GameboyColorRom == 0x80) ? GameboySystemTypes.Gameboy | GameboySystemTypes.ColorGameboy : GameboySystemTypes.Gameboy;

			System |= (Sgb == 0x3) ? GameboySystemTypes.SuperGameboy : 0;


			Description = "Rom Title: " + CartTitle + crlf;
			Description += "Rom Size: " + ActualRomSize.ToString() + crlf;
			Description += "Ram Size: " + ActualSaveRamSize.ToString() + crlf;
			Description += "MBC Type: " + this.MemoryController.GetType().Name + crlf + crlf;

			Description += "Color? " + ((GameboyColorRom == 0x80 || GameboyColorRom == 0xa0) ? "Yes" : "No") + crlf;
			Description += "Super Gameboy? " + (Sgb != 0 ? "Yes" : "No") + crlf;
			Description += "Rom Type: " + GetRomTypeName(RomType) + crlf;
			Description += "Running System: ";

			bool started = false;

			if ((System & GameboySystemTypes.Gameboy) != 0)
			{
				Description += "GB";
				started = true;
			}

			if ((System & GameboySystemTypes.SuperGameboy) != 0)
			{
				if (started)
					Description += ", ";

				Description += "SuperGB";
				started = true;
			}

			if ((System & GameboySystemTypes.ColorGameboy) != 0)
			{
				if (started)
					Description += ", ";

				Description += "ColorGB";
				started = true;
			}

			Trace.WriteLine(Description);
		}

		internal void Dispose()
		{
			if (SaveRam != null)
			{
				var file = File.Open(SRamFileName, FileMode.Create);
				BinaryWriter w = new BinaryWriter(file);

				w.Write(SaveRam);

				file.Dispose();
			}
		}

		int GetRomSize(int theSize)
		{

			/*			 0 - 256Kbit =  32KByte =   2 banks
						 1 - 512Kbit =  64KByte =   4 banks
						 2 -   1Mbit = 128KByte =   8 banks
						 3 -   2Mbit = 256KByte =  16 banks
						 4 -   4Mbit = 512KByte =  32 banks
						 5 -   8Mbit =   1MByte =  64 banks
						 6 -  16Mbit =   2MByte = 128 banks
					   $52 -   9Mbit = 1.1MByte =  72 banks
					   $53 -  10Mbit = 1.2MByte =  80 banks
					   $54 -  12Mbit = 1.5MByte =  96 banks
			*/
			switch (theSize)
			{
				case 0: return 0x8000;
				case 1: return 0x10000;
				case 2: return 0x20000;
				case 3: return 0x40000;
				case 4: return 0x80000;
				case 5: return 0x100000;
				case 6: return 0x200000;
				case 7: return 0x400000;
				case 0x52: return 0x120000;
				case 0x53: return 0x140000;
				case 0x54: return 0x180000;
				default:
					throw new Exception("Unsupported size.");
			}
		}
		string GetRomTypeName(int theRomType)
		{
			switch (theRomType)
			{
				case 0x0: return "ROM ONLY";
				case 0x1: return "ROM + MBC1";
				case 0x2: return "ROM + MBC1 + RAM";
				case 0x3: return "ROM + MBC1 + RAM + BATT";
				case 0x5: return "ROM + MBC2";
				case 0x6: return "ROM + MBC2 + BATTERY";
				case 0x8: return "ROM + RAM";
				case 0x9: return "ROM + RAM + BATTERY";
				case 0xB: return "ROM + MMM01";
				case 0xC: return "ROM + MMM01 + SRAM";
				case 0xD: return "ROM + MMM01 + SRAM + BATT";
				case 0xF: return "ROM + MBC3 + TIMER + BATT";
				case 0x10: return "ROM + MBC3 + TIMER + RAM + BATT";
				case 0x11: return "ROM + MBC3";
				case 0x12: return "ROM + MBC3 + RAM";
				case 0x13: return "ROM + MBC3 + RAM + BATT";
				case 0x19: return "ROM + MBC5";
				case 0x1A: return "ROM + MBC5 + RAM";
				case 0x1B: return "ROM + MBC5 + RAM + BATT";
				case 0x1C: return "ROM + MBC5 + RUMBLE";
				case 0x1D: return "ROM + MBC5 + RUMBLE + SRAM";
				case 0x1E: return "ROM + MBC5 + RUMBLE + SRAM + BATT";
				case 0x1F: return "Pocket Camera";
				case 0xFD: return "Bandai TAMA5";
				case 0xFE: return "Hudson HuC-3";
				case 0xFF: return "Hudson HuC-1";
				default:
					return "Unknown Type: " + theRomType.ToString();

			}
		}

		public MemoryController MemoryController { get; private set; }

		MemoryController CreateMbc(int theRomType)
		{
			return (MemoryController)Activator.CreateInstance(MbcType(theRomType), this);
		}
		Type MbcType(int theRomType)
		{
			switch (theRomType)
			{
				case 0x0:
				case 0x8:
				case 0x9:
					return typeof(MemoryBankControllers.MbcNone);
				case 0x1:
				case 0x2:
				case 0x3:
					return typeof(MemoryBankControllers.Mbc1);
				case 0x5:
				case 0x6:
					return typeof(MemoryBankControllers.Mbc2);
				case 0xB:
				case 0xC:
				case 0xD:
					return typeof(MemoryBankControllers.Mmm01);
				case 0xF:
				case 0x10:
				case 0x11:
				case 0x12:
				case 0x13:
					return typeof(MemoryBankControllers.Mbc3);
				case 0x19:
				case 0x1A:
				case 0x1B:
				case 0x1C:
				case 0x1D:
				case 0x1E:
					return typeof(MemoryBankControllers.Mbc5);
				case 0x1F:		// Pocket Camera
				case 0xFD:		// Bandai TAMA5
				case 0xFE:		// Hudson HuC-3
				case 0xFF:		// Hudson HuC-1
					throw new NotSupportedException();
				default:
					throw new NotSupportedException();
			}
		}

		int GetSRamSize(int theSRamValue)
		{
			/*         0 - None
					   1 -  16kBit =  2kB = 1 bank
					   2 -  64kBit =  8kB = 1 bank
					   3 - 256kBit = 32kB = 4 banks
					   4 -   1MBit =128kB =16 banks
			*/
			switch (theSRamValue)
			{
				case 0: return 0;
				case 1: return 0x0800;
				case 2: return 0x2000;
				case 3: return 0x8000;
				case 4: return 0x20000;
				default: return 0;
			}
		}

		public string CartTitle { get; private set; }
		public string CartFileName { get; private set; }
		public string SRamFileName { get; private set; }

		public byte GameboyColorRom { get; private set; }
		public ushort NewLicensee { get; private set; }
		public byte Sgb { get; private set; }
		public byte RomType { get; private set; }
		public byte RomSize { get; private set; }
		public byte SaveRamSize { get; private set; }		// byte stored in cartridge for save ram size
		public byte CountryCode { get; private set; }
		public byte Licensee { get; private set; }
		public byte HeaderCheck { get; private set; }
		public byte GlobalCheck { get; private set; }

		public byte[] SaveRam { get; private set; }
		public byte[] RomData { get; private set; }
		public int RomDataSize { get { return RomData.Length; } }

		public GameboySystemTypes System { get; private set; }
		public int ActualRomSize { get; private set; }		// number of bytes in rom
		//public MbcType MbcType { get; private set; }			// mbc type number
		public int ActualSaveRamSize { get; private set; }	// number of bytes for save ram
		public int RomBanks { get; private set; }
		public int RamBanks { get; private set; }

		public string Description { get; private set; }
	}


	public enum MbcType
	{
		MBCNone,
		MBC1,
		MBC2,
		MBC3,
		MMM01,
		MBC5,
		MBCUnsupported = -1
	}
}