using System;

namespace logic
{	
	public struct CubicHexCoord
	{
		public int _x;
		public int _y;
		public int _z;

		private static readonly CubicHexCoord[] DIAGONALS = {
			new CubicHexCoord(  1, -2,  1 ),
			new CubicHexCoord( -1, -1,  2 ),
			new CubicHexCoord( -2,  1,  1 ),
			new CubicHexCoord( -1,  2, -1 ),
			new CubicHexCoord(  1,  1, -2 ),
			new CubicHexCoord(  2, -1, -1 )
		};

		private static readonly CubicHexCoord[] DIRECTIONS = {
			new CubicHexCoord(  1, -1,  0 ),
			new CubicHexCoord(  0, -1,  1 ),
			new CubicHexCoord( -1,  0,  1 ),
			new CubicHexCoord( -1,  1,  0 ),
			new CubicHexCoord(  0,  1, -1 ),
			new CubicHexCoord(  1,  0, -1 )
		};
		
		/// <summary>
		/// Create a new CubicHexCoord given the coordinates x, y and z.
		/// </summary>
		/// <param name="x">The position on the x-axis in cube-space.</param>
		/// <param name="y">The position on the y-axis in cube-space.</param>
		/// <param name="z">The position on the z-axis in cube-space.</param>
		public CubicHexCoord( int x, int y, int z ) 
		{
			this._x = x;
			this._y = y;
			this._z = z;
		}
		

		#region Operator Overloads
		
		/// <summary>
		/// Add 2 CubicHexCoords together and return the result.
		/// </summary>
		/// <param name="lhs">The CubicHexCoord on the left-hand side of the + sign.</param>
		/// <param name="rhs">The CubicHexCoord on the right-hand side of the + sign.</param>
		/// <returns>A new CubicHexCoord representing the sum of the inputs.</returns>
		public static CubicHexCoord operator +( CubicHexCoord lhs, CubicHexCoord rhs ) 
		{
			int x = lhs._x + rhs._x;
			int y = lhs._y + rhs._y;
			int z = lhs._z + rhs._z;

			return new CubicHexCoord( x, y, z );
		}

		
		/// <summary>
		/// Subtract 1 CubicHexCoord from another and return the result.
		/// </summary>
		/// <param name="lhs">The CubicHexCoord on the left-hand side of the - sign.</param>
		/// <param name="rhs">The CubicHexCoord on the right-hand side of the - sign.</param>
		/// <returns>A new CubicHexCoord representing the difference of the inputs.</returns>
		public static CubicHexCoord operator -( CubicHexCoord lhs, CubicHexCoord rhs ) 
		{
			int x = lhs._x - rhs._x;
			int y = lhs._y - rhs._y;
			int z = lhs._z - rhs._z;

			return new CubicHexCoord( x, y, z );
		}

		
		/// <summary>
		/// Check if 2 CubicHexCoords represent the same hex on the grid.
		/// </summary>
		/// <param name="lhs">The CubicHexCoord on the left-hand side of the == sign.</param>
		/// <param name="rhs">The CubicHexCoord on the right-hand side of the == sign.</param>
		/// <returns>A bool representing whether or not the CubicHexCoords are equal.</returns>
		public static bool operator ==( CubicHexCoord lhs, CubicHexCoord rhs ) 
		{
			return ( lhs._x == rhs._x ) && ( lhs._y == rhs._y ) && ( lhs._z == rhs._z );
		}

		
		/// <summary>
		/// Check if 2 CubicHexCoords represent the different hexes on the grid.
		/// </summary>
		/// <param name="lhs">The CubicHexCoord on the left-hand side of the != sign.</param>
		/// <param name="rhs">The CubicHexCoord on the right-hand side of the != sign.</param>
		/// <returns>A bool representing whether or not the CubicHexCoords are unequal.</returns>
		public static bool operator !=( CubicHexCoord lhs, CubicHexCoord rhs ) 
		{
			return ( lhs._x != rhs._x ) || ( lhs._y != rhs._y ) || ( lhs._z != rhs._z );
		}


		/// <summary>
		/// Check if this CubicHexCoord is equal to an arbitrary object.
		/// </summary>
		/// <returns>Whether or not this CubicHexCoord and the given object are equal.</returns>
		public override
		bool 
		Equals( object obj )
		{
			if ( obj == null )
			{
				return false;
			}            

            if ( GetType() != obj.GetType() )
			{
                return false;
			}  
			
			CubicHexCoord other = (CubicHexCoord)obj;

			return ( this._x == other._x ) && ( this._y == other._y ) && ( this._z == other._z );
		}

		#endregion


		#region Instance Methods
		
		public override string ToString()
		{
			return this._x + ", " + this._y + ", " + this._z;
		}
		
		/// <summary>
		/// Returns an array of CubicHexCoords within the given range from this hex (including 
		/// this hex) in no particular order.
		/// </summary>
		/// <param name="range">The maximum number of grid steps away from this hex that 
		/// CubicHexCoords will be returned for.</param>
		/// <returns>An array of CubicHexCoords within the given range from this hex (including 
		/// this hex) in no particular order.</returns>
		public 
		CubicHexCoord[] 
		AreaAround( int range )
		{
			return CubicHexCoord.Area( this, range );
		}


		/// <summary>
		/// Returns a CubicHexCoord representing the diagonal of this hex in the given diagonal 
		/// direction.
		/// </summary>
		/// <param name="direction">The diagonal direction of the requested neighbor.</param>
		/// <returns>A CubicHexCoord representing the diagonal of this hex in the given diagonal 
		/// direction.</returns>
		public 
		CubicHexCoord 
		Diagonal( DiagonalEnum direction )
		{
			return this + DIAGONALS[ (int)direction ];
		}

		
		/// <summary>
		/// Returns an array of CubicHexCoords representing this hex's diagonals (in clockwise
		/// order).
		/// </summary>
		/// <returns>An array of CubicHexCoords representing this hex's diagonals (in clockwise
		/// order).</returns>
		public 
		CubicHexCoord[] 
		Diagonals()
		{
			return new CubicHexCoord[ 6 ] {
				this + DIAGONALS[ (int)DiagonalEnum.ESE ], 
				this + DIAGONALS[ (int)DiagonalEnum.S   ], 
				this + DIAGONALS[ (int)DiagonalEnum.WSW ], 
				this + DIAGONALS[ (int)DiagonalEnum.WNW ], 
				this + DIAGONALS[ (int)DiagonalEnum.N   ], 
				this + DIAGONALS[ (int)DiagonalEnum.ENE ]
			};
		}

		
		/// <summary>
		/// Returns the minimum number of grid steps to get from this hex to the given hex.
		/// </summary>
		/// <param name="other">Any CubicHexCoord.</param>
		/// <returns>An integer number of grid steps from this hex to the given hex.</returns>
		public 
		int 
		DistanceTo( CubicHexCoord other )
		{
			return CubicHexCoord.Distance( this, other );
		}

		
		/// <summary>
		/// Returns an array of CubicHexCoords that form the straightest path from this hex to the 
		/// given end. The hexes in the line are determined by forming a straight line from start 
		/// to end and linearly interpolating and rounding each of the interpolated points to 
		/// the nearest hex position.
		/// </summary>
		/// <param name="other">The CubicHexCoord representing the last hex in the line.</param>
		/// <returns>An array of CubicHexCoords ordered as a line from start to end.</returns>
		public 
		CubicHexCoord[] 
		LineTo( CubicHexCoord other )
		{
			return CubicHexCoord.Line( this, other );
		}

		
		/// <summary>
		/// Returns a CubicHexCoord representing the neighbor of this hex in the given direction.
		/// </summary>
		/// <param name="direction">The direction of the requested neighbor.</param>
		/// <returns>A CubicHexCoord representing the neighbor of this hex in the given direction.
		/// </returns>
		public 
		CubicHexCoord 
		Neighbor( DirectionEnum direction )
		{
			return this + DIRECTIONS[ (int)direction ];
		}

		
		/// <summary>
		/// Returns an array of CubicHexCoords representing this hex's neighbors (in clockwise
		/// order).
		/// </summary>
		/// <returns>An array of CubicHexCoords representing this hex's neighbors (in clockwise
		/// order).</returns>
		public 
		CubicHexCoord[] 
		Neighbors()
		{
			return new CubicHexCoord[ 6 ] {
				this + DIRECTIONS[ (int)DirectionEnum.E  ], 
				this + DIRECTIONS[ (int)DirectionEnum.SE ], 
				this + DIRECTIONS[ (int)DirectionEnum.SW ], 
				this + DIRECTIONS[ (int)DirectionEnum.W  ], 
				this + DIRECTIONS[ (int)DirectionEnum.NW ], 
				this + DIRECTIONS[ (int)DirectionEnum.NE ]
			};
		}
		
		
		/// <summary>
		/// Returns an array of CubicHexCoords that appear at the given range around this hex. 
		/// The ring begins from the CubicHexCoord range grid steps away from the center, heading 
		/// in the given direction, and encircling the center in clockwise order.
		/// </summary>
		/// <param name="range">The number of grid steps distance away from the center that the 
		/// ring will be.</param>
		/// <param name="startDirection">The direction in which the first CubicHexCoord of the 
		/// ring will appear in.</param>
		/// <returns>An array of CubicHexCoords ordered as a ring.</returns>
		public 
		CubicHexCoord[] 
		RingAround( int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			return CubicHexCoord.Ring( this, range, startDirection );
		}

		
		/// <remarks>THIS IS NOT YET IMPLEMENTED!</remarks>
		/// <summary>
		/// Rotate this CubicHexCoord around the given center by the given rotation (constrained to 
		/// exact 60 degree rotation steps) and return the CubicHexCoord that represents the 
		/// rotated position in the grid.
		/// </summary>
		/// <param name="center">The CubicHexCoord to be rotated around this CubicHexCoord.</param>
		/// <param name="rotation">The direction and magnitude of rotation to be applied (in exact 
		/// 60 degree rotation steps.</param>
		/// <returns>A CubicHexCoord representing the position of the rotated hex on the grid.
		/// </returns>
		public 
		CubicHexCoord 
		RotateAroundOther( CubicHexCoord center, RotationEnum rotation )
		{
			return CubicHexCoord.Rotate( center, this, rotation );
		}

		
		/// <remarks>THIS IS NOT YET IMPLEMENTED!</remarks>
		/// <summary>
		/// Rotate a CubicHexCoord around this CubicHexCoord by the given rotation (constrained to 
		/// exact 60 degree rotation steps) and return the CubicHexCoord that represents the 
		/// rotated position in the grid.
		/// </summary>
		/// <param name="toRotate">The CubicHexCoord to be rotated around this CubicHexCoord.</param>
		/// <param name="rotation">The direction and magnitude of rotation to be applied (in exact 
		/// 60 degree rotation steps.</param>
		/// <returns>A CubicHexCoord representing the position of the rotated hex on the grid.
		/// </returns>
		public 
		CubicHexCoord 
		RotateOtherAround( CubicHexCoord toRotate, RotationEnum rotation )
		{
			return CubicHexCoord.Rotate( this, toRotate, rotation );
		}
		
		/// <summary>
		/// Scale this CubicHexCoord by the given factor, such that the x, y and z values of 
		/// the CubicHexCoord change proprtionally to the factor provided.
		/// </summary>
		/// <param name="factor">A multiplicative factor to scale by.</param>
		/// <returns>A new scaled CubicHexCoord.</returns>
		public 
			CubicHexCoord 
			Scale( float factor )
		{
			return new FloatCubic( this ).Scale( factor ).Round();
		}

				
		
		/// <summary>
		/// Returns an array of CubicHexCoords of a area centering around ths hex and extending in 
		/// every direction up to the given range. The hexes are ordered starting from the center 
		/// and then spiraling outward clockwise beginning from the neighbor in the given 
		/// direction, until the outside ring is complete.
		/// </summary>
		/// <param name="range">The number of grid steps distance away from the center that the 
		/// edge of the spiral will be.</param>
		/// <param name="startDirection">The direction in which the first CubicHexCoord of the 
		/// spiral will appear in.</param>
		/// <returns>An array of CubicHexCoords ordered as a spiral, beginning from the center
		/// and proceeding clockwise until it reaches the outside of the spiral.</returns>
		public 
		CubicHexCoord[] 
		SpiralAroundInward( int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			return CubicHexCoord.SpiralInward( this, range, startDirection );
		}

		
		/// <summary>
		/// Returns an array of CubicHexCoords of a area centering around this hex and extending in 
		/// every direction up to the given range. The hexes are ordered starting from the maximum 
		/// range, in the given direction, spiraling inward in a clockwise direction until the 
		/// center is reached (and is the last element in the array).
		/// </summary>
		/// <param name="range">The number of grid steps distance away from the center that the 
		/// edge of the spiral will be.</param>
		/// <param name="startDirection">The direction in which the first CubicHexCoord of the 
		/// spiral will appear in.</param>
		/// <returns>An array of CubicHexCoords ordered as a spiral, beginning from the outside
		/// and proceeding clockwise until it reaches the center of the spiral.</returns>
		/// <returns></returns>
		public 
		CubicHexCoord[] 
		SpiralAroundOutward( int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			return CubicHexCoord.SpiralOutward( this, range, startDirection );
		}

		#endregion


		#region Static Methods
		
		/// <summary>
		/// Returns an array of CubicHexCoords within the given range from the given center 
		/// (including the center itself) in no particular order.
		/// </summary>
		/// <param name="center">The CubicHexCoord around which the area is formed.</param>
		/// <param name="range">The maximum number of grid steps away from the center that 
		/// CubicHexCoords will be returned for.</param>
		/// <returns>An array of CubicHexCoords within the given range from the given center 
		/// (including the center itself) in no particular order.</returns>
		public static 
		CubicHexCoord[] 
		Area( CubicHexCoord center, int range )
		{
			if ( range < 0 )
			{
				throw new ArgumentOutOfRangeException( "range must be a non-negative integer value." );
			}
			else if ( range == 0 )
			{
				return new CubicHexCoord[ 1 ] { center };
			}

			int arraySize = 1;
			for ( int i = range; i > 0; i-- )
			{
				arraySize += 6 * i;
			}


			CubicHexCoord[] result = new CubicHexCoord[ arraySize ];

			for ( int i = 0, dx = -range; dx <= range; dx++ )
			{
				int dyMinBound = Math.Max( -range, -dx - range );
				int dyMaxBound = Math.Min(  range, -dx + range );

				for ( int dy = dyMinBound; dy <= dyMaxBound; dy++ )
				{
					int dz = -dx - dy;
					result[ i++ ] = center + new CubicHexCoord( dx, dy, dz );
				}
			}

			return result;
		}

		
		/// <summary>
		/// Returns a CubicHexCoord representing the diff between some hex and its diagonal in 
		/// the given diagonal direction.
		/// </summary>
		/// <param name="direction">The diagonal direction to return a diff for.</param>
		/// <returns>A CubicHexCoord representing the diff between some hex and its diagonal in 
		/// the given diagonal direction.</returns>
		public static 
		CubicHexCoord 
		DiagonalDiff( DiagonalEnum direction )
		{
			return DIAGONALS[ (int)direction ];
		}

		
		/// <summary>
		/// Returns a CubicHexCoord representing the diff between some hex and its neighbor in 
		/// the given direction.
		/// </summary>
		/// <param name="direction">The direction to return a diff for.</param>
		/// <returns>A CubicHexCoord representing the diff between some hex and its neighbor in 
		/// the given direction.</returns>
		public static 
		CubicHexCoord 
		DirectionDiff( DirectionEnum direction )
		{
			return DIRECTIONS[ (int)direction ];
		}

				
		/// <summary>
		/// Returns the minimum number of grid steps to get from a to b.
		/// </summary>
		/// <param name="a">Any CubicHexCoord.</param>
		/// <param name="b">Any CubicHexCoord.</param>
		/// <returns>An integer number of grid steps from a to b.</returns>
		public static 
		int 
		Distance( CubicHexCoord a, CubicHexCoord b )
		{
			int dx = Math.Abs( a._x - b._x );
			int dy = Math.Abs( a._y - b._y );
			int dz = Math.Abs( a._z - b._z );

			return Math.Max( Math.Max( dx, dy ), dz );
		}

		
		/// <summary>
		/// Returns an array of CubicHexCoords that form the straightest path from the given start
		/// to the given end. The hexes in the line are determined by forming a straight line from 
		/// start to end and linearly interpolating and rounding each of the interpolated points to 
		/// the nearest hex position.
		/// </summary>
		/// <param name="start">The CubicHexCoord representing the first hex in the line.</param>
		/// <param name="end">The CubicHexCoord representing the last hex in the line.</param>
		/// <returns>An array of CubicHexCoords ordered as a line from start to end.</returns>
		public static 
		CubicHexCoord[] 
		Line( CubicHexCoord start, CubicHexCoord end )
		{
			int distance = CubicHexCoord.Distance( start, end );

			CubicHexCoord[] result = new CubicHexCoord[ distance + 1 ];

			for ( int i = 0; i <= distance; i++ )
			{
				float xLerp = start._x + ( end._x - start._x ) * 1f / distance * i;
				float yLerp = start._y + ( end._y - start._y ) * 1f / distance * i;
				float zLerp = start._z + ( end._z - start._z ) * 1f / distance * i;

				result[ i ] = new FloatCubic( xLerp, yLerp, zLerp ).Round();
			}

			return result;
		}

		
		/// <summary>
		/// Returns an array of CubicHexCoords that appear at the given range around the given 
		/// center hex. The ring begins from the CubicHexCoord range grid steps away from the 
		/// center, heading in the given direction, and encircling the center in clockwise order.
		/// </summary>
		/// <param name="center">The CubicHexCoord around which the ring is formed.</param>
		/// <param name="range">The number of grid steps distance away from the center that the 
		/// ring will be.</param>
		/// <param name="startDirection">The direction in which the first CubicHexCoord of the 
		/// ring will appear in.</param>
		/// <returns>An array of CubicHexCoords ordered as a ring.</returns>
		public static 
		CubicHexCoord[] 
		Ring( CubicHexCoord center, int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			if ( range <= 0 )
			{
				throw new ArgumentOutOfRangeException( "range must be a positive integer value." );
			}
			
			CubicHexCoord[] result = new CubicHexCoord[ 6 * range ];

			CubicHexCoord cube = center + DIRECTIONS[ (int)startDirection ].Scale( range );

			int[] directions = new int[ 6 ];
			for ( int i = 0; i < 6; i++ )
			{
				directions[ i ] = ( (int)startDirection + i ) % 6;
			}

			int index = 0;
			for ( int i = 0; i < 6; i++ )
			{
				int neighborDirection = ( directions[ i ] + 2 ) % 6;
				for ( int j = 0; j < range; j++ )
				{
					result[ index++ ] = cube;
					cube = cube.Neighbor( (DirectionEnum)neighborDirection );
				}
			}

			return result;
		}

		
		/// <remarks>THIS IS NOT YET IMPLEMENTED!</remarks>
		/// <see cref="http://www.redblobgames.com/grids/hexagons/"/>
		/// <summary>
		/// Rotate a CubicHexCoord around the given center by the given rotation (constrained to 
		/// exact 60 degree rotation steps) and return the CubicHexCoord that represents the 
		/// rotated position in the grid.
		/// </summary>
		/// <param name="center">The CubicHexCoord representing the rotation's pivot.</param>
		/// <param name="toRotate">The CubicHexCoord to be rotated around the pivot.</param>
		/// <param name="rotation">The direction and magnitude of rotation to be applied (in exact 
		/// 60 degree rotation steps.</param>
		/// <returns>A CubicHexCoord representing the position of the rotated hex on the grid.
		/// </returns>
		public static 
		CubicHexCoord 
		Rotate( CubicHexCoord center, CubicHexCoord toRotate, RotationEnum rotation )
		{
			throw new NotImplementedException( "Feature not suppored yet!" );
		}

		
		/// <summary>
		/// Returns an array of CubicHexCoords of a area centering around the given center hex and 
		/// extending in every direction up to the given range. The hexes are ordered starting from 
		/// the center and then spiraling outward clockwise beginning from the neighbor in the 
		/// given direction, until the outside ring is complete.
		/// </summary>
		/// <param name="center">The CubicHexCoord around which the spiral is formed.</param>
		/// <param name="range">The number of grid steps distance away from the center that the 
		/// edge of the spiral will be.</param>
		/// <param name="startDirection">The direction in which the first CubicHexCoord of the 
		/// spiral will appear in.</param>
		/// <returns>An array of CubicHexCoords ordered as a spiral, beginning from the center
		/// and proceeding clockwise until it reaches the outside of the spiral.</returns>
		public static 
		CubicHexCoord[] 
		SpiralInward( CubicHexCoord center, int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			if ( range <= 0 )
			{
				throw new ArgumentOutOfRangeException( "range must be a positive integer value." );
			}
			else if ( range == 0 )
			{
				return new CubicHexCoord[ 1 ] { center };
			}

			int arraySize = 1;
			for ( int i = range; i > 0; i-- )
			{
				arraySize += 6 * i;
			}

			CubicHexCoord[] result = new CubicHexCoord[ arraySize ];

			result[ result.Length - 1 ] = center;

			int arrayIndex = result.Length - 1;
			for ( int i = range; i >= 1; i-- ) {
				CubicHexCoord[] ring = CubicHexCoord.Ring( center, i, startDirection );
				arrayIndex -= ring.Length;
				ring.CopyTo( result, arrayIndex );
			}
				
			return result;
		}

		
		/// <summary>
		/// Returns an array of CubicHexCoords of a area centering around the given center hex and 
		/// extending in every direction up to the given range. The hexes are ordered starting from 
		/// the maximum range, in the given direction, spiraling inward in a clockwise direction 
		/// until the center is reached (and is the last element in the array).
		/// </summary>
		/// <param name="center">The CubicHexCoord around which the spiral is formed.</param>
		/// <param name="range">The number of grid steps distance away from the center that the 
		/// edge of the spiral will be.</param>
		/// <param name="startDirection">The direction in which the first CubicHexCoord of the 
		/// spiral will appear in.</param>
		/// <returns>An array of CubicHexCoords ordered as a spiral, beginning from the outside
		/// and proceeding clockwise until it reaches the center of the spiral.</returns>
		public static 
		CubicHexCoord[] 
		SpiralOutward( CubicHexCoord center, int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			if ( range <= 0 )
			{
				throw new ArgumentOutOfRangeException( "range must be a positive integer value." );
			}
			else if ( range == 0 )
			{
				return new CubicHexCoord[ 1 ] { center };
			}

			int arraySize = 1;
			for ( int i = range; i > 0; i-- )
			{
				arraySize += 6 * i;
			}

			CubicHexCoord[] result = new CubicHexCoord[ arraySize ];

			result[ 0 ] = center;

			int arrayIndex = 1;
			for ( int i = 1; i <= range; i++ ) {
				CubicHexCoord[] ring = CubicHexCoord.Ring( center, i, startDirection );
				ring.CopyTo( result, arrayIndex );
				arrayIndex += ring.Length;
			}
				
			return result;
		}

		#endregion
	}
}