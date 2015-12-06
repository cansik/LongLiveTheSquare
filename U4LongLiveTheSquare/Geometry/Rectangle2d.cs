using System;
using Efalg5GeometrischeAlgo;
using Eto.Drawing;

namespace U4LongLiveTheSquare
{
	public class Rectangle2d : IGeometry
	{
		public Vector2d Location { get; set; }

		public Vector2d Size { get; set; }

		public Rectangle2d ()
		{
		}

		public Rectangle2d (Vector2d a, Vector2d c) : this ()
		{
			Location = a;
			Size = c - a;
		}

		public double Area ()
		{
			return Size.X * Size.Y;
		}

		public Vector2d[] Points {
			get {
				return new  [] {
					new Vector2d (Location.X, Location.Y),
					new Vector2d (Location.X + Size.X, Location.Y),
					new Vector2d (Location.X + Size.X, Location.Y + Size.Y),
					new Vector2d (Location.X, Location.Y + Size.Y)
				};
			}
		}

		#region IGeometry implementation

		public Eto.Drawing.GraphicsPath GraphicsPath {
			get {
				var gp = new GraphicsPath ();
				gp.AddRectangle ((float)Location.X, (float)Location.Y, (float)(Size.X), (float)(Size.Y));
				return gp;
			}
		}

		#endregion
	}
}

