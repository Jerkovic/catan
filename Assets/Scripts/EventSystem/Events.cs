using UnityEngine;

namespace EventSystem
{
    public static class Events
    {
        public static readonly Event<int> OnRollDice = new Event<int>();
        public static readonly Event<GameObject> OnClickHexagon = new Event<GameObject>();
        public static readonly Event<GameObject> OnClickEdge = new Event<GameObject>();
        public static readonly Event<GameObject> OnClickCorner = new Event<GameObject>();
        
        // Events.OnRollDice.Invoke(12);
        // Events.OnRollDice.AddListener(SomeMethod)
        // Events.OnRollDice.RemoveListener(SomeMethod)
        
    }
}