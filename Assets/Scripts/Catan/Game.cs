using System.Collections.Generic;
using EventSystem;
using UnityEngine;

namespace Catan
{
    public class Game
    {
        private readonly Board _board;
        private readonly List<Player> _players;

        public Game()
        {
            _board = new Board(3);
            _players = new List<Player>();
            
            // set some players to the game
            var player1 = new Player(Color.blue, "Robert");
            var player2 = new Player(Color.red, "Sarah");
            var player3 = new Player(Color.yellow, "Vincent");
            var player4 = new Player(Color.magenta, "Victoria");
            _players.Add(player1);
            _players.Add(player2);
            _players.Add(player3);
            _players.Add(player4);
            
            // The logic of the placement round
            // take random player to start 
            // if player 2 starts with 3 players the order is: 2,3,1,1,3,2
            // 1 placement round is 1 village and 1 adjacent road.
        }

        public Board GetBoard()
        {
            return _board;
        }
        
        public List<Player> GetPlayers()
        {
            return _players;
        }

        private void RollDices()
        {
            var dice1 = Random.Range(1, 6);
            var dice2 = Random.Range(1, 6);
            var sum = dice1 + dice2;
            Events.OnRollDice.Invoke(sum);
        }
    }
}