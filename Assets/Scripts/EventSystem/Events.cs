using System.Collections.Generic;
using Catan;
using Catan.Resources;
using UnityEngine;

namespace EventSystem
{
    public static class Events
    {
        // User Action Events
        public static readonly Event<GameObject> OnClickHexagon = new Event<GameObject>();
        public static readonly Event<GameObject> OnClickEdge = new Event<GameObject>();
        public static readonly Event<GameObject> OnClickCorner = new Event<GameObject>();
        public static readonly Event<GameObject> OnClickSettlement = new Event<GameObject>();

        // Events Invoked in the Game
        public static readonly Event<List<Player>> OnGameStarted = new Event<List<Player>>();
        public static readonly Event<HexTile> OnRobberMove = new Event<HexTile>();
        public static readonly Event<int> OnRollDice = new Event<int>();
        public static readonly Event<List<HexTile>> OnTilesProducing = new Event<List<HexTile>>();
        
        public static readonly Event<List<Edge>> OnLongestRoad = new Event<List<Edge>>();
        public static readonly Event<Edge> OnRoadHighlight = new Event<Edge>();

        public static readonly Event<ResourcesGained> OnResourcesUpdate = new Event<ResourcesGained>();

        public static readonly Event<Player> OnPlayerTurnChanged = new Event<Player>();
        public static readonly Event<Player> OnPlayerDataChanged = new Event<Player>();
        public static readonly Event<SettlementBuilt> OnSettlementBuilt = new Event<SettlementBuilt>();
        public static readonly Event<RoadBuilt> OnRoadBuilt = new Event<RoadBuilt>();
        
        public static readonly Event<SettlementBuilt> OnSettlementUpgradeToCity = new Event<SettlementBuilt>();
        
        public static readonly Event<string> OnDevCardBought = new Event<string>();
    }

    // Event Params Models
    public class ResourcesGained
    {
        public Player Player { get; set; }

        public Dictionary<HexTile, int> Resources { get; set; }

        public ResourcesGained(Player player, Dictionary<HexTile, int> resources)
        {
            this.Player = player;
            this.Resources = resources;
        }
    }
    public class SettlementBuilt
    {
        public Player Player { get; set; }
        public Corner Corner { get; set; }

        public SettlementBuilt(Player player, Corner corner)
        {
            this.Player = player;
            this.Corner = corner;
        }
    }

    public class RoadBuilt
    {
        public Player Player { get; set; }
        public Edge Edge { get; set; }

        public RoadBuilt(Player player, Edge edge)
        {
            this.Player = player;
            this.Edge = edge;
        }
    }
}