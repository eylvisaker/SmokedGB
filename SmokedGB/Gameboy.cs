using System;
using System.Collections.Generic;
using System.Threading;
using AgateLib;
using AgateLib.Geometry;
using AgateLib.InputLib;
using AgateLib.AudioLib;

namespace SmokedGB
{
	public class Gameboy : IGameboy
	{
		bool mIsGbc;
		double mTotalCpuSeconds;
		Dictionary<Button, bool> mButtons = new Dictionary<Button, bool>();
		Interrupt[] interruptOrder = new Interrupt[] { Interrupt.VBLANK, Interrupt.LCDC, Interrupt.TIMER, Interrupt.SERIAL, Interrupt.JOYPAD };

		double divider = 0;
		/// <summary>
		/// Period for the divider in microseconds.
		/// </summary>
		double dividerPeriod = 61.03515625;

		int[] timerFrequencies = new int[] { 4096, 262144, 65536, 16384 };
		int[] speedDivider = new int[] { 1, 1, 4 };

		double timerPeriod = 0;
		double timer = 0;

		public bool Deleting { get; set; }
		public bool Paused { get; set; }

		public int ScaleFactor { get; set; }

		// Gameboy Hardware:
		public GameboyCpu Cpu { get; private set; }
		public Rom rom { get; private set; }
		public GameboyVideo vid { get; private set; }
		public GameboyAudio aud { get; private set; }

		private Thread GBThread;

		#region --- Gameboy Type Properties ---

		public GameboySystemTypes System
		{
			get { return rom.System; }
		}

		public bool IsGbc
		{
			get { return mIsGbc; }
		}

		#endregion

		#region --- Constructor and Initialization ---

		public Gameboy()
		{
			foreach (Button value in Enum.GetValues(typeof(Button)))
				mButtons[value] = false;

			ScaleFactor = 2;

			Cpu = new GameboyCpu();
			vid = new GameboyVideo();
			aud = new GameboyAudio();

			Paused = true;

			GBThread = new Thread(GBThreadStart);
			GBThread.Start();
		}

		public void ResetCpu()
		{
			Paused = true;

			Cpu.Initialize(this);

			Paused = false;
		}

		#endregion
		#region --- Drawing ---

		public void DrawScreen()
		{
			if (rom == null)
				return;

			vid.DrawScreen();
		}

		#endregion
		#region --- Running emulation ---

		public void InsertRom(Rom theRom)
		{
			aud.StopAudio();

			rom = theRom;
			mIsGbc = (rom.System & GameboySystemTypes.ColorGameboy) != 0;
			rom.MemoryController.TheGameboy = this;
            rom.MemoryController.aud = aud;
            rom.MemoryController.Cpu = Cpu;
			Paused = true;

			Cpu.Initialize(this);
			vid.Initialize(this);
			aud.Initialize(this);

			aud.StartAudio();
		}
		public void BeginEmulation()
		{
			Paused = false;
		}
		public void StopEmulation()
		{
			EndThread();

			Cpu.Memory.CopyMemoryToSaveRam();

			if (rom != null)
				rom.Dispose();

			aud.StopAudio();
		}
		public void GBThreadStart(object x)
		{
			int cycles = 100000;

			for (; ; )
			{
				while (Paused)
				{
					Thread.Sleep(100);

					if (Deleting)
						break;
				}

				if (rom != null)
				{
					Cpu.Emulate(cycles);
				}

				if (Deleting)
					break;
			}
		}

		public void EndThread()
		{
			if (GBThread == null)
				return;

			Deleting = true;
			Paused = false;

			int waitCount = 0;

			while (GBThread.IsAlive)
			{
				Thread.Sleep(10);
				waitCount++;

				if (waitCount > 50)
				{
					GBThread.Abort();
				}
			}

			Deleting = false;
		}

		#endregion

		#region --- Timing ---

		/// <summary>
		/// Number of seconds that have been emulated.
		/// </summary>
		public double CpuTime
		{
			get { return mTotalCpuSeconds; }
			set { mTotalCpuSeconds = value; }
		}

		public void PassTime(double us)
		{
			mTotalCpuSeconds += us / 1000000.0;

			vid.PassTime(us);
			aud.PassTime(us);
			IncrementTimer(us);

			if (Cpu.InterruptMasterFlag == false)
				return;

			if ((Cpu.Memory[0xff0f] & 0x0f) == 0)
				return;

			for (int i = 0; i < interruptOrder.Length; i++)
			{
				int icheck = Cpu.Memory[0xff0f] & Cpu.Memory[0xffff];
				bool flag = ((icheck >> i) & 0x01) != 0;

				if (flag)
				{
					Cpu.Interrupt(interruptOrder[i]);

					Cpu.Memory[0xff0f] = (byte)(Cpu.Memory[0xff0f] & ~(0x01 << i));
					break;
				}
			}
		}

		public void TimeToNextPassTime(double us)
		{
			Cpu.TimeToUpdate = (int)(us / 1000000 * Cpu.CyclesPerSecond + 0.5);
		}

		#endregion
		#region --- Interrupts ---

		public void UpdateTimerFrequency()
		{
			int timerFrequency = timerFrequencies[Cpu.Memory[0xff07] & 3];
			timerPeriod = 1000000.0 / timerFrequency;

		}
		private void IncrementTimer(double us)
		{
			// this is the divider register 0xff04
			divider += us;

			if (divider >= dividerPeriod)
			{
				divider -= dividerPeriod;

				int value = Cpu.Memory[0xff04];

				Cpu.Memory.MemWrite(0xff04, (byte)(value + 1));

				TimeToNextPassTime(dividerPeriod - divider);
			}

			// timer register
			if ((Cpu.Memory[0xff07] & 4) != 0)
			{
				timer += us;
				if (timer >= timerPeriod)
				{
					timer -= timerPeriod;

					Cpu.Memory[0xff05] += 1;

					if (Cpu.Memory[0xff05] == 0)
					{
						RequestInterrupt(Interrupt.TIMER);
						Cpu.Memory[0xff05] = Cpu.Memory[0xff06];
					}
				}

				TimeToNextPassTime(timerPeriod - timer);
			}
			else
			{
				timer = 0;
			}
		}

		bool AllowVBlankInterrupt
		{
			get { return (Cpu.Memory[0xffff] & 1) != 0; }
		}
		bool AllowLcdcInterrupt
		{
			get { return (Cpu.Memory[0xffff] & 2) != 0; }
		}
		bool AllowTimerInterrupt
		{
			get { return (Cpu.Memory[0xffff] & 4) != 0; }
		}
		bool AllowSerialIOInterrupt
		{
			get { return (Cpu.Memory[0xffff] & 8) != 0; }
		}
		bool AllowJoystickInterrupt
		{
			get { return (Cpu.Memory[0xffff] & 16) != 0; }
		}

		public void RequestInterrupt(Interrupt interrupt)
		{
			switch (interrupt)
			{
				case Interrupt.VBLANK: Cpu.Memory[0xff0f] |= 1; break;
				case Interrupt.LCDC: Cpu.Memory[0xff0f] |= 2; break;
				case Interrupt.TIMER: Cpu.Memory[0xff0f] |= 4; break;
				case Interrupt.SERIAL: Cpu.Memory[0xff0f] |= 8; break;
				case Interrupt.JOYPAD: Cpu.Memory[0xff0f] |= 16; break;
			}
		}

		#endregion
		#region --- Joystick I/O ---

		public bool ButtonState(Button b)
		{
			return mButtons[b];
		}
		public void SetButtonState(Button b, bool value)
		{
			mButtons[b] = value;

			// TODO: Joystick interrupt
		}

		public void CheckJoysticks()
		{
			byte inRegister = Cpu.Memory[0xff00];
			byte value = (byte)~inRegister;
			value &= 0xf0;

			if (inRegister == 3)
			{
				value = 0xf0;  // 0x30 for super gameboy
			}
			else if ((inRegister & 0x30) == 0x10)	// p15 is selected (0)
			{
				if (ButtonState(Button.Start))			// start?
				{
					value |= 0x8;
				}
				if (ButtonState(Button.Select))		// select ?
				{
					value |= 0x4;
				}
				if (ButtonState(Button.B))		// b ?
				{
					value |= 0x2;
				}
				if (ButtonState(Button.A))		// a ?
				{
					value |= 0x1;
				}
			}
			else if ((inRegister & 0x30) == 0x20)		// p14 is selected (0)
			{

				if (ButtonState(Button.Down))
				{
					value |= 0x8;
				}
				if (ButtonState(Button.Up))
				{
					value |= 0x4;
				}
				if (ButtonState(Button.Left))
				{
					value |= 0x2;
				}
				if (ButtonState(Button.Right))
				{
					value |= 0x1;
				}
			}

			Cpu.Memory.MemWrite(0xff00, (byte)~value);
		}

		#endregion

	}

	[Flags]
	public enum GameboySystemTypes
	{
		Gameboy = 1,
		SuperGameboy = 2,
		ColorGameboy = 4,
	}

	public enum Button
	{
		Up,
		Down,
		Left,
		Right,

		A,
		B,
		Select,
		Start,
	}
}