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

        // Events Invoked in the Game Game
        public static readonly Event<HexTile> OnRobberMove = new Event<HexTile>();
        public static readonly Event<int> OnRollDice = new Event<int>();
        public static readonly Event<Dictionary<ResourceEnum, int>> OnResourcesUpdate = new Event<Dictionary<ResourceEnum, int>>();
        
        // Events.OnRollDice.Invoke(12);
        // Events.OnRollDice.AddListener(SomeMethod)
        // Events.OnRollDice.RemoveListener(SomeMethod)

    }
}