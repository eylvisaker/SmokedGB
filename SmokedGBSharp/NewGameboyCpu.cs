
//#define DASM

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SmokedGBSharp
{
	public partial class GameboyCpu 
	{
		Gameboy theGameboy;
		MemoryController ctrl;
		double totalMs;
		int cyclesPerSecond;
		bool trace;

		int timeToUpdate;

		public MemoryController Memory { get { return ctrl; } }

		int cpuSpeed = 1;
		public int CpuSpeed
		{
			get { return cpuSpeed; }
			set
			{
				cpuSpeed = value;

				cyclesPerSecond = 4194304 * value;
			}
		}
#if DASM
		System.IO.StreamWriter w;
#endif

		public GameboyCpu()
		{
			registers = new Registers();

			LimitSpeed = true;

#if DASM
			w = new System.IO.StreamWriter("dasm.txt");
#endif
		}

		public void Initialize(Gameboy theGameboy)
		{
			this.theGameboy = theGameboy;
			this.mem = theGameboy.rom.MemoryController;
			totalMs = 0;
			trace = false;

			InitializeCpu(theGameboy.IsGbc);
		}

		public void Dispose()
		{
#if DASM
			w.Dispose();
#endif
		}

		private void InitializeCpu(bool gameboyColor)
		{
			CpuSpeed = 1;

			registers.PC = 0x0100;
			registers.AF = 0x01B0;
			registers.BC = 0x0013;
			registers.DE = 0x00D8;
			registers.HL = 0x014D;
			registers.SP = 0xFFFE;
			registers.halt = 0;
			registers.IME = 1;
			registers.EI_ = 0;
			registers.DI_ = 0;

			if (gameboyColor)
				registers.A = 0x11;

			cyclesPerSecond = 4194304;
			cpuCycles = 4;
		}

		public double clock
		{
			get
			{
				return totalMs;
			}
		}

		public int CyclesPerSecond
		{
			get { return cyclesPerSecond; }
		}
		public InstructionInfo Dasm(MemoryController A, int index)
		{
			byte opcode = A[index];

			if (opcode == 0xcb)
			{
				return DasmCB(A, index+1);
			}

			return SubstituteDasmArgs(Mnemonics[opcode], A, index, 1);
		}

		private InstructionInfo DasmCB(MemoryController A, int index)
		{
			byte opcode = A[index];

			return SubstituteDasmArgs(MnemonicsCB[opcode], A, index, 2);
		}


		private static InstructionInfo SubstituteDasmArgs(string text, MemoryController A, int index, int size)
		{
			if (text.Contains("##"))
			{
				int val = A[index + 1] + (A[index + 2] << 8);

				text = text.Replace("##", val.ToString("X4"));
				size += 2;
			}
			else if (text.Contains("#"))
			{
				int val = A[index + 1];

				text = text.Replace("#", val.ToString("X2"));
				size++;
			}
			else if (text.Contains("@"))
			{
				sbyte val = (sbyte)A[index + 1];
				int addr = index + 2 + val;

				text = text.Replace("@", addr.ToString("X4"));
				size++;
			}

			InstructionInfo retval = new InstructionInfo();
			retval.Text = text;
			retval.Size = size;

			return retval;
		}

		bool OnDebug()
		{
			if (Debug != null)
				return Debug(this, registers);

			return
				true;
		}

		public event DebugHandler Debug;

		Stopwatch realClock = new Stopwatch();
		long startTics;

		public void Emulate(int cycles)
		{
			int start = cpuCycles;
			int end = start + cycles;

			if (realClock.IsRunning == false)
			{
				realClock.Reset();
				realClock.Start();
			}

			startTics = realClock.ElapsedTicks;
			usCounter = 0;

			while (cpuCycles < end)
			{
				if (Trace)
				{
					if (OnDebug())
					{
						return;
					}
				}

#if DASM
				WriteDasm();
#endif

				if (registers.halt == 0)
				{
					ExecOpCode();
				}
				else
					cpuCycles = timeToUpdate+1;

#if DASM
				WriteRegisters();
#endif
				if (registers.EI_ > 0)
				{
					registers.EI_--;

					if (registers.EI_ == 0)
						registers.IME = 1;
				}
				if (registers.DI_ > 0)
				{
					registers.DI_--;

					if (registers.DI_ == 0)
						registers.IME = 0;
				}

				if (timeToUpdate < cpuCycles)
				{
					timeToUpdate = 500;

					end -= cpuCycles;
					double us = cpuCycles * 1000000.0 / CyclesPerSecond;

					totalMs += us / 1000;
					theGameboy.PassTime(us);

					if (LimitSpeed)
					{
						SpeedLimiter(us);
					}

				}
			}
		}

		double usCounter;
		private void SpeedLimiter(double us)
		{
			usCounter += us;

			double targetTicsPassed = startTics +
				(long)((usCounter / 1000000.0) * Stopwatch.Frequency);

			while (realClock.ElapsedTicks < targetTicsPassed) ;
		}

		public int TimeToUpdate
		{
			get { return timeToUpdate; }
			set
			{
				if (value > timeToUpdate)
					return;

				timeToUpdate = value;
				cpuCycles = 0;
			}
		}

		public bool LimitSpeed { get; set; }

#if DASM
		List<int> usedAddrs = new List<int>();

		private void WriteDasm()
		{
			//if (usedAddrs.Contains(registers.PC))
			//    return;

			//usedAddrs.Add(registers.PC);

			var info = Dasm(mem, registers.PC);

			w.Write(registers.PC.ToString("X4"));
			w.Write("  ");

			string text = "";
			int start = 0;

			for (int i = start; i < info.Size; i++)
			{
				text += mem[registers.PC + i].ToString("X2");
				text += " ";
			}

			w.Write(text);
			w.Write(new string(' ', 10 - text.Length));

			w.Write(info.Text);

			w.Write(new string(' ', 16 - info.Text.Length));
		}
			
		private void WriteRegisters()
		{
			w.Write("  AF: " + registers.AF.ToString("X4"));
			w.Write("  BC: " + registers.BC.ToString("X4"));
			w.Write("  DE: " + registers.DE.ToString("X4"));
			w.Write("  HL: " + registers.HL.ToString("X4"));
			w.Write("  SP: " + registers.SP.ToString("X4"));
			w.Write("   Stack: ");

			for (int i = 0; i < 3; i++)
			{
				int addr = registers.SP + i*2;

				if (addr >= 0xffff)
					break;


				int value = mem[addr] | (mem[addr + 1] << 8);

				w.Write(" {0:X4}", value);
			}
			w.WriteLine();
		}
#endif

		public void EmulateFor(int cycles)
		{
			Emulate(cycles);
		}

		public void Interrupt(Interrupt interrupt)
		{
			mem[registers.SP - 1] = (byte)(registers.PC >> 8);
			mem[registers.SP - 2] = (byte)(registers.PC & 0xFF);

			registers.SP -= 2;
			registers.halt = 0;

			InterruptMasterFlag = false;
			registers.PC = (ushort)interrupt;
			registers.lastInterrupt = interrupt;

#if DASM
			w.WriteLine("Interrupt " + ((int)interrupt).ToString("X4"));
#endif
		}

		public bool InterruptMasterFlag
		{
			get { return registers.IME != 0; }
			set { registers.IME = value ? (byte)1 : (byte)0; }
		}
	

		public void Step()
		{
			ExecOpCode();
			theGameboy.PassTime(cpuCycles);
			cpuCycles = 0;
		}

		public void StepOver()
		{
			throw new NotImplementedException();
		}

		public bool Trace
		{
			get
			{
				return trace;
			}
			set
			{
				trace = value;
			}
		}

		public MemoryController mem
		{
			get
			{
				return Memory;
			}
			set
			{
				ctrl = value;
			}
		}

		public byte[] vRam
		{
			get { throw new NotImplementedException(); }
		}

	}

	public partial class Registers 
	{
		[FieldOffset(20)]
		Interrupt mLastInterrupt;

		public Interrupt lastInterrupt
		{
			get { return mLastInterrupt; }
			set { mLastInterrupt = value; }
		}

		public Registers Clone()
		{
			Registers retval = new Registers();

			retval.AF = AF;
			retval.BC = BC;
			retval.DE = DE;
			retval.HL = HL;
			retval.PC = PC;
			retval.SP = SP;
			retval.lastInterrupt = lastInterrupt;

			return retval;
		}
	}

	// Re-examine the values here.
	public enum Interrupt : ushort
	{
		VBLANK = 0x0040,
		LCDC = 0x0048,
		TIMER = 0x0050,
		SERIAL = 0x0058,
		JOYPAD = 0x0060,

		NONE = 0xFFFF,
		QUIT = 0xFFFE,
	}

	// dump from CPU 
	[StructLayout(LayoutKind.Explicit)]
	public struct pair
	{
		public pair(byte[] array, int index)
		{
			W = 0;
			l = array[index];
			h = array[index + 1];
		}
		public pair(short value)
		{
			l = 0;
			h = 0;
			W = (ushort)value;
		}
		public pair(ushort value)
		{
			l = 0;
			h = 0;
			W = value;
		}
		// these two should switch is MSB first!
		[FieldOffset(0)]
		public byte l;
		[FieldOffset(1)]
		public byte h;

		[FieldOffset(0)]
		public ushort W;


		public void WriteTo(byte[] array, int index)
		{
			array[index] = l;
			array[index + 1] = h;
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct quad
	{
		[FieldOffset(0)]
		public byte m1;
		[FieldOffset(1)]
		public byte m2;
		[FieldOffset(2)]
		public byte m3;
		[FieldOffset(3)]
		public byte m4;

		[FieldOffset(0)]
		public ushort w1;
		[FieldOffset(2)]
		public ushort w2;

		[FieldOffset(0)]
		public uint dw;

	}


	public struct InstructionInfo
	{
		public string Text;
		public int Size;
	}

	public delegate bool DebugHandler(GameboyCpu cpu, Registers registers);




}
