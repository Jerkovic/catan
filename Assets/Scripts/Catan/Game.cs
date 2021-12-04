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
        private int _turnCounter;
        private string _turnPlayerGuid;

        private GamePhaseStateEnum _gamePhaseState;

        public Game()
        {
            _board = new Board(3);
            _players = new List<Player>();
            _gameId = GenerateGuid();
            _gamePhaseState = GamePhaseStateEnum.PLACE_FIRST_SETTLEMENT_ROAD;

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
            Events.OnGameStarted.Invoke(GetPlayers());

            Debug.Log("Initializing Game " + _gameId);
            GetBoard().SetRobberDesert();
            _placementTurn = InitPlacementTurn();
            NextTurn();
        }

        private IEnumerable<string> InitPlacementTurn()
        {
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
            Debug.Log(_turnCounter + " of " + _placementTurn.Count());
            if (_turnCounter == _placementTurn.Count() / 2)
            {
                Debug.Log("First phase is over! Place second settlement and road");
                _gamePhaseState = GamePhaseStateEnum.PLACE_SECOND_SETTLEMENT_ROAD;
            }

            if (_turnCounter >= _placementTurn.Count())
            {
                Debug.Log("Second phase is over! Enter roll/build/trade phase");
                _gamePhaseState = GamePhaseStateEnum.ROLL_BUILD_TRADE;
                return;
            }

            var guid = _placementTurn.ElementAt(_turnCounter++);
            Debug.Log("Turn changed to " + guid + " Phase" + _gamePhaseState);
            _turnPlayerGuid = guid;
            Events.OnPlayerTurnChanged.Invoke(GetPlayerByGuid(guid));
        }

        public string GetTurnPlayerGuid()
        {
            return _turnPlayerGuid;
        }

        public static string GenerateGuid()
        {
            return $"{Guid.NewGuid():N}";
        }

        public Board GetBoard()
        {
            return _board;
        }

        private List<Player> GetPlayers()
        {
            return _players;
        }

        public int RollDices()
        {
            var dice1 = Random.Range(1, 6);
            var dice2 = Random.Range(1, 6);
            var diceSum = dice1 + dice2;
            Events.OnRollDice.Invoke(diceSum);

            ProduceResources(diceSum);

            return diceSum;
        }

        private void ProduceResources(int diceSum)
        {
            var producingTiles = _board.GetTiles().Where((tile) => tile.GetChit() == diceSum).ToList();
            TilesProduce(producingTiles);
        }

        private void TilesProduce(IEnumerable<HexTile> producingTiles)
        {
            var producingTileCorners = producingTiles.Select((tile) =>
                new
                {
                    Tile = tile,
                    Corners = GetBoard().GetCornersByTile(tile.GetHashCode())
                        .Where(c => c.GetState() != CornerStateEnum.EMPTY)
                });

            foreach (var item in producingTileCorners)
            {
                foreach (var corner in item.Corners)
                {
                    var player = GetPlayerByGuid(corner.GetPlayerGuid());
                    player.AddResource(item.Tile.GetResourceType(), (int)corner.GetState());
                    Debug.Log(corner.GetPlayerGuid() + " got " + item.Tile.GetResourceType() + " amount: " +
                              corner.GetState());
                    Events.OnResourcesUpdate.Invoke(player);
                }
            }
        }

        public void BuildSettlementAtCorner(int hashCode)
        {
            var player = GetPlayerByGuid(_turnPlayerGuid);
            var corner = GetBoard().GetCornerByHashCode(hashCode);
            var edges = GetBoard().GetEdgesByCorner(corner.GetHashCode());
            var corners = edges.Select((e) => e.GetAdjacentCorner(corner));
            var adjacentOccupiedCorners = corners.Where((c) => c != null && c.GetState() != CornerStateEnum.EMPTY);
            
            if (adjacentOccupiedCorners.Any())
            {
                Debug.Log("Too close to other building");
                return;
            }

            if (corner.PlaceSettlement(_turnPlayerGuid))
            {
                if (_gamePhaseState == GamePhaseStateEnum.PLACE_SECOND_SETTLEMENT_ROAD)
                {
                    // Second placement turn, will produce stuff for built settlement adjacent tiles
                    var tiles = GetBoard().GetTilesByCorner(corner);
                    TilesProduce(tiles);
                }
                Events.OnSettlementBuilt.Invoke(new SettlementBuilt(player, corner));
                return;
            }
            
            // Events.OnError.Invoke("Cannot build settlement at this corner");
            Debug.Log("Cannot build settlement at this corner");
        }

        public void UpgradeSettlementToCityAtCorner(int hashCode)
        {
            var corner = GetBoard().GetCornerByHashCode(hashCode);

            if (corner.PlaceCity(_turnPlayerGuid))
            {
                var player = GetPlayerByGuid(_turnPlayerGuid);
                Events.OnSettlementUpgradeToCity.Invoke(new SettlementBuilt(player, corner));
            }
            else
            {
                Debug.Log("Cannot upgrade settlement to city!");
            }
        }

        public void BuildRoadAtEdge(int hashCode)
        {
            if (_gamePhaseState == GamePhaseStateEnum.PLACE_FIRST_SETTLEMENT_ROAD)
            {
                // Has to be connected to the building existing
            }
            if (_gamePhaseState == GamePhaseStateEnum.PLACE_SECOND_SETTLEMENT_ROAD)
            {
                // Has to be connected to the settlement built in this turn
            }
            // Todo: A road must always be connected to another road edge owned by the player or connected to corner with a building owned by the player.
            var edge = GetBoard().GetEdgeByHashCode(hashCode);
            var ownedAdjacentCorners = edge.GetCorners().Where((c) => c.OwnedByPlayerGuid(_turnPlayerGuid));
            if (ownedAdjacentCorners.Any())
            {
                if (edge.PlaceRoad(_turnPlayerGuid))
                {
                    var player = GetPlayerByGuid(_turnPlayerGuid);
                    Events.OnRoadBuilt.Invoke(new RoadBuilt(player, edge));
                    return;
                }
            }

            // // Events.OnError.Invoke("Cannot build settlement at this corner");
            Debug.Log("Cannot build road at this edge");
        }
    }
}