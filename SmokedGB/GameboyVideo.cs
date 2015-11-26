using System;
using System.Collections.Generic;
using System.Linq;
using AgateLib;
using AgateLib.DisplayLib;
using AgateLib.Geometry;

namespace SmokedGB
{
	public class GameboyVideo
	{
		PixelBuffer screen = new PixelBuffer(PixelFormat.BGRA8888, new Size(160, 144));
		PixelBuffer renderedScreen;
		Surface surface;

		GameboyCpu cpu;
		MemoryController mem;
		byte[] vram;
		Gameboy theGameboy;

		VideoInterrupt lastInterrupt;
		int lastLYC;
		bool rendered;

		byte[] scanlineData;
		int[] scanlinePriorities;
		
		const double newLineTime = 108.7;
		const double searchSpriteAttTime = 19.07;
		const double transferToLcdTime = 41.001 + searchSpriteAttTime;

		double usPassed = 0;

		int[] palette = new int[4];

		// 456 clock cycles per scanline: 108.7 us, this gives 59.73787 fps.
		// 80 cycles for mode 2: 19.070 us here
		// 172 cycles for mode 3: 41.001 us here
		// rest for hblank
		
		#region --- Constructor ---

		public GameboyVideo()
		{
			surface = new Surface(screen);
			renderedScreen = new PixelBuffer(screen, new Rectangle(Point.Empty, screen.Size));

			switch (screen.PixelFormat)
			{
				case PixelFormat.BGRA8888:
					WritePixel = WritePixelBGRA8888;
					break;

				case PixelFormat.RGBA8888:
					WritePixel = WritePixelRGBA8888;
					break;
			}

			for (int i = 0; i < 65536; i++)
			{
				ushort color = (ushort)i;
				gbcPaletteMap[color] = GbcColor((short)color);
			}

			gbPaletteMap[0] = unchecked((int)0xffebebeb);
			gbPaletteMap[1] = unchecked((int)0xffa5a5a5);
			gbPaletteMap[2] = unchecked((int)0xff5f5f5f);
			gbPaletteMap[3] = unchecked((int)0xff191919);


			screen.Clear(Color.White);
			renderedScreen.Clear(Color.White);

			rendered = true;

			surface = new Surface(160, 144);

			scanlinePriorities = new int[160];

			int scBitCount = 32;
			int scTotalWidth = 160 * scBitCount / 8;

			scanlineData = new byte[scTotalWidth];

		}

		public void Initialize(Gameboy theGB)
		{
			theGameboy = theGB;
			cpu = theGB.Cpu;
			mem = theGB.rom.MemoryController;
			vram = mem.VideoMemory;
		}

		#endregion
		#region --- Plotting Pixels ---

		int[] gbcPaletteMap = new int[65536];
		int[] gbPaletteMap = new int[4];


		void PlotPixel(int x, int y, int color, int priority)
		{
			if (x < 0 || x >= 160 || y < 0 || y >= 144)
				return;

			if (scanlinePriorities[x] > priority)
				return;

			scanlinePriorities[x] = priority;

			WritePixel(x, y, color);
		}

		void WritePixelBGRA8888(int x, int y, int color)
		{
			int index = y * screen.RowStride + x * screen.PixelStride;

			screen.Data[index] = (byte)((color) & 0xff);
			screen.Data[index + 1] = (byte)((color >> 8) & 0xff);
			screen.Data[index + 2] = (byte)((color >> 16) & 0xff);
			screen.Data[index + 3] = (byte)((color >> 24) & 0xff);
		}
		void WritePixelRGBA8888(int x, int y, int color)
		{
			int index = y * screen.RowStride + x * screen.PixelStride;

			screen.Data[index] = (byte)((color >> 16) & 0xff);
			screen.Data[index + 1] = (byte)((color >> 8) & 0xff);
			screen.Data[index + 2] = (byte)((color) & 0xff);
			screen.Data[index + 3] = (byte)((color >> 24) & 0xff);
		}

	
		delegate void WritePixelHandler(int x, int y, int color);

		WritePixelHandler WritePixel;

		#endregion
		#region --- Moving Pixels to the screen ---

		public void DrawScreen()
		{
			VideoRegisters vr = new VideoRegisters();
			LoadRegisters(vr);

			if (vr.LcdOn == false)
				return;

			lock (renderedScreen)
			{
				surface.WritePixels(renderedScreen);
				surface.SetScale(theGameboy.ScaleFactor, theGameboy.ScaleFactor);
				surface.Draw(0, 0);
			}
		}

		public void RenderScreen()
		{
			if (rendered)
				return;
			else
				rendered = true;

			lock (renderedScreen)
			{
				renderedScreen.CopyFrom(screen, new Rectangle(Point.Empty, screen.Size), Point.Empty, false);
			}
		}

		#endregion

		#region --- Properties ---

		bool IsGbc
		{
			get { return theGameboy.IsGbc; }
		}

		#endregion

		#region --- Time ---

		void TimeToNextPassTime(double us)
		{
			theGameboy.TimeToNextPassTime(us);
		}
		public void PassTime(double us)
		{
			usPassed += us;

			VideoRegisters vr = new VideoRegisters();
			LoadRegisters(vr);

			vr.stat &= 0xfc;

			
			if (usPassed >= newLineTime)
			{
				if (vr.ly < 144)
				{
					RefreshCurrentScanline(vr);
				}

				// Increment the current line
				vr.ly++;

				if (vr.ly == 144)
				{
					theGameboy.RequestInterrupt(Interrupt.VBLANK);

					lastInterrupt = VideoInterrupt.HBLANK;

					vr.stat |= 1;

					rendered = false;

					RenderScreen();
				}
				else if (vr.ly == 154)
				{
					vr.ly = 0;
					vr.stat |= 2;
				}
				else
				{
					vr.stat |= 2;
				}


				mem.CheckGbcHdma();

				usPassed -= newLineTime;

				TimeToNextPassTime(searchSpriteAttTime);
			}
			else if (usPassed >= transferToLcdTime)		// mode 0: hBlank
			{
				// check if HBlank causes an interrupt
				if ((vr.stat & 0x8) != 0 && lastInterrupt != VideoInterrupt.HBLANK)
				{
					theGameboy.RequestInterrupt(Interrupt.LCDC);
					lastInterrupt = VideoInterrupt.HBLANK;
				}

				TimeToNextPassTime(newLineTime - usPassed);
				// vr.stat |= 0;
			}
			else if (usPassed < searchSpriteAttTime)	// mode 2: Searching OAM
			{
				// Searching OAM  ???? Do anything here?
				if ((vr.stat & 0x20) != 0 && lastInterrupt != VideoInterrupt.OAM)
				{
					lastInterrupt = VideoInterrupt.OAM;
					theGameboy.RequestInterrupt(Interrupt.LCDC);
				}

				vr.stat |= 2;

				TimeToNextPassTime(searchSpriteAttTime - usPassed);
			}
			else if (usPassed < transferToLcdTime)			// mode 3: Transfer OAM... Do anything here?
			{
				vr.stat |= 3;

				TimeToNextPassTime(transferToLcdTime - usPassed);
			}
			else
			{
				throw new Exception("Unhandled video state?");
			}

			// Check if ly is the same as the comparison	
			if (vr.ly == vr.lyc)
			{
				if ((lastLYC != vr.lyc || lastInterrupt != VideoInterrupt.LYC) && (vr.stat & 64) != 0)
				{
					theGameboy.RequestInterrupt(Interrupt.LCDC);
					//interrupt = INT.LCDC;	// generate an LCDC interrupt, if requested
					//mem[0xff0f] |= 2;
				}

				lastInterrupt = VideoInterrupt.LYC;
				lastLYC = vr.lyc;
				vr.stat |= 4;		// set the coincident flag

			}
			else
				vr.stat &= 0xfb;	// reset the coincident flag

			StoreRegisters(vr);
		}

		#endregion
		#region --- Gbc Specific ---

		private static int GbcColor(short gbcColor)
		{
			byte r, g, b;
			//byte	avg;

			b = (byte)(gbcColor >> 10);
			g = (byte)(gbcColor >> 5);
			r = (byte)(gbcColor & 0x1f);
			/*
					if (r >= 4)
						r -= 4;
					else
						r = 0;
			*/
			//avg = (r + g + b) / 3;

			r <<= 3;
			g <<= 3;
			b <<= 3;

			return (int)(0xff000000u | (uint)(r << 16) | (uint)(g << 8) | (uint)b);
		}

		#endregion

		#region --- Palette Functions ---

		int bgPalette(int pal, int index, VideoRegisters vr)
		{
			int mask;

			if (theGameboy.IsGbc) 
			{
				short theValue = (short)new pair(mem.bgPaletteData, (pal & 7) * 8 + index * 2).W;
				return gbcPaletteMap[(ushort)theValue];
			}
			else
			{
				switch (index)
				{
					case 0: mask = 0x03; break;
					case 1: mask = 0x0C; break;
					case 2: mask = 0x30; break;
					case 3: mask = 0xC0; break;
					default:
						throw new Exception();
				}

				index = (vr.bgp & mask) >> (index * 2);

				return MasterPalette(index);
			}
		}
		int objPalette(int pal, int index, VideoRegisters vr)
		{
			int mask;

			if (IsGbc)
			{
				short theValue = (short)new pair(mem.objPaletteData, (pal&7) * 8 + index * 2).W;

				return gbcPaletteMap[(ushort)theValue];
			}
			else
			{
				switch (index)
				{
					case 0: mask = 0x03; break;
					case 1: mask = 0x0C; break;
					case 2: mask = 0x30; break;
					case 3: mask = 0xC0; break;
					default:
						throw new Exception();
				}

				if (pal == 0)
					index = (vr.obp0 & mask) >> (index * 2);
				else
					index = (vr.obp1 & mask) >> (index * 2);

				return MasterPalette(index);
			}
		}

		#endregion

		#region --- Reading and writing registers ---

		void LoadRegisters(VideoRegisters vr)
		{
			LoadRegisters(vr, mem);
		}
		public static void LoadRegisters(VideoRegisters vr, IMemoryController mem)
		{
			vr.lcdc = mem[0xFF40];
			vr.stat = mem[0xFF41];
			vr.scy = mem[0xFF42];
			vr.scx = mem[0xFF43];
			vr.ly = mem[0xFF44];
			vr.lyc = mem[0xFF45];
			vr.dma = mem[0xFF46];
			vr.bgp = mem[0xFF47];
			vr.obp0 = mem[0xFF48];
			vr.obp1 = mem[0xFF49];
			vr.wy = mem[0xFF4A];
			vr.wx = mem[0xFF4B];

			vr.cyclesPerTile = 12;
		}

		void StoreRegisters(VideoRegisters vr)
		{
			mem.MemWrite(0xFF40, vr.lcdc);
			mem.MemWrite(0xFF41, vr.stat);
			// mem.WriteMem(0xFF42, vr.scy);
			// mem.WriteMem(0xFF43, vr.scx);
			mem.MemWrite(0xFF44, vr.ly);
			// mem.WriteMem(0xFF45, vr.lyc);
			// mem.WriteMem(0xFF46, vr.dma);
			// mem.WriteMem(0xFF47, vr.bgp);
			// mem.WriteMem(0xFF48, vr.obp0);
			// mem.WriteMem(0xFF49, vr.obp1);
			// mem.WriteMem(0xFF4A, vr.wy);
			// mem.WriteMem(0xFF4B, vr.wx);
		}

		#endregion

		#region --- Emulating Video ---

		void RefreshCurrentScanline(VideoRegisters vr)
		{
			if ((vr.lcdc & 0x80) == 0)
				return;

			if (vr.ly < 0)
				return;

			if (vr.ly >= 144)
				return;

			Array.Clear(scanlineData, 0, scanlineData.Length);
			Array.Clear(scanlinePriorities, 0, 160);

			mem.CopyMemoryToVram();

			RenderBackground(vr);
			RenderWindow(vr);
			RenderSprites(vr);
		}

		void RenderBackground(VideoRegisters vr)
		{
			if ((vr.lcdc & 1) == 0)			// background is off
			{
				// Clear the row
				Rectangle targetRect;

				//targetRect.Y = vr.ly + vr.scy;
				//targetRect.X = 0;
				//targetRect.Width = 256;
				//targetRect.Height = 1;

				return;
			}

			// render background
			int line, i, j;
			int originalBkTileMapBase = ((vr.lcdc & 8) != 0) ? 0x9c00 : 0x9800;
			int bkTilesBase = ((vr.lcdc & 16) != 0) ? 0x8000 : 0x8800;
			int bkTileMapBase;
			int tileAdjust = (bkTilesBase == 0x8800) ? 128 : 0;
			int tileNum;
			int attrib;
			int linePos;
			int memBase;
			int flipAdjust;
			int bankAdjust;
			sbyte sTile;
			bool flipY, flipX;
			quad val = new quad();

			//WORD theData;
			int bp;
			int startx = vr.scx;
			int finishx = vr.scx + 160;
			int currentx = startx;
			int shift = -(startx % 8);

			originalBkTileMapBase -= 0x8000;
			bkTilesBase -= 0x8000;

			line = vr.ly + vr.scy;
			linePos = line % 8;

			if (line > 255)
				line -= 256;


			bkTileMapBase = originalBkTileMapBase + (line / 8) * 0x20 + currentx / 8;


			for (i = 0; i < 168; i += 8)
			{
				if (vr.ly >= vr.wy && i >= (vr.wx - 7) && ((vr.lcdc & 32) != 0))
					return;

				if (currentx > 255)
				{
					currentx -= 256;
					bkTileMapBase = originalBkTileMapBase + (line / 8) * 0x20 + currentx / 8;
				}

				if (tileAdjust != 0)
				{
					attrib = vram[bkTileMapBase + 0x2000];
					sTile = (sbyte)vram[bkTileMapBase++];
					tileNum = sTile + tileAdjust;
				}
				else
				{
					attrib = vram[bkTileMapBase + 0x2000];
					tileNum = vram[bkTileMapBase++];
				}

				flipY = ((attrib & 64) != 0) ? true : false;
				flipX = ((attrib & 32) != 0) ? true : false;
				bankAdjust = ((attrib & 8) != 0) ? 0x2000 : 0;

				memBase = bkTilesBase + tileNum * 0x10 + bankAdjust;

				if (flipY)
				{
					flipAdjust = 14 - linePos * 2;
				}
				else
				{
					flipAdjust = linePos * 2;
				}

				val.w1 = new pair(vram, memBase + flipAdjust).W;
				//val.w.w2 = *(WORD*)(vMem + memBase + 0x2000 + flipAdjust);


				for (j = 0; j < 4; j++)
				{
					palette[j] = bgPalette(attrib, j, vr);
				}



				for (j = 0; j < 8; j++)
				{
					bp = GetBitPair(val.m1, val.m2, flipX ? j : 7 - j);

					if (0 <= i + j + shift && i + j + shift < 256)
					{
						PlotPixel(i + j + shift, vr.ly, palette[bp], bp != 0 ? 500 : 0);
					}
				}

				//cpu.EmulateFor(vr.cyclesPerTile);

				currentx += 8;

			}
		}
		void RenderSprites(VideoRegisters vr)
		{
			if ((vr.lcdc & 2) == 0)
				return;


			int x, y, tileNum, flags;
			bool flipY, flipX;
			int priority;
			int palette;
			int objTilesBase = 0x8000;
			int line = vr.scy + vr.ly;
			int linePos;
			int bankAdjust = 0;
			int spHeight = ((vr.lcdc & 4) != 0) ? 16 : 8;
			int spMask = (spHeight == 16) ? 0xfe : 0xff;
			int spCount = 0;

			byte theMem1, theMem2;
			int i, j, bp;
			int memBase;

			objTilesBase = 0;

			for (i = 0xfe9f; i > 0xfe00; )
			{
				flags = mem[i--];
				tileNum = mem[i--] & spMask;

				x = mem[i--];
				y = mem[i--];

				if (y == 0 && x == 0 || x > 168)
					continue;

				y -= 16;
				x -= 8;

				if (!(vr.ly - y >= 0 && vr.ly - y < spHeight))
					continue;

				linePos = vr.ly - y;

				spCount++;

				priority = ((flags & 128) != 0) ? 1 : 0;
				flipY = ((flags & 64) != 0) ? true : false;
				flipX = ((flags & 32) != 0) ? true : false;
				palette = ((flags & 16) != 0) ? 1 : 0;

				if (IsGbc)
				{
					bankAdjust = (flags & 8) != 0 ? 0x2000 : 0;
					palette = (flags & 7) != 0 ? 1 : 0;
				}

				memBase = objTilesBase + tileNum * 16 + bankAdjust;

				if (!flipY)
					memBase += linePos * 2;
				else
					memBase += (spHeight - linePos - 1) * 2;

				theMem1 = vram[memBase];
				theMem2 = vram[memBase + 1];

				if (theMem1 == 0 && theMem2 == 0)
					continue;

				for (j = 0; j < 8; j++)
				{
					bp = GetBitPair(theMem1, theMem2, flipX ? j : 7 - j);

					if (bp != 0)
					{
						PlotPixel(x + j, vr.ly, objPalette(palette, bp, vr),
							(priority == 1) ? 1 : 501);
					}
				}
			}
		}
		void RenderWindow(VideoRegisters vr)
		{
			if ((vr.lcdc & 32) == 0)			// window is on
				return;


			// render window
			int line, i, j;
			int originalWndTileMapBase = ((vr.lcdc & 64) != 0 ? 0x9c00 : 0x9800);
			int wndTileMapBase;
			int wndTilesBase = 0x8800;
			int tileAdjust = (wndTilesBase == 0x8800) ? 128 : 0;
			int tileNum;
			int linePos;
			int bankAdjust;
			int memBase;
			sbyte sTile;
			int bp;
			int startx = vr.wx - 7;
			int finishx = vr.wx + 160;
			int currentx = 0;
			int shift = -(startx % 8);
			int flipAdjust;
			bool flipY, flipX;
			int attrib;

			originalWndTileMapBase -= 0x8000;
			wndTilesBase -= 0x8000;

			quad val = new quad();

			if (vr.ly < vr.wy)
				return;

			line = vr.ly - vr.wy;
			linePos = line % 8;

			if (line > 255)
				line -= 256;

			wndTileMapBase = originalWndTileMapBase + (line / 8) * 0x20 + currentx / 8;

			for (i = 0; i < 168; i += 8)
			{
				if (currentx > 255)
				{
					currentx -= 256;
					wndTileMapBase = originalWndTileMapBase + (line / 8) * 0x20 + currentx / 8;
				}

				if (tileAdjust != 0)
				{
					sTile = (sbyte)vram[wndTileMapBase++];
					tileNum = sTile + tileAdjust;
				}
				else
				{
					tileNum = vram[wndTileMapBase++];
				}

				attrib = vram[wndTileMapBase + 0x2000];
				flipY = ((attrib & 64) != 0) ? true : false;
				flipX = ((attrib & 32) != 0) ? true : false;
				bankAdjust = ((attrib & 8) != 0) ? 0x2000 : 0;

				memBase = wndTilesBase + tileNum * 16 + bankAdjust;

				if (flipY)
				{
					flipAdjust = 14 - linePos * 2;
				}
				else
				{
					flipAdjust = linePos * 2;
				}

				unsafe
				{
					fixed (byte* vMemptr = vram)
					{
						val.w1 = *(ushort*)(vMemptr + memBase + flipAdjust);
						val.w2 = *(ushort*)(vMemptr + memBase + 0x2000 + flipAdjust);
					}
				}

				for (j = 0; j < 4; j++)
				{
					palette[j] = bgPalette(attrib, j, vr);
				}

				for (j = 0; j < 8; j++)
				{
					bp = GetBitPair(val.m1, val.m2, flipX ? j : 7 - j);

					if (0 <= i + j + shift && i + j + shift < 256)
					{
						PlotPixel(i + j + shift, vr.ly, palette[bp], bp != 0 ? 500 : 0);
					}
				}

				currentx += 8;
			}
		}

		#endregion

		#region --- Core Utility Functions ---

		int GetBitPair(int theValue1, int theValue2, int thePair)
		{
			int mask = 1 << thePair;
			int newValue = ((theValue1 & mask) >> thePair) + 2 * ((theValue2 & mask) >> thePair);

			return newValue;
		}

		int MasterPalette(int index)
		{
			return gbPaletteMap[index];
		}

		#endregion
	}

	public class VideoRegisters
	{
		public byte lcdc, stat, scy, scx,
				 ly, lyc, dma, bgp,
				 obp0, obp1, wy, wx;

		public int cyclesPerTile;

		public bool LcdOn
		{
			get { return (lcdc & 0x80) != 0; }
		}

	}

	public enum VideoInterrupt
	{
		VBLANK = 1,
		HBLANK,
		OAM,
		LYC
	}
}