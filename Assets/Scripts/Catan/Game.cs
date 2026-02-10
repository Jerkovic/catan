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

        private int _currentTurnSettlementCount = 0;
        private int _currentTurnRoadCount = 0;

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
            // var player3 = new Player(Color.yellow, "Vincent");
            // var player4 = new Player(Color.magenta, "Victoria");
            _players.Add(player1);
            _players.Add(player2);
            // _players.Add(player3);
            // _players.Add(player4);
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

            Debug.Log("Initializing Game #" + GetGameId());
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
                var index = _turnCounter % _players.Count;
                var turnPlayer = _players.ElementAt(index);
                _turnPlayerGuid = turnPlayer.Guid;
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
        
        
        public int GetTurnCounter()
        {
            return _turnCounter;
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
            Events.OnRolledDices.Invoke(new DicesRolled(dice1, dice2, diceSum));
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
            var producingTileCorners = producingTiles.Select((tile) => new
            {
                Tile = tile,
                Corners = GetBoard()
                    .GetCornersByTile(tile.GetHashCode())
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
                    // Events.OnResourcesUpdate.Invoke(new ResourcesGained(player, resourcesGained));
                    Events.OnPlayerDataChanged.Invoke(player); // temporary
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

            if (_gamePhaseState == GamePhaseStateEnum.ROLL_BUILD_TRADE)
            {
                if (player.CanAffordResource(Costs.Settlement))
                {
                    player.DeductResourceCost(Costs.Settlement);
                    Events.OnPlayerDataChanged.Invoke(player);
                }
                else
                {
                    Debug.Log("Cant afford settlement");
                    return;
                }
            }

            if (corner.PlaceSettlement(_turnPlayerGuid))
            {
                player.AddPoints(1);
                Events.OnPlayerDataChanged.Invoke(player);
                _currentTurnSettlementCount += 1;
                _lastSettlementCorner = corner;
                if (_gamePhaseState == GamePhaseStateEnum.PLACE_SECOND_SETTLEMENT_ROAD)
                {
                    // Second placement turn, will produce stuff for built settlement adjacent tiles
                    var resourcesGained = new Dictionary<HexTile, int>();
                    var tiles = GetBoard()
                        .GetTilesByCorner(corner)
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

            Debug.Log("Cannot build settlement at this corner");
        }

        public void UpgradeSettlementToCityAtCorner(int hashCode)
        {
            var corner = GetBoard().GetCornerByHashCode(hashCode);

            if (corner.PlaceCity(_turnPlayerGuid))
            {
                var player = GetPlayerByGuid(_turnPlayerGuid);
                player.AddPoints(1);
                Events.OnPlayerDataChanged.Invoke(player);
                Events.OnSettlementUpgradeToCity.Invoke(new SettlementBuilt(player, corner));
            }
            else
            {
                Debug.Log("Cannot upgrade this settlement to a city");
            }
        }

        private bool OwnAnyAdjacentCorner(Edge edge)
        {
            var ownedAdjacentCorners = edge.GetCorners().Where((c) => c.OwnedByPlayerGuid(_turnPlayerGuid));
            return ownedAdjacentCorners.Any();
        }

        private bool CanBuildRoadOnEdge(Edge edge)
        {
            var edgeCorners = edge.GetCorners().ToList(); // corner  <- edge ->  corner
            var testEdges = edgeCorners.SelectMany(c => GetBoard().GetEdgesByCorner(c.GetHashCode())).ToList();
            var filteredOwnedEdges = testEdges
                .Where((e) => e.GetHashCode() != edge.GetHashCode() && e.OwnedByPlayerGuid(_turnPlayerGuid))
                .ToList();

            // Has road edge blocking opponent building?
            if (filteredOwnedEdges.Count == 1)
            {
                var adjacentEdgeCorners = filteredOwnedEdges[0].GetCorners();
                var allCorners = adjacentEdgeCorners.Concat(edgeCorners).ToList();
                Debug.Log(allCorners.Count());
                var cornerHash = allCorners.GroupBy(x => x.GetHashCode())
                    .Where(g => g.Count() > 1)
                    .Select(y => y.Key)
                    .ToList();
                if (cornerHash.Any())
                {
                    var corner = GetBoard().GetCornerByHashCode(cornerHash[0]);
                    if (corner.HasBuilding() && !corner.OwnedByPlayerGuid(_turnPlayerGuid))
                    {
                        Debug.Log("Cannot build road because of blocking building");
                        return false;
                    }
                }
            }

            Debug.Log("Connected edges: " + filteredOwnedEdges.Count);
            return filteredOwnedEdges.Count > 0;
        }

        public void BuildRoadAtEdge(int hashCode)
        {
            var edge = GetBoard().GetEdgeByHashCode(hashCode);
            var player = GetPlayerByGuid(_turnPlayerGuid);

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

            if (_gamePhaseState == GamePhaseStateEnum.ROLL_BUILD_TRADE)
            {
                // can afford it?
                if (!player.CanAffordResource(Costs.Road))
                {
                    Debug.Log("Cant afford road building");
                    return;
                }
            }

            if (true)
            {
                if (_gamePhaseState != GamePhaseStateEnum.ROLL_BUILD_TRADE)
                {
                    var currentCorner = edge.GetCorners()
                        .Where((c) => c.GetHashCode() == _lastSettlementCorner.GetHashCode());
                    if (!currentCorner.Any())
                    {
                        Debug.Log("Edge has to be connected to settlement built in this turn");
                        return;
                    }
                }
                else // Normal phase ROLL_BUILD_TRADE
                {
                    if (!CanBuildRoadOnEdge(edge) && !OwnAnyAdjacentCorner(edge))
                    {
                        Debug.Log("Edge is not valid for road building");
                        return;
                    }
                }

                if (edge.PlaceRoad(_turnPlayerGuid))
                {
                    _currentTurnRoadCount += 1;
                    Events.OnRoadBuilt.Invoke(new RoadBuilt(player, edge));
                    if (_gamePhaseState == GamePhaseStateEnum.ROLL_BUILD_TRADE)
                    {
                        player.DeductResourceCost(Costs.Road);
                        Events.OnPlayerDataChanged.Invoke(player);
                        return;
                    }

                    ;

                    NextTurn(); // Auto change turn on initial phases 
                    return;
                }
            }

            // Events.OnError.Invoke("Cannot build settlement at this corner");
            Debug.Log("Cannot build road at this edge");
        }

        private List<Edge> FindNeighborEdges(Edge edge)
        {
            // todo check corner if opposing player owns any of these corners
            // filter out remove corners that : corner.HasBuilding() && !corner.OwnedByPlayerGuid(_turnPlayerGuid)
            var edgeCorners = edge.GetCorners().ToList(); // corner  <- edge ->  corner
            var testEdges = edgeCorners.SelectMany(c => GetBoard().GetEdgesByCorner(c.GetHashCode())).ToList();
            return testEdges.Where((e) =>
                    e.HasRoad() && e.GetHashCode() != edge.GetHashCode() && e.OwnedByPlayerGuid(_turnPlayerGuid))
                .ToList();
        }

        private void FindLongestPath()
        {
            var playerRoadEdges = GetBoard()
                .GetEdges()
                .Where(e => e.HasRoad() && e.OwnedByPlayerGuid(_turnPlayerGuid) )
                .ToList();
            // call detect longest road for current player

            Events.OnLongestRoad.Invoke(playerRoadEdges.ToList());
        }

        public void BuyDevelopmentCard()
        {
            FindLongestPath(); // test
            
            var player = GetPlayerByGuid(_turnPlayerGuid);
            if (!player.CanAffordResource(Costs.DevCard)) return;

            player.DeductResourceCost(Costs.DevCard);

            var card = _devCardDeck.TakeCard();
            card.ObtainedTurn = _turnCounter; // track obtain identifier
            player.AddDevelopmentCard(card);

            Events.OnDevCardBought.Invoke(card);
            Events.OnPlayerDataChanged.Invoke(player);
            Events.OnPlayerDevCardsChanged.Invoke(player);
        }
    }
}