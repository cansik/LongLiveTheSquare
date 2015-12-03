using System;
using Eto.Forms;
using Eto.Drawing;
using System.Diagnostics;
using Efalg5GeometrischeAlgo;
using System.Diagnostics.Contracts;

namespace U4LongLiveTheSquare
{
	/// <summary>
	/// Your application's main form
	/// </summary>
	public class MainForm : Form
	{
		GridView canvas;

		public MainForm ()
		{
			Title = "Long live the square!";
			ClientSize = new Size (500, 500);

			canvas = new GridView ();
			canvas.LoadComplete += (sender, e) => canvas.Update ();
			canvas.MouseDown += Canvas_MouseDown;
			canvas.MouseWheel += Canvas_MouseWheel;

			Content = canvas;

			var calcBoundingBox = new Command {
				MenuText = "Bounding Box",
				ToolBarText = "Bounding Box"
			};
			calcBoundingBox.Executed += CalcBoundingBox_Executed;

			var resetGrid = new Command {
				MenuText = "Reset",
				ToolBarText = "Reset"
			};
			resetGrid.Executed += ResetGrid_Executed;

			var quitCommand = new Command {
				MenuText = "Quit",
				Shortcut = Application.Instance.CommonModifier | Keys.Q
			};
			quitCommand.Executed += (sender, e) => Application.Instance.Quit ();

			var aboutCommand = new Command { MenuText = "About" };
			aboutCommand.Executed += (sender, e) => MessageBox.Show (this, "developed by Florian Bruggisser 2015");

			// create menu
			Menu = new MenuBar {
				Items = {
					// File submenu
					new ButtonMenuItem { Text = "&File", Items = { calcBoundingBox } },
				},
				ApplicationItems = {
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences..." },
				},
				QuitItem = quitCommand,
				AboutItem = aboutCommand
			};

			// create toolbar			
			ToolBar = new ToolBar { Items = { resetGrid, calcBoundingBox } };
		}

		void ResetGrid_Executed (object sender, EventArgs e)
		{
			canvas.Geometries.Clear ();
			canvas.ScaleFactor = 1;
			canvas.Update ();
		}

		void Canvas_MouseWheel (object sender, MouseEventArgs e)
		{
			var direction = 0 > e.Delta.Height ? -1 : 1;
			canvas.ScaleFactor += Math.Min (0.5f, Math.Abs (e.Delta.Height)) * direction;
			canvas.Update ();

			Debug.WriteLine (canvas.ScaleFactor);
		}

		void Canvas_MouseDown (object sender, MouseEventArgs e)
		{
			var m = canvas.ProjectionMatrix;
			var translatedPoint = new PointF ((e.Location.X - m.X0) / m.Xx, (e.Location.Y - m.Y0) / m.Yy);

			canvas.Geometries.Add (new Vector2d (translatedPoint.X, translatedPoint.Y));
			canvas.Update ();
		}

		void CalcBoundingBox_Executed (object sender, EventArgs e)
		{
			canvas.Geometries.Add (new Line2d (new Vector2d (0, 0), new Vector2d (500, 500)));
			canvas.Update ();
		}
	}
}
