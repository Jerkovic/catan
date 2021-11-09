using System;
using System.Collections.Generic;
using logic.development;
using UnityEngine;

namespace logic
{
    public class Board
    {
        public readonly List<HexTile> tiles;
        
        // List<Corner> 
        
        // Class Corner -  tiles[]

        public Board(int radius)
        {
            CubicHexCoord[] board = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), radius);
            CubicHexCoord[] island = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), radius-1);
            CubicHexCoord center = new CubicHexCoord(0, 0, 0);
            CubicHexCoord[] water = CubicHexCoord.Ring(center, radius);
            
            // test
            //  CornerDirectionEnum.SE            
            var east = center.Neighbor(DirectionEnum.E).GetHashCode();
            var southEast = center.Neighbor(DirectionEnum.SE).GetHashCode();
            Debug.Log(east);
            Debug.Log(southEast);
            
            /* Find the triangle value between the center points of the tiles.
             that will be be position of the crossroad
             centerX = (tri[0].x + tri[1].x + tri[2].x) / 3;
             centerY = (tri[0].y + tri[1].y + tri[2].y) / 3;
             */
                        
            tiles = new List<HexTile>();
            
            foreach (var coord in board)
            {                
                var color = Color.green;
                if (Array.IndexOf(water, coord) > -1)
                {
                    color = Color.blue;
                }
                tiles.Add(new HexTile(coord, color));
            }                                    
        }

        public void BuildCorners()
        {
            // A Corner has connection to 3 tiles
            
            // A Island Tile has  6 corners.
        }
        
    }
}
