using System;
using System.Linq;
using Efalg5GeometrischeAlgo;
using System.Threading.Tasks;

namespace U4LongLiveTheSquare
{
	public static class MinimalBoundingBox
	{
		static Line2d xAxis = new Line2d (new Vector2d (0, 0), new Vector2d (1, 0));

		public static Polygon2d Calculate (Vector2d[] points)
		{
			//calculate the convex hull
			var hullPoints = GeoAlgos.MonotoneChainConvexHull (points);

			//check if no bounding box available
			if (hullPoints.Length <= 1)
				return new Polygon2d{ Points = hullPoints.ToList () };

			Rectangle2d minBox = null;
			var minAngle = 0d;

			//foreach edge of the convex hull
			for (var i = 0; i < hullPoints.Length; i++) {
				var nextIndex = i + 1;

				var current = hullPoints [i];
				var next = hullPoints [nextIndex % hullPoints.Length];

				var segment = new Segment2d (current, next);

				//min / max points
				var top = double.MinValue;
				var bottom = double.MaxValue;
				var left = double.MaxValue;
				var right = double.MinValue;

				//get angle of segment to x axis
				var angle = AngleToXAxis (segment);

				//rotate every point and get min and max values for each direction
				foreach (var p in hullPoints) {
					var rotatedPoint = RotateToXAxis (p, angle);

					top = Math.Max (top, rotatedPoint.Y);
					bottom = Math.Min (bottom, rotatedPoint.Y);

					left = Math.Min (left, rotatedPoint.X);
					right = Math.Max (right, rotatedPoint.X);
				}

				//create axis aligned bounding box
				var box = new Rectangle2d (new Vector2d (left, bottom), new Vector2d (right, top));

				if (minBox == null || minBox.Area () > box.Area ()) {
					minBox = box;
					minAngle = angle;
				}
			}

			//rotate axis algined box back
			var minimalBoundingBox = new Polygon2d {
				Points = minBox.Points.Select (p => RotateToXAxis (p, -minAngle)).ToList ()
			};

			return minimalBoundingBox;
		}

		static double AngleToXAxis (Segment2d s)
		{
			var delta = s.A - s.B;
			return Math.Atan (delta.Y / delta.X);
		}

		static Vector2d RotateToXAxis (Vector2d v, double angle)
		{
			//newX = centerX + (point2x-centerX)*Math.cos(x) - (point2y-centerY)*Math.sin(x);
			//newY = centerY + (point2x-centerX)*Math.sin(x) + (point2y-centerY)*Math.cos(x);
			double newX = v.X * Math.Cos (angle) - v.Y * Math.Sin (angle);
			double newY = v.X * Math.Sin (angle) + v.Y * Math.Cos (angle);

			return new Vector2d (newX, newY);
		}
	}
}

