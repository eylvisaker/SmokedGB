namespace SmokedGBSharp
{
	partial class frmSmoked
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSmoked));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.romInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.emulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showDisassemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stepOverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.breakAtInterruptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.openFile = new System.Windows.Forms.OpenFileDialog();
			this.lblRegisters = new System.Windows.Forms.Label();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.status1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.boxDisassembly = new System.Windows.Forms.GroupBox();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.lstDasm = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.grpStack = new System.Windows.Forms.GroupBox();
			this.lblStack = new System.Windows.Forms.Label();
			this.grpMemory = new System.Windows.Forms.GroupBox();
			this.vsbMemory = new System.Windows.Forms.VScrollBar();
			this.lstMemory = new System.Windows.Forms.PictureBox();
			this.agateRenderTarget1 = new AgateLib.WinForms.AgateRenderTarget();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.boxDisassembly.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lstDasm)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.grpStack.SuspendLayout();
			this.grpMemory.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lstMemory)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.emulationToolStripMenuItem,
            this.debugToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openRomToolStripMenuItem,
            this.recentToolStripMenuItem,
            this.toolStripSeparator3,
            this.romInfoToolStripMenuItem,
            this.toolStripSeparator2,
            this.quitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openRomToolStripMenuItem
			// 
			this.openRomToolStripMenuItem.Name = "openRomToolStripMenuItem";
			this.openRomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openRomToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.openRomToolStripMenuItem.Text = "Open Rom...";
			this.openRomToolStripMenuItem.Click += new System.EventHandler(this.openROMToolStripMenuItem_Click);
			// 
			// recentToolStripMenuItem
			// 
			this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
			this.recentToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.recentToolStripMenuItem.Text = "Recent";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(180, 6);
			// 
			// romInfoToolStripMenuItem
			// 
			this.romInfoToolStripMenuItem.Name = "romInfoToolStripMenuItem";
			this.romInfoToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.romInfoToolStripMenuItem.Text = "Rom Info";
			this.romInfoToolStripMenuItem.Click += new System.EventHandler(this.romInfoToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.quitToolStripMenuItem.Text = "Quit";
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
			// 
			// emulationToolStripMenuItem
			// 
			this.emulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseCPUToolStripMenuItem,
            this.resetCPUToolStripMenuItem,
            this.showDisassemblyToolStripMenuItem});
			this.emulationToolStripMenuItem.Name = "emulationToolStripMenuItem";
			this.emulationToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
			this.emulationToolStripMenuItem.Text = "Emulation";
			// 
			// pauseCPUToolStripMenuItem
			// 
			this.pauseCPUToolStripMenuItem.Name = "pauseCPUToolStripMenuItem";
			this.pauseCPUToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.pauseCPUToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.pauseCPUToolStripMenuItem.Text = "Pause CPU";
			this.pauseCPUToolStripMenuItem.Click += new System.EventHandler(this.pauseEmulationToolStripMenuItem_Click);
			// 
			// resetCPUToolStripMenuItem
			// 
			this.resetCPUToolStripMenuItem.Name = "resetCPUToolStripMenuItem";
			this.resetCPUToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.resetCPUToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.resetCPUToolStripMenuItem.Text = "Reset CPU";
			this.resetCPUToolStripMenuItem.Click += new System.EventHandler(this.resetCPUToolStripMenuItem_Click);
			// 
			// showDisassemblyToolStripMenuItem
			// 
			this.showDisassemblyToolStripMenuItem.Name = "showDisassemblyToolStripMenuItem";
			this.showDisassemblyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.showDisassemblyToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.showDisassemblyToolStripMenuItem.Text = "Show Disassembly";
			this.showDisassemblyToolStripMenuItem.Click += new System.EventHandler(this.showDisassemblyToolStripMenuItem_Click);
			// 
			// debugToolStripMenuItem
			// 
			this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepToolStripMenuItem,
            this.stepOverToolStripMenuItem,
            this.breakAtInterruptToolStripMenuItem});
			this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
			this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.debugToolStripMenuItem.Text = "Debug";
			// 
			// stepToolStripMenuItem
			// 
			this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
			this.stepToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
			this.stepToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.stepToolStripMenuItem.Text = "Step";
			this.stepToolStripMenuItem.Click += new System.EventHandler(this.stepToolStripMenuItem_Click);
			// 
			// stepOverToolStripMenuItem
			// 
			this.stepOverToolStripMenuItem.Name = "stepOverToolStripMenuItem";
			this.stepOverToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
			this.stepOverToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.stepOverToolStripMenuItem.Text = "Step over";
			this.stepOverToolStripMenuItem.Click += new System.EventHandler(this.stepOverToolStripMenuItem_Click);
			// 
			// breakAtInterruptToolStripMenuItem
			// 
			this.breakAtInterruptToolStripMenuItem.Name = "breakAtInterruptToolStripMenuItem";
			this.breakAtInterruptToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.breakAtInterruptToolStripMenuItem.Text = "Break at Interrupt";
			this.breakAtInterruptToolStripMenuItem.Click += new System.EventHandler(this.breakAtInterruptToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
			// 
			// openFile
			// 
			this.openFile.Filter = "Gameboy Files|*.gb;*.gbc";
			// 
			// lblRegisters
			// 
			this.lblRegisters.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRegisters.Location = new System.Drawing.Point(12, 16);
			this.lblRegisters.Name = "lblRegisters";
			this.lblRegisters.Size = new System.Drawing.Size(416, 161);
			this.lblRegisters.TabIndex = 1;
			this.lblRegisters.Text = "Registers";
			this.lblRegisters.Paint += new System.Windows.Forms.PaintEventHandler(this.lblRegisters_Paint);
			this.lblRegisters.Click += new System.EventHandler(this.lblRegisters_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status1,
            this.lblTime});
			this.statusStrip1.Location = new System.Drawing.Point(0, 538);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(800, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// status1
			// 
			this.status1.AutoSize = false;
			this.status1.Name = "status1";
			this.status1.Size = new System.Drawing.Size(400, 17);
			this.status1.Text = "status1";
			this.status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTime
			// 
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(31, 17);
			this.lblTime.Text = "time";
			// 
			// boxDisassembly
			// 
			this.boxDisassembly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.boxDisassembly.Controls.Add(this.vScrollBar1);
			this.boxDisassembly.Controls.Add(this.lstDasm);
			this.boxDisassembly.Location = new System.Drawing.Point(440, 27);
			this.boxDisassembly.Name = "boxDisassembly";
			this.boxDisassembly.Size = new System.Drawing.Size(357, 282);
			this.boxDisassembly.TabIndex = 3;
			this.boxDisassembly.TabStop = false;
			this.boxDisassembly.Text = "Disassembly";
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(334, 16);
			this.vScrollBar1.Minimum = -100;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 260);
			this.vScrollBar1.TabIndex = 1;
			// 
			// lstDasm
			// 
			this.lstDasm.BackColor = System.Drawing.SystemColors.Window;
			this.lstDasm.Location = new System.Drawing.Point(3, 16);
			this.lstDasm.Name = "lstDasm";
			this.lstDasm.Size = new System.Drawing.Size(328, 260);
			this.lstDasm.TabIndex = 0;
			this.lstDasm.TabStop = false;
			this.lstDasm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstDasm_MouseDown);
			this.lstDasm.Paint += new System.Windows.Forms.PaintEventHandler(this.lstDasm_Paint);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblRegisters);
			this.groupBox1.Location = new System.Drawing.Point(0, 364);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(434, 165);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Registers";
			// 
			// grpStack
			// 
			this.grpStack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpStack.Controls.Add(this.lblStack);
			this.grpStack.Location = new System.Drawing.Point(440, 315);
			this.grpStack.Name = "grpStack";
			this.grpStack.Size = new System.Drawing.Size(357, 55);
			this.grpStack.TabIndex = 1;
			this.grpStack.TabStop = false;
			this.grpStack.Text = "Stack";
			// 
			// lblStack
			// 
			this.lblStack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblStack.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStack.Location = new System.Drawing.Point(6, 16);
			this.lblStack.Name = "lblStack";
			this.lblStack.Size = new System.Drawing.Size(325, 27);
			this.lblStack.TabIndex = 0;
			// 
			// grpMemory
			// 
			this.grpMemory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpMemory.Controls.Add(this.vsbMemory);
			this.grpMemory.Controls.Add(this.lstMemory);
			this.grpMemory.Location = new System.Drawing.Point(440, 376);
			this.grpMemory.Name = "grpMemory";
			this.grpMemory.Size = new System.Drawing.Size(357, 157);
			this.grpMemory.TabIndex = 0;
			this.grpMemory.TabStop = false;
			this.grpMemory.Text = "Memory";
			// 
			// vsbMemory
			// 
			this.vsbMemory.Location = new System.Drawing.Point(334, 16);
			this.vsbMemory.Maximum = 8192;
			this.vsbMemory.Name = "vsbMemory";
			this.vsbMemory.Size = new System.Drawing.Size(17, 124);
			this.vsbMemory.TabIndex = 1;
			this.vsbMemory.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbMemory_Scroll);
			// 
			// lstMemory
			// 
			this.lstMemory.BackColor = System.Drawing.SystemColors.Window;
			this.lstMemory.Location = new System.Drawing.Point(6, 16);
			this.lstMemory.Name = "lstMemory";
			this.lstMemory.Size = new System.Drawing.Size(328, 124);
			this.lstMemory.TabIndex = 0;
			this.lstMemory.TabStop = false;
			this.lstMemory.Resize += new System.EventHandler(this.lstMemory_Resize);
			this.lstMemory.Paint += new System.Windows.Forms.PaintEventHandler(this.lstMemory_Paint);
			// 
			// agateRenderTarget1
			// 
			this.agateRenderTarget1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.agateRenderTarget1.Location = new System.Drawing.Point(0, 24);
			this.agateRenderTarget1.Name = "agateRenderTarget1";
			this.agateRenderTarget1.Size = new System.Drawing.Size(434, 334);
			this.agateRenderTarget1.TabIndex = 1;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// frmSmoked
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 560);
			this.Controls.Add(this.grpMemory);
			this.Controls.Add(this.grpStack);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.boxDisassembly);
			this.Controls.Add(this.agateRenderTarget1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmSmoked";
			this.Text = "Smoked GB";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSmoked_FormClosed);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.boxDisassembly.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lstDasm)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.grpStack.ResumeLayout(false);
			this.grpMemory.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lstMemory)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private AgateLib.WinForms.AgateRenderTarget agateRenderTarget1;
		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Label lblRegisters;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openRomToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem emulationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseCPUToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showDisassemblyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel status1;
		private System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stepOverToolStripMenuItem;
		private System.Windows.Forms.GroupBox boxDisassembly;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox grpStack;
		private System.Windows.Forms.GroupBox grpMemory;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.PictureBox lstDasm;
		private System.Windows.Forms.ToolStripMenuItem resetCPUToolStripMenuItem;
		private System.Windows.Forms.VScrollBar vsbMemory;
		private System.Windows.Forms.PictureBox lstMemory;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem romInfoToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem breakAtInterruptToolStripMenuItem;
		private System.Windows.Forms.Label lblStack;
		private System.Windows.Forms.ToolStripStatusLabel lblTime;
		private System.Windows.Forms.Timer timer1;
	}
}