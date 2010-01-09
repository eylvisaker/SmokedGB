using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CpuEmulatorGenerator
{
	using Description;

	class Program
	{
		static void Main(string[] args)
		{
			
			new Program().Run(args);
		}

		private void Run(string[] args)
		{
			string filename = args[0];

			Cpu cpu = Cpu.Load(filename);

			Generator g = new Generator(cpu);

			g.Run();
		}
	}
}
