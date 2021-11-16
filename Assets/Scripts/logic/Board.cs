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
            CubicHexCoord[] board = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), radius);
            CubicHexCoord[] island = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), radius-1);
            CubicHexCoord center = new CubicHexCoord(0, 0, 0);
            CubicHexCoord[] water = CubicHexCoord.Ring(center, radius);
            
            // test
            //  CornerDirectionEnum.SE            
            var east = center.Neighbor(DirectionEnum.E).GetHashCode();
            var southEast = center.Neighbor(DirectionEnum.SE).GetHashCode();
                                                
            tiles = new List<HexTile>();
            
            foreach (var coordinate in board)
            {                
                var color = Color.cyan;
                if (Array.IndexOf(water, coordinate) > -1)
                {
                    color = Color.blue;
                }
                tiles.Add(new HexTile(coordinate, color));
            }                                    
        }

        public HexTile getTileByHashCode(int hashCode)
        {
            return tiles.Single(tile => tile.GetHashCode() == hashCode);
        }
    }
}
