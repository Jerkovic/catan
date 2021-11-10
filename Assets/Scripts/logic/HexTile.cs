using System;
using UnityEngine;
using System.Collections;

namespace logic
{
    public class HexTile
    {
        public CubicHexCoord coordinate;
        public Color color;
        
        // Todo: 6 corners 
        
        // UI offsets 
        private const float xOffset = 0.882f;
        private const float zOffset = 0.764f;

        public HexTile(CubicHexCoord coord, Color col)
        {
            coordinate = coord;
            color = col;

            // Get Corners ---
            //  CornerDirectionEnum.SE            
            coordinate.Neighbor(DirectionEnum.E);
            coordinate.Neighbor(DirectionEnum.SE);
            
            //  CornerDirectionEnum.S
            coordinate.Neighbor(DirectionEnum.SE);
            coordinate.Neighbor(DirectionEnum.SW);
            
            // CornerDirectionEnum.SW
            coordinate.Neighbor(DirectionEnum.SW);
            coordinate.Neighbor(DirectionEnum.W);
            
            // CornerDirectionEnum.NW
            coordinate.Neighbor(DirectionEnum.W);
            coordinate.Neighbor(DirectionEnum.NW);
            
            // CornerDirectionEnum.N
            coordinate.Neighbor(DirectionEnum.NW);
            coordinate.Neighbor(DirectionEnum.NE);
            
            // CornerDirectionEnum.NE
            coordinate.Neighbor(DirectionEnum.NE);
            coordinate.Neighbor(DirectionEnum.E);
        }

        public Vector3 ToWorldCoordinates()
        {            
            var xPos = coordinate._x * xOffset;
            var zPos = coordinate._z * zOffset;
			
            if( coordinate._z > 0 && coordinate._z % 2 == 1)
            {
                xPos += xOffset / 2f + xOffset * (coordinate._z / 2);
            }
            if( coordinate._z > 0 && coordinate._z % 2 == 0)
            {
                xPos += xOffset * (coordinate._z / 2);
            }
			
            if( coordinate._z < 0 &&  (coordinate._z * -1) % 2 == 1)
            {
                xPos -= xOffset / 2f + xOffset * (-coordinate._z / 2);
            }
			
            if( coordinate._z < 0 && (coordinate._z * -1) % 2 == 0) // even 
            {
                xPos -= xOffset * (-coordinate._z / 2);
            }

            return new Vector3(xPos, 0, zPos);
        }

        public int getId()
        {
            return coordinate.GetHashCode();
        }

    }
}