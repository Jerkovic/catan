using System;
using System.Collections.Generic;
using System.Linq;
using Catan.DevelopmentCards;
using Catan.Resources;
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
        private readonly Deck _devCardDeck;

        // Snake order
        private IEnumerable<string> _placementTurn;
        private int _turnCounter;
        private string _turnPlayerGuid;

        private GamePhaseStateEnum _gamePhaseState;
        private Corner _lastSettlementCorner;

        public Game()
        {
            _board = new Board(3);
            _players = new List<Player>();
            _gameId = GenerateGuid();
            _devCardDeck = new Deck();
            _gamePhaseState = GamePhaseStateEnum.PLACE_FIRST_SETTLEMENT_ROAD;

            // set some mocked players to the game
            var player1 = new Player(Color.blue, "Robert");
            var player2 = new Player(Color.red, "Edvin");
            var player3 = new Player(Color.yellow, "Vincent");
            var player4 = new Player(Color.magenta, "Victoria");
            _players.Add(player1);
            _players.Add(player2);
            _players.Add(player3);
            _players.Add(player4);
        }

        public GamePhaseStateEnum GetGameState()
        {
            return _gamePhaseState;
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

        // Move these to top
        private int _currentTurnSettlementCount = 0;
        private int _currentTurnRoadCount = 0;

        public void NextTurn()
        {
            _currentTurnRoadCount = 0;
            _currentTurnSettlementCount = 0;

            if (_gamePhaseState != GamePhaseStateEnum.ROLL_BUILD_TRADE)
            {
                if (_turnCounter == _placementTurn.Count() / 2)
                {
                    Debug.Log("First phase is over! Place second settlement and road");
                    _gamePhaseState = GamePhaseStateEnum.PLACE_SECOND_SETTLEMENT_ROAD;
                }

                if (_turnCounter >= _placementTurn.Count())
                {
                    Debug.Log("Second phase is over! Enter roll/build/trade phase");
                    _gamePhaseState = GamePhaseStateEnum.ROLL_BUILD_TRADE;
                }    
            }

            if (_gamePhaseState == GamePhaseStateEnum.ROLL_BUILD_TRADE)
            {
                var index = _turnCounter % 4;
                _turnPlayerGuid = _players.ElementAt(index).Guid;
                _turnCounter++;
            }
            else // placement phase
            {
                _turnPlayerGuid = _placementTurn.ElementAt(_turnCounter++);
            }

            Events.OnPlayerTurnChanged.Invoke(GetPlayerByGuid(_turnPlayerGuid));
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
            Events.OnRollDice.Invoke(diceSum); // todo send both 

            ProduceResources(diceSum);

            return diceSum;
        }

        private void ProduceResources(int diceSum)
        {
            var producingTiles = _board.GetTiles().Where((tile) => tile.GetChit() == diceSum).ToList();
            TilesProduce(producingTiles);
            Events.OnTilesProducing.Invoke(producingTiles);
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

            var resourcesGained = new Dictionary<HexTile, int>();

            foreach (var item in producingTileCorners)
            {
                foreach (var corner in item.Corners)
                {
                    var player = GetPlayerByGuid(corner.GetPlayerGuid());
                    var amount = (int) corner.GetState();
                    var resourceType = item.Tile.GetResourceType();
                    // TODO: wont work Key already exists here
                    // group by player instead?
                    // resourcesGained.Add(item.Tile, amount);
                    player.AddResource(resourceType, amount);
                    Debug.Log(corner.GetPlayerGuid() + " got " + item.Tile.GetResourceType() + " amount: " +
                              corner.GetState());
                    Events.OnResourcesUpdate.Invoke(new ResourcesGained(player, resourcesGained));
                }
            }
        }

        public void BuildSettlementAtCorner(int hashCode)
        {
            if (_currentTurnSettlementCount > 0 && _gamePhaseState != GamePhaseStateEnum.ROLL_BUILD_TRADE)
            {
                Debug.Log("Only one settlement allowed in this turn");
                return;
            }

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

            if (_gamePhaseState != GamePhaseStateEnum.ROLL_BUILD_TRADE)
            {
                // Keep track of last settlement corner if not normal phase
                _lastSettlementCorner = corner;
            }

            if (corner.PlaceSettlement(_turnPlayerGuid))
            {
                _currentTurnSettlementCount += 1;
                _lastSettlementCorner = corner;
                if (_gamePhaseState == GamePhaseStateEnum.PLACE_SECOND_SETTLEMENT_ROAD)
                {
                    // Second placement turn, will produce stuff for built settlement adjacent tiles
                    var resourcesGained = new Dictionary<HexTile, int>();

                    var tiles = GetBoard().GetTilesByCorner(corner)
                        .Where((t) => t.GetResourceType() != ResourceEnum.NONE);

                    foreach (var tile in tiles)
                    {
                        var amount = (int) corner.GetState();
                        var resourceType = tile.GetResourceType();
                        resourcesGained.Add(tile, amount);
                        player.AddResource(resourceType, amount);
                    }

                    Events.OnResourcesUpdate.Invoke(new ResourcesGained(player, resourcesGained));
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
            if (_gamePhaseState != GamePhaseStateEnum.ROLL_BUILD_TRADE && _currentTurnRoadCount > 0)
            {
                Debug.Log("Too many roads in this turn");
                return;
            }

            if (_gamePhaseState != GamePhaseStateEnum.ROLL_BUILD_TRADE && _currentTurnSettlementCount != 1)
            {
                Debug.Log("Settlement must be placed first in this phase");
                return;
            }

            // Todo: A road must always be connected to another road edge owned by the player or connected to corner with a building owned by the player.
            var edge = GetBoard().GetEdgeByHashCode(hashCode);
            var ownedAdjacentCorners = edge.GetCorners().Where((c) => c.OwnedByPlayerGuid(_turnPlayerGuid));
            if (ownedAdjacentCorners.Any())
            {
                if (_gamePhaseState != GamePhaseStateEnum.ROLL_BUILD_TRADE)
                {
                    var currentCorner = edge.GetCorners()
                        .Where((c) => c.GetHashCode() == _lastSettlementCorner.GetHashCode());
                    if (!currentCorner.Any())
                    {
                        Debug.Log("Edge be connected to settlement build in this turn!");
                        return;
                    }
                }

                if (edge.PlaceRoad(_turnPlayerGuid))
                {
                    _currentTurnRoadCount += 1;
                    var player = GetPlayerByGuid(_turnPlayerGuid);
                    Events.OnRoadBuilt.Invoke(new RoadBuilt(player, edge));
                    if (_gamePhaseState == GamePhaseStateEnum.ROLL_BUILD_TRADE) return;
                    NextTurn(); // Auto change turn 
                    return;
                }
            }

            // // Events.OnError.Invoke("Cannot build settlement at this corner");
            Debug.Log("Cannot build road at this edge");
        }

        public void FindLongestPath()
        {
            // We have edges / roads
            var edges = GetBoard().GetEdges();
            var edge = edges[0]; // starting edge
            var corner1 = edge.GetLeftCorner();
            var corner2 = edge.GetLeftCorner();
            var test = GetBoard().GetEdgesByCorner(corner1.GetHashCode());
            // filter by
            // edge.HasRoad()
            // edge.GetPlayerGuid()

            // edge.GetCorners()

            // Pick a random road segment, add it to a set, and mark it
            // Branch out from this segment, ie. follow connected segments in both directions that aren't marked (if they're marked, we've already been here)
            // If found road segment is not already in the set, add it, and mark it
            // Keep going from new segments until you cannot find any more unmarked segments that are connected to those currently in the set
            // If there's unmarked segments left, they're part of a new set, pick a random one and start back at 1 with another set
            // Note: A road can be broken if another play builds a settlement on a joint between two segments.
            // You need to detect this and not branch past the settlement.
        }

        public void BuyDevelopmentCard()
        {
            var player = GetPlayerByGuid(_turnPlayerGuid);
            var card = _devCardDeck.TakeCard();
            player.AddDevelopmentCard(card);
            Debug.Log("Player got total cards: of " + player.GetDevelopmentCardsCount());
            // Debug.Log(Costs.DevCard);
            // Can player afford it?
            // Deduct costs from player resources
            Debug.Log("Bought Card: " +  card.ToString());
            Events.OnDevCardBought.Invoke(card.ToString());
            Events.OnPlayerDataChanged.Invoke(player);
        }
    }
}