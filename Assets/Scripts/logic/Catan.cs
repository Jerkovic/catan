using EventSystem;
using UnityEngine;

namespace logic
{
    public class Catan
    {
        private readonly Board _board;
        
        public Catan()
        {
            _board = new Board(3);
            RollDices(); // test
        }

        public Board GetBoard()
        {
            return _board;
        }
        
        public void RollDices()
        {
            var dice1 = Random.Range(1, 6);
            var dice2 = Random.Range(1, 6);
            var sum = dice1 + dice2;
            Events.OnRollDice.Invoke(sum);
        }
    }
}