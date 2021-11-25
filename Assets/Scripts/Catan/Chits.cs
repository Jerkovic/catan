using System;
using System.Collections.Generic;
using System.Text;

namespace Catan
{
    public class Chits
    {
        private readonly List<int> _chits = new List<int>
            {2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12};

        private readonly Random _random;

        public Chits()
        {
            _random = new Random();
        }

        public int RandomNextChit()
        {
            var index = _random.Next(_chits.Count);
            var chit = _chits[index];
            _chits.RemoveAt(index);
            return chit;
        }
    }
}