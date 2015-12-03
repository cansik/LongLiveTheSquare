using System;

namespace Efalg5GeometrischeAlgo
{
	public class Line2d
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
	}
}

