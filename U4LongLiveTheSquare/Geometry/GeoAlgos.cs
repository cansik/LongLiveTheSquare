using System;
using System.Collections.Generic;
using System.Collections;

namespace Efalg5GeometrischeAlgo
{
	public static class GeoAlgos
	{
		public static bool IsDoubleEqual (double d1, double d2)
		{
			return Math.Abs (d1 - d2) < double.Epsilon;
		}

		public static string DoubleFormat (double d)
		{
			return string.Format ("{0:F2}", d);
		}

		public static IntersectionResult SegmentIntersection2D (Segment2d a, Segment2d b)
		{
			//g
			var oa = a.A;
			var ab = a.R;

			//h
			var oc = b.A;
			var cd = b.R;

			//create linear system
			//lambda
			var l = new Vector2d (ab);

			//my
			var m = new Vector2d (0 - cd.X, 0 - cd.Y);

			//num
			var n = new Vector2d (oc.X - oa.X, oc.Y - oa.Y);

			//solve
			//if det = 0 => both vectors are over each other
			var det = (l.X * m.Y) - (m.X * l.Y);
			var x1 = (n.X * m.Y - (m.X * n.Y)) / det;
			var x2 = (l.X * n.Y - (n.X * l.Y)) / det;

			var s = oc.Add (cd.Multiply (x2));

			return new IntersectionResult (s, x1, x2);
		}

		public struct IntersectionResult
		{
			public readonly Vector2d s;
			public double lambda;
			public double my;

			public bool IsSpecialIntersection ()
			{
				if (my < 0) {
					my = 10;
				}

				if (lambda < 0) {
					lambda = 10;
				}
				
				return my < 1 || lambda < 1;
			}

			public IntersectionResult (Vector2d s, double l, double m)
			{
				this.s = s;
				this.lambda = l;
				this.my = m;
			}
		}

		/// <summary>
		/// Calculates the convex hull with the monotone chain approach.
		/// </summary>
		/// <returns>The chain convex hull.</returns>
		/// <param name="points">Points.</param>
		public static Vector2d[] MonotoneChainConvexHull (Vector2d[] points)
		{
			//sort vectors
			Array.Sort<Vector2d> (points);
			var hullPoints = new Vector2d [2 * points.Length];

			//break if only one point as input
			if (points.Length <= 1)
				return points;

			int pointLength = points.Length;
			int counter = 0;

			//iterate for lowerHull
			for (var i = 0; i < pointLength; ++i) {
				while (counter >= 2 && Cross (hullPoints [counter - 2], 
					       hullPoints [counter - 1],
					       points [i]) <= 0)
					counter--;
				hullPoints [counter++] = points [i];
			}

			//iterate for upperHull
			for (int i = pointLength - 2, j = counter + 1; i >= 0; i--) {
				while (counter >= j && Cross (hullPoints [counter - 2], 
					       hullPoints [counter - 1],
					       points [i]) <= 0)
					counter--;
				hullPoints [counter++] = points [i];
			}

			//remove duplicate start points
			var result = new Vector2d[counter - 1];
			Array.Copy (hullPoints, 0, result, 0, counter - 1);
			return result;
		}

		/// <summary>
		/// Cross the specified o, a and b.
		/// Zero if collinear
		/// Positive if counter-clockwise
		/// Negative if clockwise
		/// </summary>
		/// <param name="o">O.</param>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public static double Cross (Vector2d o, Vector2d a, Vector2d b)
		{
			return (a.X - o.X) * (b.Y - o.Y) - (a.Y - o.Y) * (b.X - o.X);
		}
	}
}

