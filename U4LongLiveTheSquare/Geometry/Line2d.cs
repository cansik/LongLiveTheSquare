using System;
using U4LongLiveTheSquare;
using Eto.Drawing;

namespace Efalg5GeometrischeAlgo
{
	public class Line2d : IGeometry
	{
		/// <summary>
		/// Position Vector of line.
		/// </summary>
		/// <value>Position Vector</value>
		public Vector2d A{ get; set; }

		/// <summary>
		/// Direction Vector of the line.
		/// B - A
		/// </summary>
		/// <value>The direction.</value>
		public Vector2d R{ get; set; }

		public Line2d (Vector2d position, Vector2d direction)
		{
			A = position;
			R = direction;
		}

		#region IGeometry implementation

		public GraphicsPath GraphicsPath {
			get {
				var gp = new GraphicsPath ();

				var b = A + R;
				var lc = 10000f;
				gp.AddLine ((float)A.X - lc, (float)A.Y - lc, (float)b.X + lc, (float)b.Y + lc);

				return gp;
			}
		}

		#endregion
	}
}

