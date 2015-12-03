using System;
using Eto.Forms;
using Eto.Drawing;
using System.Collections.Generic;
using Efalg5GeometrischeAlgo;
using System.Dynamic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;

namespace U4LongLiveTheSquare
{
	public class GridView : Drawable
	{
		public List<IGeometry> Geometries { get; private set; }

		public Color ClearColor { get; set; } = Color.FromRgb (0x000000);

		public Color ForeGroundColor { get; set; } = Color.FromRgb(0xFFFFFF);

		public float ScaleFactor { get; set; } = 1;

		public PointF TransformationDelta { get; set; } = new PointF(0, 0);

		public IMatrix ProjectionMatrix {
			get {
				//scaling / transformation / rotation
				var centerX = Width / 2f + TransformationDelta.X;
				var centerY = Height / 2f + TransformationDelta.Y; 

				var m = Matrix.Create ();

				//translate xy
				m.Translate (centerX, centerY);

				//scale
				m.Scale (ScaleFactor);

				//flip coordination system to cartesian coordinate system
				m.Yy *= -1;

				return m;
			}
		}

		public GridView ()
		{
			Geometries = new List<IGeometry> ();

			Paint += GridViewPaint;

			Cursor = Cursors.Crosshair;
		}

		void GridViewPaint (object sender, PaintEventArgs e)
		{
			var g = e.Graphics;

			//clear image
			g.Clear (ClearColor);

			//generate paths
			var path = new GraphicsPath ();
			foreach (var geometry in Geometries)
				path.AddPath (geometry.GraphicsPath);

			//draw paths
			var brush = new SolidBrush (ForeGroundColor);
			var pen = new Pen (ForeGroundColor, 2);

			var m = ProjectionMatrix;

			if (!path.IsEmpty) {
				path.Transform (m);

				//draw
				g.FillPath (brush, path);
				g.DrawPath (pen, path);
			}

			//test
			var rect1 = new GraphicsPath ();
			rect1.AddRectangle (-50, -50, 100, 100);
			rect1.Transform (m);
			pen.Color = Color.FromRgb (0x00FF00);
			g.DrawPath (pen, rect1);

			var rect2 = new GraphicsPath ();
			rect2.AddRectangle (-5, -5, 10, 10);
			rect2.Transform (m);
			pen.Color = Color.FromRgb (0xFF0000);
			g.FillPath (pen.Color, rect2);
			g.DrawPath (pen, rect2);

			brush.Dispose ();
			pen.Dispose ();
		}

		public void Update ()
		{
			Update (new Rectangle (0, 0, Width, Height));
		}
	}
}

