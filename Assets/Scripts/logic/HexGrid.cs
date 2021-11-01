using System;
using System.Linq;

namespace logic
{
	/// <summary>
	/// A logical hex-grid implementation based on Amit Patel's examples at 
	/// http://www.redblobgames.com/grids/hexagons/. Creating a new hex grid does not create an
	/// array in memory or any other concrete data. It provides a service to query for data such as
	/// what 2D point at which a given hex index appears at or which hex a given 2D point is over.
	///
	/// Coordinate Systems
	/// ==================
	/// 
	/// See http://www.redblobgames.com/grids/hexagons/ for a complete explanation of Axial, Cubic 
	/// and Offset coordinate spaces. This hex grid implementation orients hexes in a pointy-topped 
	/// odd-row offset configuration, and otherwise uses the same axes and conventions as those in 
	/// Amit Patel's examples.
	///
	/// It should be noted that when converting from point to hex space, the x-axis is the 
	/// horizontal axis with values increasing from left to right, and the y-axis is the vertical 
	/// axis with values increasing from top to bottom (POSITIVE Y IS DOWNWARD).
	/// </summary>
    public class HexGrid
    {
		
    }
}