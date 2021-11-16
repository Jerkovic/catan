using logic;

namespace EventSystem
{
    public static class Events
    {
        public static readonly Event<int> OnRollDice = new Event<int>();
        public static readonly Event<int> OnClickHexagon = new Event<int>();
        
        // Events.onRollDice.Invoke(12);
        // Events.onRollDice.AddListener(SomeMethod)
        // Events.onRollDice.RemoveListener(SomeMethod)
        
    }
}