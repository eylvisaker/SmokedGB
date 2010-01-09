using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using SmokedGBSharp;
using AgateLib;

namespace CpuTester
{
	class MyMemory : MemoryController
	{
		public MyMemory(Gameboy theGameboy, FakeRom rom)
			: base(theGameboy, rom)
		{

		}

		public void Update()
		{
			FakeRom.array.CopyTo(base.mem, 0);
		}
	}
	class FakeRom : ROM
	{
		public static byte[] array = new byte[0x8000];

		public FakeRom()
			: base(array, null, "nothing.gb")
		{

		}

		public void SetOpCode(int value)
		{
			array[0] = (byte)value;
		}
		public void SetOpCode(int value, int arg0)
		{
			array[0] = (byte)value;
			array[1] = (byte)arg0;
		}
		public void SetOpCode(int value, int arg0, int arg1)
		{
			array[0] = (byte)value;
			array[1] = (byte)arg0;
			array[2] = (byte)arg1;
		}
	}
	class Program
	{
		static StreamWriter w;
		static string line;
		static bool diff = false;

		static void Main(string[] args)
		{
			using (w = new StreamWriter("output.txt"))
			{
				using (AgateSetup setup = new AgateSetup())
				{
					setup.Initialize(true, false, false);

					Gameboy g = new Gameboy();
					FakeRom r = new FakeRom();

					g.InsertROM(r);

					GameboyCPU mcpu = new GameboyCPU(g);
					NewGameboyCpu ncpu = new NewGameboyCpu(g);
					MyMemory mema = new MyMemory(g, r), memb = new MyMemory(g, r);

					mcpu.mem = mema;
					ncpu.mem = memb;

					mcpu.InitializeCpu(0);

					for (int i = 0; i <= 0xff; i++)
					{
						r.SetOpCode(i);
						mema.Update();
						memb.Update();

						InstructionInfo info = mcpu.Dasm(mema, 0);

						//w.WriteLine(info.Text);

						if (info.Size == 3)
						{
							for (int j = 0; j <= 0xff; j++)
							{
								for (int k = 0; k <= 0xff; k++)
								{
									line = string.Format("{0:X2} {1:X1} {2:X2}  ", i, j, k);
									diff = false;

									r.SetOpCode(i, j, k);

									mema.Update();
									memb.Update();

									info = mcpu.Dasm(mema, 0);

									line += info.Text;
									line += "   ";

									CompareCpus(mcpu, ncpu);

									if (diff)
									{
										w.WriteLine(line);
										w.Flush();
									}
								}
							}
						}
						else if (info.Size == 2)
						{
							for (int j = 0; j <= 0xff; j++)
							{
								line = string.Format("{0:X2} {1:X1}     ", i, j);
								diff = false;

								r.SetOpCode(i, j);

								mema.Update();
								memb.Update();

								info = mcpu.Dasm(mema, 0);

								line += info.Text;
								line += "   ";

								CompareCpus(mcpu, ncpu);

								if (diff)
								{
									w.WriteLine(line);
									w.Flush();
								}
							}
						}
						else if (info.Size == 1)
						{
							line = string.Format("{0:X2}        ", i);
							diff = false;

							r.SetOpCode(i);

							mema.Update();
							memb.Update();

							info = mcpu.Dasm(mema, 0);

							line += info.Text;
							line += "   ";

							CompareCpus(mcpu, ncpu);

							if (diff)
							{
								w.WriteLine(line);
								w.Flush();
							}
						}
					}
				}
			}
		}

		private static void CompareCpus(ICpu mcpu, ICpu ncpu)
		{
			mcpu.InitializeCpu(0);
			ncpu.InitializeCpu(0);

			mcpu.TimeToUpdate = 1000;
			ncpu.TimeToUpdate = 1000;

			bool exception = false;
			Type t1 = null, t2 = null;

			try
			{
				mcpu.Emulate(1);
			}
			catch (Exception e)
			{
				t1 = e.GetType();
				exception = true;
			}

			try
			{
				ncpu.Emulate(1);
			}
			catch (Exception e)
			{
				t2 = e.GetType();
				exception = true;
			}

			if (exception)
			{
				if (t1 != t2)
				{
					diff = true;
					line += string.Format("Exception: {0} {1}", t1, t2);
				}
			}

			DiffRegisters(mcpu.Registers, ncpu.Registers);
		}

		private static void DiffRegisters(IRegisters a, IRegisters b)
		{
			CompareValue("AF", a.AF, b.AF);
			CompareValue("BC", a.BC, b.BC);
			CompareValue("DE", a.DE, b.DE);
			CompareValue("HL", a.HL, b.HL);
			CompareValue("SP", a.SP, b.SP);
			CompareValue("PC", a.PC, b.PC);

		}

		private static void CompareValue(string name, ushort p, ushort p_2)
		{
			if (p != p_2)
			{
				line += name + ": ";
				line += p.ToString("X4") + " " + p_2.ToString("X4");
				line += "   ";
				diff = true;
			}
		}
	}
}
