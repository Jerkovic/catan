using System;
using System.Collections.Generic;
using UnityEngine;

namespace logic
{
    public class Board
    {
        public List<HexTile> tiles;
        
        // List<Corner> 
        
        // Class Corner -  tiles[]

        public Board(int radius)
        {
            CubicHexCoord[] board = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), radius);
            CubicHexCoord[] island = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), radius-1);
            CubicHexCoord[] water = CubicHexCoord.Ring(new CubicHexCoord(0, 0, 0), radius);

            tiles = new List<HexTile>();

            var index = 0;
            foreach (CubicHexCoord coord in board)
            {
                CubicHexCoord[] adjacents = coord.Neighbors();
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
