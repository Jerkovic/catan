using System;
using UnityEngine;
using System.Collections;

namespace logic
{
    public class HexTile
    {
        public CubicHexCoord coordinate;
        public Color color;

        public HexTile(CubicHexCoord coord, Color col)
        {
            coordinate = coord;
            color = col;
        }

        public int getId()
        {
            return coordinate.GetHashCode();
        }

    }
}