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

		public Color ClearColor { get; set; } = Colors.Black;

		public Color ForeGroundColor { get; set; } = Colors.Gray;

		public Color ForeGroundFillColor { get; set; } = Colors.White;

		public Color GridColor { get; set; } = Colors.White;

		public float ScaleFactor { get; set; } = 8;

		public PointF TransformationDelta { get; set; } = new PointF(0, 0);

		public IMatrix ProjectionMatrix {
			get {
				//scaling / transformation / rotation
				var m = Matrix.Create ();

				//translate xy
				m.Translate (Center.X, Center.Y);

				//scale
				m.Scale (ScaleFactor);

				//flip coordination system to cartesian coordinate system
				m.Yy *= -1;

				return m;
			}
		}

		public PointF Center {
			get {
				var centerX = Width / 2f + TransformationDelta.X;
				var centerY = Height / 2f + TransformationDelta.Y; 
				return new PointF (centerX, centerY);
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

			//utils
			var brush = new SolidBrush (ForeGroundFillColor);
			var pen = new Pen (ForeGroundColor, 1);
			var gridPen = new Pen (GridColor, 1);
			var m = ProjectionMatrix;

			//background gradient
			var bg = new RadialGradientBrush (
				         Colors.WhiteSmoke, 
				         Colors.LightBlue, 
				         Center, 
				         Center, 
				         new SizeF (Width * 0.8f, Width * 0.8f));

			//clear image
			g.Clear (ClearColor);
			g.FillRectangle (bg, 0, 0, Width, Height);
				
			//generate paths
			var path = new GraphicsPath ();
			foreach (var geometry in Geometries)
				path.AddPath (geometry.GraphicsPath);

			//draw coordinate system
			g.DrawPath (gridPen, GetCoordinateSystem ());

			//draw paths
			if (!path.IsEmpty) {
				path.Transform (m);

				//draw
				g.FillPath (brush, path);
				g.DrawPath (pen, path);
			}
				
			brush.Dispose ();
			pen.Dispose ();
		}

		GraphicsPath GetCoordinateSystem ()
		{
			var gp = new GraphicsPath ();

			//add axis
			gp.AddLine (-Width, 0, Width, 0);
			gp.AddLine (0, -Height, 0, +Height);

			gp.Transform (ProjectionMatrix);
			return gp;
		}

		public void Update ()
		{
			Update (new Rectangle (0, 0, Width, Height));
		}
	}
}

