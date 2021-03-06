﻿//    This file is part of SmokedGB.
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

//#define DASM

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SmokedGB
{
    public partial class GameboyCpu : IGameboyCpu
    {
        double totalMs;
        int cyclesPerSecond;
        bool trace;

        int timeToUpdate;

        public IMemoryController Memory { get; set; }

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
            Initialize(theGameboy.rom.MemoryController, theGameboy.IsGbc);

        }

        public void Initialize(IMemoryController memoryController, bool isGbc)
        {
            this.Memory = memoryController;
            totalMs = 0;
            trace = false;

            InitializeCpu(isGbc);
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

        public int DecimalAdjust(byte value)
        {
            bool H = registers.Flag_H;
            bool C = registers.Flag_C;
            bool N = registers.Flag_N;

            int A = value;

            if (!N)
            {
                if (H || (A & 0xF) > 9)
                    A += 0x06;

                if (C || A > 0x9f)
                    A += 0x60;
            }
            else
            {
                if (H)
                    A = (A - 6) & 0xff;
                if (C)
                    A -= 0x60;
            }

            if ((A & 0x100) == 0x100)
                registers.Flag_C = true;

            return A & 0xFF;
        }

        public int CyclesPerSecond
        {
            get { return cyclesPerSecond; }
        }
        public InstructionInfo Dasm(IMemoryController A, int index)
        {
            byte opcode = A[index];

            if (opcode == 0xcb)
            {
                return DasmCB(A, index + 1);
            }

            return SubstituteDasmArgs(Mnemonics[opcode], A, index, 1);
        }

        private InstructionInfo DasmCB(IMemoryController A, int index)
        {
            byte opcode = A[index];

            return SubstituteDasmArgs(MnemonicsCB[opcode], A, index, 2);
        }


        private static InstructionInfo SubstituteDasmArgs(string text, IMemoryController A, int index, int size)
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
                    cpuCycles = timeToUpdate + 1;

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

                    totalMs += us * 0.001;
                    OnPassTime(us);

                    if (LimitSpeed)
                    {
                        SpeedLimiter(us);
                    }
                }
            }
        }

        PassTimeEventArgs passTimeEventArgs = new PassTimeEventArgs();

        private void OnPassTime(double us)
        {
            passTimeEventArgs.Microseconds = us;

            PassTime?.Invoke(this, passTimeEventArgs);
        }

        public event EventHandler<PassTimeEventArgs> PassTime;

        double msCounter;
        Stopwatch limiterStopwatch = new Stopwatch();

        private void SpeedLimiter(double us)
        {
            if (limiterStopwatch.IsRunning == false)
            {
                limiterStopwatch.Restart();
                return;
            }

            msCounter += us * 0.001;

            if (msCounter < 13)
                return;

            bool printed = false;

            while (limiterStopwatch.Elapsed.TotalMilliseconds < msCounter)
            {
                //if (!printed)
                //{
                //    System.Diagnostics.Debug.Print(
                //    $"Waiting for {ms - limiterStopwatch.Elapsed.TotalMilliseconds} milliseconds.");
                //    printed = true;
                //}
            }

            msCounter -= limiterStopwatch.ElapsedMilliseconds;
            limiterStopwatch.Restart();
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
            Memory[registers.SP - 1] = (byte)(registers.PC >> 8);
            Memory[registers.SP - 2] = (byte)(registers.PC & 0xFF);

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
            OnPassTime(cpuCycles);
            cpuCycles = 0;
        }

        public void StepOver()
        {
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

        public bool Flag_H
        {
            get { return (F & GameboyCpu.FlagSet_H) > 0; }
            set
            {
                if (value)
                    F = (byte)(F | GameboyCpu.FlagSet_H);
                else
                    F = (byte)(F & GameboyCpu.FlagReset_H);
            }
        }
        public bool Flag_C
        {
            get { return (F & GameboyCpu.FlagSet_C) > 0; }
            set
            {
                if (value)
                    F = (byte)(F | GameboyCpu.FlagSet_C);
                else
                    F = (byte)(F & GameboyCpu.FlagReset_C);
            }
        }
        public bool Flag_Z
        {
            get { return (F & GameboyCpu.FlagSet_Z) > 0; }
            set
            {
                if (value)
                    F = (byte)(F | GameboyCpu.FlagSet_Z);
                else
                    F = (byte)(F & GameboyCpu.FlagReset_Z);
            }
        }
        public bool Flag_N
        {
            get { return (F & GameboyCpu.FlagSet_N) > 0; }
            set
            {
                if (value)
                    F = (byte)(F | GameboyCpu.FlagSet_N);
                else
                    F = (byte)(F & GameboyCpu.FlagReset_N);
            }
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
