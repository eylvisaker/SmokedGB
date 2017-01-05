using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AgateLib;
using AgateLib.DisplayLib;
using AgateLib.InputLib;
using AgateLib.Geometry;
using AgateLib.InputLib.Legacy;
using AgateLib.Platform.WinForms.ApplicationModels;
using AgateLib.Platform.WinForms;

namespace SmokedGB
{
	class SmokedGB
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.SetCompatibleTextRenderingDefault(true);
			Application.EnableVisualStyles();

			using (var setup = new AgateSetupWinForms(args))
			{
				setup.CreateDisplayWindow = false;
				setup.InitializeAgateLib();

				new SmokedGB().Run();
			}
		}

		Gameboy gmb;

		Dictionary<KeyCode, Button> mKeys = new Dictionary<KeyCode, Button>();

		void Run()
		{
			mKeys[KeyCode.Up] = Button.Up;
			mKeys[KeyCode.Down] = Button.Down;
			mKeys[KeyCode.Left] = Button.Left;
			mKeys[KeyCode.Right] = Button.Right;
			mKeys[KeyCode.A] = Button.Select;
			mKeys[KeyCode.S] = Button.Start;
			mKeys[KeyCode.Z] = Button.B;
			mKeys[KeyCode.X] = Button.A;

			frmSmoked frm = new frmSmoked();

			DisplayWindow wind = DisplayWindow.CreateFromControl(frm.RenderTarget);

			Input.Unhandled.KeyDown += Keyboard_KeyDown;
			Input.Unhandled.KeyUp += Keyboard_KeyUp;
			gmb = new Gameboy();
			frm.Show();
			frm.Gmb = gmb;

			while (frm.Done == false)
			{
				Display.BeginFrame();
				Display.Clear(Color.Blue);

				gmb.DrawScreen();

				Display.EndFrame();
				Core.KeepAlive();
			}
		}

		void Keyboard_KeyUp(object sender, AgateInputEventArgs e)
		{
			if (gmb.Cpu == null)
				return;


			if (mKeys.ContainsKey(e.KeyCode))
			{
				gmb.SetButtonState(mKeys[e.KeyCode], false);
			}

			if (e.KeyCode == KeyCode.Tilde)
			{
				gmb.Cpu.LimitSpeed = true;
			}
		}
		void Keyboard_KeyDown(object sender, AgateInputEventArgs e)
		{
			if (gmb.Cpu == null)
				return;

			if (mKeys.ContainsKey(e.KeyCode))
			{
				gmb.SetButtonState(mKeys[e.KeyCode], true);
			}

			switch (e.KeyCode)
			{
				case KeyCode.Tilde:
					gmb.Cpu.LimitSpeed = false;
					break;
			}

		}
	}
}