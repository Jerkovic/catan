using logic;
using UnityEngine;

namespace EventSystem
{
    public static class Events
    {
        // User Action Events
        public static readonly Event<GameObject> OnClickHexagon = new Event<GameObject>();
        public static readonly Event<GameObject> OnClickEdge = new Event<GameObject>();
        public static readonly Event<GameObject> OnClickCorner = new Event<GameObject>();

        // Events Invoked in the Catan Game
        public static readonly Event<HexTile> OnRobberMove = new Event<HexTile>();
        public static readonly Event<int> OnRollDice = new Event<int>();

        
        // Events.OnRollDice.Invoke(12);
        // Events.OnRollDice.AddListener(SomeMethod)
        // Events.OnRollDice.RemoveListener(SomeMethod)

    }
}