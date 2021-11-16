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
        }

        public void GetCorners(int hashCode)
        {
            
        }

        public HexTile GetTileByHashCode(int hashCode)
        {
            return tiles.Single(tile => tile.GetHashCode() == hashCode);
        }
    }
}
