using System;
using System.Collections.Generic;
using System.Linq;
using logic.development;
using UnityEngine;

namespace logic
{
    public class Board
    {
        public readonly List<HexTile> tiles;
        public List<Corner> corners;
        public List<Edge> edges;
                
        public Board(int radius)
        {
            var center = new CubicHexCoord(0, 0, 0);
            CubicHexCoord[] board = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), radius);                        
            CubicHexCoord[] water = CubicHexCoord.Ring(center, radius);
                                                            
            tiles = new List<HexTile>();
            
            foreach (var coordinate in board)
            {                
                var color = Color.green;
                if (Array.IndexOf(water, coordinate) > -1)
                {
                    color = Color.blue;
                }
                tiles.Add(new HexTile(coordinate, color));
            }

            GenerateCorners();
            GenerateEdges();
        }

        private void GenerateEdges()
        {
	        // // An edge connects to corners. Total edges #72
	        edges = new List<Edge>();
	        
	        // We start by defining the center edges
	        // Can we render roads for it
	        AddEdge(621001, 619967); // = \
	        // AddEdge(0, 1); // = I
	        // AddEdge(0, 0); // = /
        }

        private void GenerateCorners()
        {
	        corners = new List<Corner>();
	        // corner placement center tile 
			AddCorner(206839, 207345, 206817);
			AddCorner(206839, 206817, 206311);
			AddCorner(206839, 206311, 206333);
			AddCorner(206839, 206333, 206861);
			AddCorner(206839, 206861, 207367);
			AddCorner(206839, 207367, 207345);
			
			// next ring total #18
			AddCorner(206817, 207345, 207323);
			AddCorner(206817, 207323, 206795);
			AddCorner(206817, 206795, 206289);		
			AddCorner(206817, 206289, 206311);		
			AddCorner(206311, 206289, 205783);
			AddCorner(206311, 205783, 205805);
			AddCorner(206311, 206333, 205805);
			AddCorner(206333, 205805, 205827);
			AddCorner(206333, 205827, 206355);
			AddCorner(206333, 206355, 206861);		
			AddCorner(206861, 206355, 206883);
			AddCorner(206861, 206883, 207389);		
			AddCorner(206861, 207367, 207389);		
			AddCorner(207367, 207389, 207895);
			AddCorner(207367, 207895, 207873);
			AddCorner(207367, 207873, 207345);
			AddCorner(207345, 207873, 207851);
			AddCorner(207345, 207851, 207323);		
			// End 2:nd ring  
			
			// Start 3 ring # of corners 30
			AddCorner(206795, 207323, 207301); 
			AddCorner(206795, 207301, 206773); 
			AddCorner(206267, 206795, 206773);
			AddCorner(206289, 206795, 206267);
			AddCorner(205761, 206289, 206267);
			AddCorner(205783, 205761, 206289);
			AddCorner(205255, 205783, 205761);
			AddCorner(205277, 205783, 205255);
			AddCorner(205805, 205277, 205783); 
			AddCorner(205299, 205805, 205277);
			AddCorner(205299, 205805, 205827); 
			AddCorner(205321, 205827, 205299); 
			AddCorner(205321, 205827, 205849); 		
			AddCorner(206355, 205827, 205849); 
			AddCorner(206355, 205849, 206377); 
			AddCorner(206355, 206377, 206883);
			AddCorner(206883, 206377, 206905);
			AddCorner(206883, 207411, 206905);			
			AddCorner(206883, 207411, 207389); 
			AddCorner(207389, 207917, 207411);
			AddCorner(207389, 207917, 207895);
			AddCorner(207895, 208423, 207917);		
			AddCorner(207895, 208423, 208401);
			AddCorner(207895, 207873, 208401);
			AddCorner(207873, 208401, 208379);
			AddCorner(207873, 207851, 208379);
			AddCorner(207851, 208379, 208357); 
			AddCorner(207851, 208357, 207829);
			AddCorner(207323, 207851, 207829);
			AddCorner(207323, 207829, 207301);
        }
        
        private void AddCorner(int hex1, int hex2, int hex3)
        {
	        corners.Add(new Corner(hex1, hex2, hex3));
        }

        private void AddEdge(int cornerHash1, int cornerHash2)
        {
	        var corner1 = GetCornerByHashCode(cornerHash1);
	        var corner2 = GetCornerByHashCode(cornerHash2);
	        edges.Add(new Edge(corner1, corner2));
        }

        private Corner GetCornerByHashCode(int hashCode)
        {
	        return corners.Single(corner => corner.GetHashCode() == hashCode);
        }

        public HexTile GetTileByHashCode(int hashCode)
        {
            return tiles.Single(tile => tile.GetHashCode() == hashCode);
        }
    }
}
