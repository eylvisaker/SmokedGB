//    This file is part of SmokedGB.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AgateLib;
using AgateLib.DisplayLib;
using AgateLib.InputLib;
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

			using (new AgateWinForms(args).Initialize())
			{
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

			DisplayWindow wind = new DisplayWindowBuilder()
				.RenderToControl(frm.RenderTarget)
				.Build();

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
				AgateApp.KeepAlive();
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