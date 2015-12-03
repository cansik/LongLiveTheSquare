using System;
using Eto.Forms;
using Eto.Drawing;
using System.Diagnostics;

namespace U4LongLiveTheSquare
{
	/// <summary>
	/// Your application's main form
	/// </summary>
	public class MainForm : Form
	{
		public MainForm ()
		{
			Title = "Long live the square!";
			ClientSize = new Size (500, 500);

			var imageView = new ImageView {
				BackgroundColor = Color.FromRgb (0x000000),
				Visible = true
			};
					
			Content = new Scrollable {
				Content = imageView
			};
					
			imageView.MouseDown += (sender, e) => Debug.WriteLine (e.Location);
				
			// create a few commands that can be used for the menu and toolbar
			var clickMe = new Command {
				MenuText = "Click Me!",
				ToolBarText = "Click Me!"
			};
			clickMe.Executed += (sender, e) => MessageBox.Show (this, "I was clicked!");

			var quitCommand = new Command {
				MenuText = "Quit",
				Shortcut = Application.Instance.CommonModifier | Keys.Q
			};
			quitCommand.Executed += (sender, e) => Application.Instance.Quit ();

			var aboutCommand = new Command { MenuText = "About..." };
			aboutCommand.Executed += (sender, e) => MessageBox.Show (this, "About my app...");

			// create menu
			Menu = new MenuBar {
				Items = {
					// File submenu
					new ButtonMenuItem { Text = "&File", Items = { clickMe } },
					// new ButtonMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new ButtonMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
				ApplicationItems = {
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences..." },
				},
				QuitItem = quitCommand,
				AboutItem = aboutCommand
			};

			// create toolbar			
			ToolBar = new ToolBar { Items = { clickMe } };
		}
	}
}
