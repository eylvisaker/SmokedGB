using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AgateLib;

namespace SmokedGBSharp
{
	public partial class frmSmoked : Form
	{
		Gameboy gmb;
		bool stopCpu;
		bool breakAtInterrupt;
		int recentCount = 6;
		List<string> recentList = new List<string>();
		List<int> breakpoints = new List<int>();

		public frmSmoked()
		{
			InitializeComponent();
			dasmFont = lblRegisters.Font;

			lstMemory_Resize(this, EventArgs.Empty);

			var setRecentList = AgateLib.Core.Settings["Recent"];
			
			for (int i = 0; i < recentCount; i++)
			{
				string str = "Recent" + i.ToString();

				if (setRecentList.ContainsKey(str))
				{
					recentList.Add(setRecentList[str]);
				}
			}

			RefreshRecentMenu();

			AgateLib.InputLib.Legacy.Keyboard.KeyDown += new AgateLib.InputLib.Legacy.InputEventHandler(Keyboard_KeyDown);

			SetStatus("");
		}

		private void SetStatus(string p)
		{
			status1.Text = p;
		}

		void Keyboard_KeyDown(AgateLib.InputLib.Legacy.InputEventArgs e)
		{
			switch (e.KeyCode)
			{
				case AgateLib.InputLib.KeyCode.D1:
				case AgateLib.InputLib.KeyCode.D2:
				case AgateLib.InputLib.KeyCode.D3:
				case AgateLib.InputLib.KeyCode.D4:
				case AgateLib.InputLib.KeyCode.D5:
				case AgateLib.InputLib.KeyCode.D6:
					int i = e.KeyCode - AgateLib.InputLib.KeyCode.D1;

					if (recentList.Count > i)
					{
						OpenRom(recentList[i]);
					}
					break;

			}
		}

		private void RefreshRecentMenu()
		{
			recentToolStripMenuItem.DropDownItems.Clear();

			for (int i = 0; i < recentList.Count; i++)
			{
				var x = new ToolStripMenuItem(recentList[i]);
				x.Tag = recentList[i];
				x.Click += recentMenu_Click;

				recentToolStripMenuItem.DropDownItems.Add(x);
			}
		}


		private void SaveRecentList()
		{
			var setRecentList = Core.Settings["Recent"];
			setRecentList.Clear();

			for (int i = 0; i < recentList.Count; i++)
				setRecentList["Recent" + (i).ToString()] = recentList[i];

			Core.Settings.SaveSettings();
		}


		void recentMenu_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem s = (ToolStripMenuItem)sender;

			OpenRom((string)s.Tag);
		}
		public Gameboy Gmb
		{
			get { return gmb; }
			set
			{
				gmb = value;
				gmb.Cpu.Debug += new DebugHandler(Cpu_Debug);

			}
		}

		VideoRegisters lastVr;
		VideoRegisters vr = new VideoRegisters();

		Registers lastRegisters;
		Registers registers;

		bool Cpu_Debug(GameboyCpu cpu, Registers registers)
		{
			if (breakpoints.Contains(registers.PC) == false)
			{
				bool brk = true;

				if (Stepping)
				{
					Stepping = false;
					brk = false;
				}
				if (StopCpu == false)
					brk = false;

				if (brk == false && BreakAtInterrupt && registers.lastInterrupt != Interrupt.NONE
					&& registers.PC > 0x38)
				{
					brk = true;
					registers.lastInterrupt = Interrupt.NONE;
				}

				if (brk == false)
					return false;

				StopCpu = true;
			}
			else if (Stepping)
			{
				Stepping = false;
				return false;
			}

			if (lastVr == null)
			{
				lastVr = new VideoRegisters();
				GameboyVideo.LoadRegisters(lastVr, cpu.mem);
				GameboyVideo.LoadRegisters(vr, cpu.mem);
			}
			else
			{
				VideoRegisters t = lastVr;
				lastVr = vr;
				vr = t;
				GameboyVideo.LoadRegisters(vr, cpu.mem);
			}

			lastRegisters = this.registers;
			this.registers = registers.Clone();

			if (lastRegisters == null)
				lastRegisters = registers.Clone();

			gmb.Paused = true;
			BeginInvoke(new EventHandler(UpdateDebugDisplay));

			SetStatus("Paused." +
				((registers.lastInterrupt != Interrupt.NONE) ? "Interrupt: " + registers.lastInterrupt.ToString() : string.Empty));

			return true;
		}

		private void UpdateDebugDisplay(object sender, EventArgs e)
		{
			updateDasm = true;

			lblRegisters.Invalidate();
			lstDasm.Invalidate();
			lstMemory.Invalidate();
			
			UpdateStack(sender, e);

			lblTime.Text = "Time: " + gmb.CpuTime.ToString("0.000");
		}
		private void timer1_Tick(object sender, EventArgs e)
		{
			UpdateTime();
		}

		Stopwatch lastTime = new Stopwatch();
		private void UpdateTime()
		{
			double speed = gmb.CpuTime * 1000.0 / lastTime.ElapsedMilliseconds;

			lblTime.Text = "Speed: " + (speed * 100).ToString("0.000") + "%";

			gmb.CpuTime = 0;
			lastTime.Reset();
			lastTime.Start();

		}

		private void UpdateStack(object sender, EventArgs e)
		{
			string text = "";

			int pt = registers.SP;
			for (int i = 0; i < 20; i++)
			{
				if (pt >= gmb.Cpu.mem.Length - 1)
					break;

				pair p = new pair();
				p.l = gmb.Cpu.mem[pt];
				p.h = gmb.Cpu.mem[pt+1];

				text += p.W.ToString("X4");
				text += " ";

				pt += 2;


			}

			lblStack.Text = text;
		}

		int dasmStart, dasmEnd;
		string[] dasm = new string[10];
		int[] dasmAddr = new int[10];
		Font dasmFont;
		bool updateDasm;

		private void lstDasm_MouseDown(object sender, MouseEventArgs e)
		{
			if (!Paused)
				return;

			Graphics g = lstDasm.CreateGraphics();
			float fontHeight = g.MeasureString("M", dasmFont).Height;
			int line = (int)(e.Y / fontHeight);

			ToggleBreakpoint(dasmAddr[line]);
		}

		private void ToggleBreakpoint(int p)
		{
			if (breakpoints.Contains(p))
			{
				breakpoints.Remove(p);

				if (breakpoints.Count == 0)
				{
					if (gmb.Paused == false)
						gmb.Cpu.Trace = false;
				}
			}
			else
			{
				breakpoints.Add(p);

				gmb.Cpu.Trace = true;
			}

			lstDasm.Invalidate();
		}

		private void lstDasm_Paint(object sender, PaintEventArgs e)
		{
			if (gmb.Cpu == null) return;
			if (registers == null) return;

			Graphics g = e.Graphics;
			float fontHeight = g.MeasureString("M", dasmFont).Height;

			if (updateDasm)
			{
				CreateDasm(g);

				if (dasmStart > registers.PC ||
					dasmEnd <= registers.PC)
				{
					dasmStart = registers.PC;
					CreateDasm(g);
				}
			}


			PointF pt = new PointF();
			pt.X = 16;
			for (int i = 0; i < dasm.Length; i++)
			{
				if (breakpoints.Contains(dasmAddr[i]))
					e.Graphics.DrawIcon(Resources.breakpoint, new Rectangle(0, (int)pt.Y, Resources.breakpoint.Width, Resources.breakpoint.Height));
				if (dasmAddr[i] == registers.PC)
				{
					e.Graphics.DrawIcon(Resources.ip, new Rectangle(0, (int)pt.Y, Resources.ip.Width, Resources.ip.Height));
				}

				e.Graphics.DrawString(dasm[i], dasmFont, Brushes.Black, pt);
				pt.Y += fontHeight;
			}
		}

		private void CreateDasm(Graphics g)
		{
			float fontHeight = g.MeasureString("M", dasmFont).Height;
			int lines = (int)(lstDasm.Height / fontHeight);

			if (dasm.Length != lines)
			{
				dasm = new string[lines];
				dasmAddr = new int[lines];
			}


			int pc = dasmStart;

			for (int i = 0; i < lines; i++)
			{
				StringBuilder b = new StringBuilder();

				if (pc >= gmb.Cpu.mem.Length)
				{
					dasm[i] = string.Empty;
					dasmAddr[i] = pc;
					continue;
				}

				InstructionInfo info = gmb.Cpu.Dasm(gmb.Cpu.mem, pc);

				string d = info.Text;
				int bytes = info.Size;

				b.Append(pc.ToString("X4"));
				b.Append(" ");

				for (int j = 0; j < 3; j++)
				{
					if (j < bytes)
					{
						b.Append(gmb.Cpu.mem[pc + j].ToString("X2"));
						b.Append(" ");
					}
					else
						b.Append("   ");
				}

				b.Append("  ");
				b.Append(d);

				dasm[i] = b.ToString();
				dasmAddr[i] = pc;

				pc += bytes;
			}

			dasmEnd = pc;
		}


		void Append(Graphics g, ref PointF pos, string text)
		{
			Append(g, ref pos, text, Brushes.Black);
		}
		void Append(Graphics g, ref PointF pos, string text, Brush b)
		{
			g.DrawString(text, lblRegisters.Font, b, pos);
			pos.X += g.MeasureString(text.Replace(" ", "T"), lblRegisters.Font).Width;
		}
		void AppendLine(Graphics g, ref PointF pos)
		{
			AppendLine(g, ref pos, "", Brushes.Black);
		}
		void AppendLine(Graphics g, ref PointF pos, string text)
		{
			AppendLine(g, ref pos, text, Brushes.Black);
		}
		void AppendLine(Graphics g, ref PointF pos, string text, Brush b)
		{
			g.DrawString(text, lblRegisters.Font, b, pos);
			pos.X = leftVal;
			pos.Y += g.MeasureString("M", lblRegisters.Font).Height;
		}

		Brush TextBrush(int val, int lastVal)
		{
			if (val == lastVal)
				return Brushes.Black;
			else
				return Brushes.Red;
		}

		float leftVal = 0;
		private void lblRegisters_Paint(object sender, PaintEventArgs e)
		{
			if (registers == null || lastRegisters == null || vr == null || lastVr == null)
				return;
			
			Graphics g = e.Graphics;
			PointF pos = new PointF();
			leftVal = 0;

			g.Clear(lblRegisters.BackColor);

			if (gmb.Cpu.Trace == false)
				return;

			Append(g, ref pos, "CPU Registers", Brushes.Black);
			AppendLine(g, ref pos);

			AddRegister(g, ref pos, registers.AF, lastRegisters.AF, "A", "F");
			AddRegister(g, ref pos, registers.BC, lastRegisters.BC, "B", "C");
			AddRegister(g, ref pos, registers.DE, lastRegisters.DE, "D", "E");
			AddRegister(g, ref pos, registers.HL, lastRegisters.HL, "H", "L");

			AddWord(g, ref pos, registers.PC, lastRegisters.PC, "PC");
			AppendLine(g, ref pos);
			AddWord(g, ref pos, registers.SP, lastRegisters.SP, "SP");
			AppendLine(g, ref pos);

			leftVal = g.MeasureString("CPU Registers               T", lblRegisters.Font).Width;
			pos.X = leftVal;
			pos.Y = 0;
			AppendLine(g, ref pos, "Video Registers");
			AddFlags(g, ref pos, vr.lcdc, lastVr.lcdc, "LCDC");
			AddFlags(g, ref pos, vr.stat, lastVr.stat, "STAT");

			AddByte(g, ref pos, vr.scy, lastVr.scy, "SCY");
			AddByte(g, ref pos, vr.dma, lastVr.dma, "DMA");
			AppendLine(g, ref pos);

			AddByte(g, ref pos, vr.scx, lastVr.scx, "SCX");
			AddByte(g, ref pos, vr.bgp, lastVr.bgp, "BGP");
			AppendLine(g, ref pos);

			AddByte(g, ref pos, vr.ly, lastVr.ly, "LY");
			AddByte(g, ref pos, vr.obp0, lastVr.obp0, "OBP0");
			AppendLine(g, ref pos);

			AddByte(g, ref pos, vr.lyc, lastVr.lyc, "LYC");
			AddByte(g, ref pos, vr.obp1, lastVr.obp1, "OBP1");
			AppendLine(g, ref pos);

			AddByte(g, ref pos, vr.wy, lastVr.wy, "WY");
			AppendLine(g, ref pos);
			AddByte(g, ref pos, vr.wx, lastVr.wx, "WX");
			AppendLine(g, ref pos);

		}

		private void AddByte(Graphics g, ref PointF pos, byte p, byte last, string name)
		{
			string head = (name + ":" + new string(' ', 5)).Substring(0, 6);

			Append(g, ref pos, head, Brushes.Black);
			Append(g, ref pos, p.ToString("X2"), TextBrush(p, last));
			Append(g, ref pos, "   ");
		}
		private void AddRegister(Graphics g, ref PointF pos, ushort p, ushort lastValue, string high, string loww)
		{
			AddRegister(g, ref pos, new pair(p), new pair(lastValue), high, loww);
		}
		private void AddRegister(Graphics g, ref PointF pos, pair p, pair lastValue, string high, string loww)
		{
			Append(g, ref pos, high + loww + ": ", Brushes.Black);
			Append(g, ref pos, p.W.ToString("X4"), TextBrush(p.W, lastValue.W));
			Append(g, ref pos, "  " + high + ": ", Brushes.Black);
			Append(g, ref pos, p.h.ToString("X2"), TextBrush(p.h, lastValue.h));
			Append(g, ref pos, "  " + loww + ": ", Brushes.Black);
			Append(g, ref pos, p.l.ToString("X2"), TextBrush(p.l, lastValue.l));
			AppendLine(g, ref pos);
		}
		private void AddFlags(Graphics g, ref PointF pos, byte w, byte last, string name)
		{
			string head = (name + ":" + new string(' ', 5)).Substring(0, 6);

			Append(g, ref pos, head, Brushes.Black);
			Append(g, ref pos, w.ToString("X2") + "   ", TextBrush(w, last));

			for (int i = 7; i >= 0; i--)
			{
				int val = w & (1 << i);
				int lv = last & (1 << i);

				if (val != 0) val = 1;
				if (lv != 0) lv = 1;

				Append(g, ref pos, val.ToString("X1"), TextBrush(val, lv));
			}

			AppendLine(g, ref pos);
		}

		private void AddWord(Graphics g, ref PointF pos, ushort w, ushort last, string name)
		{
			Append(g, ref pos, name + ": ", Brushes.Black);
			Append(g, ref pos, w.ToString("X4") + "   ", TextBrush(w, last));
		}

		public bool Done { get; set; }
		/// <summary>
		/// Gets a value indicating whether or not the CPU is currently paused.
		/// </summary>
		public bool Paused
		{
			get { return gmb.Paused; }
		}
		/// <summary>
		/// Gets or sets a valoue indicating whether or not the CPU should be 
		/// stopped for debugging at the next instruction.
		/// </summary>
		public bool StopCpu
		{
			get { return stopCpu; }
			set
			{
				stopCpu = value;
				if (gmb == null)
					return;

				gmb.Paused = value;

				if (gmb.Cpu == null)
					return;

				if (value)
					gmb.Cpu.Trace = true;
				else if (breakpoints.Count == 0)
				{
					gmb.Cpu.Trace = false;
					SetStatus("");
				}
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether or not the CPU should be paused
		/// after the next instruction executes.
		/// </summary>
		public bool Stepping
		{
			get;
			set; 
		}
		public Control RenderTarget
		{
			get { return agateRenderTarget1; }
		}

		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void openROMToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFile.ShowDialog() != DialogResult.OK)
				return;

			OpenRom(openFile.FileName);
		}

		private void OpenRom(string filename)
		{
			if (recentList.Contains(filename))
				recentList.Remove(filename);

			recentList.Insert(0, filename);
			SaveRecentList();
			RefreshRecentMenu();

			Rom r = Rom.OpenROM(filename);
			gmb.InsertRom(r);

			if (!StopCpu)
				gmb.BeginEmulation();

			Text = "Smoked GB - " + System.IO.Path.GetFileName(filename);
			status1.Text = "Loaded " + gmb.rom.CartTitle;
		}

		private void showDisassemblyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showDisassemblyToolStripMenuItem.Checked = !showDisassemblyToolStripMenuItem.Checked;
			boxDisassembly.Visible = showDisassemblyToolStripMenuItem.Checked;
		}

		private void frmSmoked_FormClosed(object sender, FormClosedEventArgs e)
		{
			gmb.StopEmulation();
			Done = true;
		}

		private void pauseEmulationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StopCpu = !StopCpu;

			if (StopCpu == false)
				Stepping = true;
			
			SetStatus("");

			pauseCPUToolStripMenuItem.Checked = StopCpu;
		}

		private void lblRegisters_Click(object sender, EventArgs e)
		{

		}

		private void stepOverToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gmb.Paused == false)
				return;

			SetStatus("");

			StopCpu = true;
			Stepping = true;

			gmb.Cpu.StepOver();
		}
		private void stepToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gmb.Cpu.Trace == false)
				return;

			SetStatus("");

			StopCpu = true;
			Stepping = true;

			gmb.Paused = false;
		}

		private void resetCPUToolStripMenuItem_Click(object sender, EventArgs e)
		{
			gmb.ResetCpu();
		}


		private void lstMemory_Paint(object sender, PaintEventArgs e)
		{
			if (gmb == null || gmb.Cpu == null)
				return;

			PointF loc = new PointF();
			float fontHeight = e.Graphics.MeasureString("M", dasmFont).Height;

			for (int p = 0; loc.Y < lstMemory.Height; p++)
			{
				int index = vsbMemory.Value + p;
				index <<= 3;

				if (index >= gmb.Cpu.mem.Length)
					break;

				StringBuilder b = new StringBuilder();

				b.Append(index.ToString("X4"));
				b.Append("    ");

				for (int i = 0; i < 8; i++)
				{
					if (index + i >= gmb.Cpu.mem.Length)
						break;

					b.Append(gmb.Cpu.mem[index + i].ToString("X2"));
					b.Append(" ");

					if (i == 3)
						b.Append(" ");
				}

				e.Graphics.DrawString(b.ToString(), dasmFont, Brushes.Black, loc);
				loc.Y += fontHeight;
			}
		}

		private void lstMemory_Resize(object sender, EventArgs e)
		{
			Graphics g = lstMemory.CreateGraphics();

			float fontHeight = g.MeasureString("M", dasmFont).Height;

			int lines = (int)(lstMemory.Height / fontHeight);

			vsbMemory.LargeChange = lines;
		}

		private void vsbMemory_Scroll(object sender, ScrollEventArgs e)
		{
			lstMemory.Invalidate();
		}

		private void romInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show(gmb.rom.Description);
		}

		private bool BreakAtInterrupt
		{
			get { return breakAtInterrupt; }
			set
			{
				breakAtInterrupt = value;
				breakAtInterruptToolStripMenuItem.Checked = value;

				if (gmb.Cpu == null)
					return;

				if (value)
					gmb.Cpu.Trace = true;
			}
		}

		private void breakAtInterruptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BreakAtInterrupt = !BreakAtInterrupt;
		}


	}
}
