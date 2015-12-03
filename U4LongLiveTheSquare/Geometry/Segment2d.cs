using System;

namespace Efalg5GeometrischeAlgo
{
	public class Segment2d : Line2d
	{
		/// <summary>
		/// Second Point of Line.
		/// </summary>
		/// <value>The b.</value>
		public Vector2d B{ get; set; }

		public Segment2d (Vector2d point1, Vector2d point2) :
			base (point1, point2 - point1)
		{
			B = point2;
		}

		public double Length ()
		{
			return Math.Abs (A.Distance (B));
		}

		public override string ToString ()
		{
			return string.Format ("[Segment2d: A={0} B={1}]", A, B);
		}
	}
}

