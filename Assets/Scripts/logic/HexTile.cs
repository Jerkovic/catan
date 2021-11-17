using System;
using UnityEngine;
using System.Collections;

namespace logic
{
    public class HexTile
    {
        private readonly CubicHexCoord _coordinate;
        public Color color;
        
        // UI offsets 
        private const float XOffset = 0.882f;
        private const float ZOffset = 0.764f;

        public HexTile(CubicHexCoord coord, Color col)
        {
            _coordinate = coord;
            color = col;
        }

        public Vector3 ToWorldCoordinates()
        {            
            var xPos = _coordinate._x * XOffset;
            var zPos = _coordinate._z * ZOffset;
			
            if( _coordinate._z > 0 && _coordinate._z % 2 == 1)
            {
                xPos += XOffset / 2f + XOffset * (_coordinate._z / 2);
            }
            if( _coordinate._z > 0 && _coordinate._z % 2 == 0)
            {
                xPos += XOffset * (_coordinate._z / 2);
            }
			
            if( _coordinate._z < 0 &&  (_coordinate._z * -1) % 2 == 1)
            {
                xPos -= XOffset / 2f + XOffset * (-_coordinate._z / 2);
            }
			
            if( _coordinate._z < 0 && (_coordinate._z * -1) % 2 == 0) // even 
            {
                xPos -= XOffset * (-_coordinate._z / 2);
            }

            return new Vector3(xPos, 0, zPos);
        }

        public override int GetHashCode()
        {
            return _coordinate.GetHashCode();
        }

    }
}