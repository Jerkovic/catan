using System;
using UnityEngine;
using System.Collections;

namespace logic
{
    public class HexTile
    {
        public CubicHexCoord coordinate;
        public Color color;
        
        // 6 corners

        public HexTile(CubicHexCoord coord, Color col)
        {
            coordinate = coord;
            color = col;

            // Get Corners ---
            //  CornerDirectionEnum.SE            
            coord.Neighbor(DirectionEnum.E);
            coord.Neighbor(DirectionEnum.SE);
            
            //  CornerDirectionEnum.S
            coord.Neighbor(DirectionEnum.SE);
            coord.Neighbor(DirectionEnum.SW);
            
            // CornerDirectionEnum.SW
            coord.Neighbor(DirectionEnum.SW);
            coord.Neighbor(DirectionEnum.W);
            
            // CornerDirectionEnum.NW
            coord.Neighbor(DirectionEnum.W);
            coord.Neighbor(DirectionEnum.NW);
            
            // CornerDirectionEnum.N
            coord.Neighbor(DirectionEnum.NW);
            coord.Neighbor(DirectionEnum.NE);
            
            // CornerDirectionEnum.NE
            coord.Neighbor(DirectionEnum.NE);
            coord.Neighbor(DirectionEnum.E);
        }

        public int getId()
        {
            return coordinate.GetHashCode();
        }

    }
}