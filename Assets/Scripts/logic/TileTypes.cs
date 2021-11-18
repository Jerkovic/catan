using System;
using System.Collections.Generic;
using System.Text;

namespace logic
{
    public class TileTypes
    {
        private static readonly int[] TileCounts = {3, 4, 3, 4, 4, 1}; //  mountain,pasture,hill,field,forest,desert
        private readonly List<int> _tiles = new List<int>();
        private readonly Random _random;

        public TileTypes()
        {
            _random = new Random();
            for (var i = 0; i < TileCounts.Length; i++)
            {
                for (var j = 0; j < TileCounts[i]; j++)
                {
                    _tiles.Add(i);
                }
            }
        }

        public int RandomNextTile()
        {
            var index = _random.Next(_tiles.Count);
            var tile = _tiles[index];
            _tiles.RemoveAt(index);
            return tile;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var card in _tiles)
            {
                sb.Append(card);
            }

            return sb.ToString();
        }
    }
}