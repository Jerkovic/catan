using System;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Catan
{
    public class Game
    {
        private readonly string _gameId;
        private readonly Board _board;
        private readonly List<Player> _players;

        // Snake order
        private IEnumerable<string> _placementTurn;
        private int _turnCounter = 0; 

        public Game()
        {
            _board = new Board(3);
            _players = new List<Player>();
            
            _gameId = GenerateGuid();

            // set some mocked players to the game
            var player1 = new Player(Color.blue, "Robert");
            var player2 = new Player(Color.red, "Sarah");
            var player3 = new Player(Color.yellow, "Vincent");
            var player4 = new Player(Color.magenta, "Victoria");
            _players.Add(player1);
            _players.Add(player2);
            _players.Add(player3);
            _players.Add(player4);
        }

        public string GetGameId()
        {
            return _gameId;
        }

        public void Start()
        {
            // Make up the placing turn order. Snake style
            Debug.Log("Initializing Game " + _gameId);
            _placementTurn = InitPlacementTurn();
            NextTurn();

        }

        private IEnumerable<string> InitPlacementTurn()
        {
            Debug.Log("InitPlacementTurn");
            var ids = _players.Select(p => p.Guid).ToArray();
            var placementOrder = ids.ToList();
            Array.Reverse(ids);
            placementOrder.AddRange(ids);
            return placementOrder.ToList();
        }

        private Player GetPlayerByGuid(string guid)
        {
            return _players.Single(p => p.Guid == guid);
        }

        public void NextTurn()
        {
            // do validation
            Debug.Log(_turnCounter + " of " +_placementTurn.Count());
            if (_turnCounter >= _placementTurn.Count())
            {
                Debug.Log("First phase is over!");
                return;
            }
            var guid = _placementTurn.ElementAt(_turnCounter++);
            Debug.Log("Turn changed to " + guid);
            Events.OnPlayerTurnChanged.Invoke(GetPlayerByGuid(guid));
        }

        public static string GenerateGuid()
        {
            return $"{Guid.NewGuid():N}";
        }

        public Board GetBoard()
        {
            return _board;
        }

        public List<Player> GetPlayers()
        {
            return _players;
        }

        private int RollDices()
        {
            var dice1 = Random.Range(1, 6);
            var dice2 = Random.Range(1, 6);
            var sum = dice1 + dice2;
            Events.OnRollDice.Invoke(sum);
            return sum;
        }
    }
}