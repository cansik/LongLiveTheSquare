using System;
using Eto.Forms;

namespace U4LongLiveTheSquare.Wpf
{
	public class Program
	{
		[STAThread]
		public static void Main (string[] args)
		{
			new Application (Eto.Platforms.Wpf).Run (new MainForm ());
		}
	}
}
