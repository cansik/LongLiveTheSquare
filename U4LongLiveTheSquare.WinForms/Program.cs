using System;
using Eto.Forms;

namespace U4LongLiveTheSquare.WinForms
{
	public class Program
	{
		[STAThread]
		public static void Main (string[] args)
		{
			new Application (Eto.Platforms.WinForms).Run (new MainForm ());
		}
	}
}
