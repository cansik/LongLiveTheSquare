using System;
using U4LongLiveTheSquare;
using Eto.Drawing;

namespace Efalg5GeometrischeAlgo
{
	public class Vector2d : IComparable, IGeometry
	{
		#if DEBUG
		private static int VECTOR_NR = 0;

		public string Name { get; set; }
		#endif

		public double X { get; set; }

		public double Y { get; set; }

		public Vector2d ()
		{
			#if DEBUG
			Name = char.ConvertFromUtf32 (65 + (VECTOR_NR % 26));

			if (VECTOR_NR / 26 != 0)
				Name += VECTOR_NR / 26;

			VECTOR_NR++;
			#endif
		}

		public Vector2d (Vector2d v) : this ()
		{
			this.X = v.X;
			this.Y = v.Y;
		}

		public Vector2d (double x, double y) : this ()
		{
			this.X = x;
			this.Y = y;
		}

		public double this [int i] {
			get {
				if (i == 0)
					return X;
				else
					return Y;
			}
			set {
				if (i == 0)
					X = value;
				else
					Y = value;
			}
		}

		public Vector2d Copy ()
		{
			return new Vector2d (X, Y);
		}

		public override string ToString ()
		{
			#if DEBUG
			return string.Format ("Vector ({0}) X={1}, Y={2}", Name, X, Y);
			#endif

			return string.Format ("X={0}, Y={1}", X, Y);
		}

		public override bool Equals (object obj)
		{
			var v = obj as Vector2d;
			if (v == null)
				return false;

			var result = true;
			if (GeoAlgos.IsDoubleEqual (X, v.X)
			    && GeoAlgos.IsDoubleEqual (Y, v.Y))
				result = false;

			return result;
		}

		public override int GetHashCode ()
		{
			var result = 1;
			result = 31 * result + BitConverter.ToInt32 (BitConverter.GetBytes (this.X), 0);
			result = 31 * result + BitConverter.ToInt32 (BitConverter.GetBytes (this.Y), 0);
			return result;
		}

		// math
		public Vector2d Add (Vector2d v)
		{
			return new Vector2d (X + v.X, Y + v.Y);
		}

		public Vector2d Subtract (Vector2d v)
		{
			return new Vector2d (X - v.X, Y - v.Y);
		}

		public Vector2d Multiply (double s)
		{
			return new Vector2d (X * s, Y * s);
		}

		public Vector2d Divide (double s)
		{
			return new Vector2d (X / s, Y / s);
		}

		public double Length ()
		{
			var sum = X * X + Y * Y;
			return (double)Math.Sqrt (sum);
		}

		public double Dot (Vector2d v)
		{
			return X * v.X + Y * v.Y;
		}

		public double Cross (Vector2d v)
		{
			return X * v.Y - Y * v.X;
		}

		public double Distance (Vector2d v)
		{
			var dx = X - v.X;
			var dy = Y - v.Y;
			return (double)Math.Sqrt (dx * dx + dy * dy);
		}

		public double Coincidence (Vector2d v)
		{
			return (X - v.X) * (X - v.X) + (Y - v.Y) * (Y - v.Y);
		}

		public Vector2d Normalize ()
		{
			var m = Length ();
			if (Math.Abs (m) > double.Epsilon && Math.Abs (m - 1d) > double.Epsilon) {
				return Divide (m);
			}
			return new Vector2d (this);
		}

		public Vector2d Pow (double power = 2)
		{
			return new Vector2d (Math.Pow (X, power), Math.Pow (Y, power));
		}

		// operation overloading
		public static Vector2d operator + (Vector2d v1, Vector2d v2)
		{
			return v1.Add (v2);
		}

		public static Vector2d operator - (Vector2d v1, Vector2d v2)
		{
			return v1.Subtract (v2);
		}

		public static Vector2d operator * (Vector2d v1, double s)
		{
			return v1.Multiply (s);
		}

		public static Vector2d operator / (Vector2d v1, double s)
		{
			return v1.Divide (s);
		}

		public static double operator * (Vector2d v1, Vector2d v2)
		{
			return v1.Dot (v2);
		}

		#region IGeogebraObject implementation

		#endregion

		#region IComparable implementation

		public int CompareTo (object obj)
		{
			var v = obj as Vector2d;
			if (v == null)
				throw new ArgumentException ();

			if (GeoAlgos.IsDoubleEqual (X, v.X)) {
				//check y
				if (GeoAlgos.IsDoubleEqual (Y, v.Y))
					return 0;

				if (Y < v.Y) {
					return -1;
				} else {
					return 1;
				}
			}

			if (X < v.X) {
				return -1;
			} else {
				return 1;
			}
		}

		#endregion

		#region IGeometry implementation

		public GraphicsPath GraphicsPath {
			get {
				var size = 1f;
				var gp = new GraphicsPath ();
				gp.AddEllipse ((float)X - size / 2, (float)Y - size / 2, size, size);
				return gp;
			}

		#endregion
		}
	}
}

