using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimingEntry
{
	class Program
	{
		static void Main(string[] args)
		{
			bool done = false;
			string[] chars = new string[] { " " };
			Dictionary<int, int> codes = new Dictionary<int, int>();
			int maxValue=-1;

			readopcodes:
			while (!done)
			{
				Console.Write("{0:000} opcodes.  Enter (opcode cycles): ", codes.Count);
				string input = Console.ReadLine();

				string[] stuff = input.Split(chars, StringSplitOptions.RemoveEmptyEntries);

				if (stuff.Length == 0)
				{
					done = true;
				} 
				else if (stuff.Length != 2)
				{
					Console.WriteLine("Could not understand input.");
					System.Media.SystemSounds.Beep.Play();
				}
				else
				{
					try
					{
						int opcode = int.Parse(stuff[0], System.Globalization.NumberStyles.HexNumber);
						int cycles = int.Parse(stuff[1]);

						if (codes.ContainsKey(opcode))
							System.Media.SystemSounds.Beep.Play();

						codes[opcode] = cycles;

						if (opcode > maxValue)
							maxValue = opcode;
					}
					catch
					{
						Console.WriteLine("Could not understand input.");
						System.Media.SystemSounds.Beep.Play();
					}
				}
			}

			maxValue++;
			bool anyMissing = false;

			Console.Write("Missing opcodes: ");

			for (int i = 0; i < maxValue; i++)
			{
				if (codes.ContainsKey(i) == false)
				{
					anyMissing = true;
					Console.Write(i.ToString("X2"));
					Console.Write("  ");
				}
			}
			int defaultValue = 4;

			if (anyMissing == false)
			{
				Console.WriteLine("NONE");
			}
			else
			{
				Console.WriteLine();
				Console.WriteLine("Add more (y/n)?");

				char input = 'a';
				while (input != 'y' && input != 'n')
					input = Console.ReadKey(true).KeyChar;

				if (input == 'y')
				{
					done = false;
					goto readopcodes;
				}

			defaultValueInput:
				Console.Write("Enter default value for missing opcode: ");
				string val = Console.ReadLine();
				try
				{
					defaultValue = int.Parse(val);
				}
				catch
				{
					goto defaultValueInput;
				}
			}

			Console.WriteLine();

			StringBuilder b = new StringBuilder();
			b.Append("int[] cycles = new int[] {");

			for (int i = 0; i < maxValue; i++)
			{
				if (i % 8 == 0)
				{
					b.AppendLine();
					b.Append("\t");
				}

				if (codes.ContainsKey(i))
					b.Append(codes[i]);
				else
					b.Append(defaultValue);

				b.Append(", ");
			}
			b.AppendLine();
			b.Append("};");

			Console.WriteLine(b.ToString());

			System.IO.File.WriteAllText("output.txt", b.ToString());
		}
	}
}
