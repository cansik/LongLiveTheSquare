using System;
using System.Linq;
using Efalg5GeometrischeAlgo;
using Eto.Drawing;
using System.Collections.Generic;

namespace U4LongLiveTheSquare
{
	public class Polygon2d : IGeometry
	{
		public List<Vector2d> Points { get; set; } = new List<Vector2d>();

		public Polygon2d ()
		{
		}

		#region IGeometry implementation

		public Eto.Drawing.GraphicsPath GraphicsPath {
			get {
				var gp = new GraphicsPath ();
				gp.AddLines (Points.Select (p => new PointF ((float)p.X, (float)p.Y)).ToArray ());
				gp.CloseFigure ();
				return gp;
			}
		}

		#endregion
	}
}

