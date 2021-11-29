using System;
using UnityEngine;
using System.Collections;
using Catan.Resources;

namespace Catan
{
    public class HexTile
    {
        private readonly CubicHexCoord _coordinate;
        private readonly TileTypeEnum _type;
        private readonly int _chit;

        // UI offsets 
        private const float XOffset = 0.882f;
        private const float ZOffset = 0.764f;

        public HexTile(CubicHexCoord coord, TileTypeEnum type, int chit)
        {
            _coordinate = coord;
            _type = type;
            _chit = chit;
        }

        public TileTypeEnum GetTileType()
        {
            return _type;
        }

        public int GetChit()
        {
            return _chit;
        }

        public ResourceEnum GetResourceType()
        {
            return GetTileType() switch
            {
                TileTypeEnum.HILL => ResourceEnum.BRICK,
                TileTypeEnum.FIELD => ResourceEnum.WHEAT,
                TileTypeEnum.FOREST => ResourceEnum.WOOD,
                TileTypeEnum.PASTURE => ResourceEnum.SHEEP,
                TileTypeEnum.MOUNTAIN => ResourceEnum.ORE,
                TileTypeEnum.DESERT => ResourceEnum.NONE,
                TileTypeEnum.SEA => ResourceEnum.NONE,
                _ => ResourceEnum.NONE
            };
        }

        public Vector3 ToWorldCoordinates()
        {
            var xPos = _coordinate.x * XOffset;
            var zPos = _coordinate.z * ZOffset;

            if (_coordinate.z > 0 && _coordinate.z % 2 == 1)
            {
                xPos += XOffset / 2f + XOffset * (_coordinate.z / 2);
            }

            if (_coordinate.z > 0 && _coordinate.z % 2 == 0)
            {
                xPos += XOffset * (_coordinate.z / 2);
            }

            if (_coordinate.z < 0 && (_coordinate.z * -1) % 2 == 1)
            {
                xPos -= XOffset / 2f + XOffset * (-_coordinate.z / 2);
            }

            if (_coordinate.z < 0 && (_coordinate.z * -1) % 2 == 0) // even 
            {
                xPos -= XOffset * (-_coordinate.z / 2);
            }

            return new Vector3(xPos, 0, zPos);
        }

        public override int GetHashCode()
        {
            return _coordinate.GetHashCode();
        }
    }
}