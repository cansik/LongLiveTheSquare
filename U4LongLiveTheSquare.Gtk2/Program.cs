using System;
using Eto.Forms;

namespace U4LongLiveTheSquare.Gtk2
{
	public class Program
	{
		[STAThread]
		public static void Main (string[] args)
		{
			new Application (Eto.Platforms.Gtk2).Run (new MainForm ());
		}
	}
}
