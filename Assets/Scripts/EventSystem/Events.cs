namespace EventSystem
{
    public static class Events
    {
        public static readonly Event<int> onRollDice = new Event<int>();  
        
        // Events.onRollDice.Invoke(12);
        // Events.onRollDice.AddListener(SomeMethod)
        // Events.onRollDice.RemoveListener(SomeMethod)
        
    }
}