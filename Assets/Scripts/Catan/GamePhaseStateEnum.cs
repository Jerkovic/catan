namespace Catan
{
    public enum GamePhaseStateEnum  : int
    {
        PLACE_FIRST_SETTLEMENT_ROAD = 0,
        PLACE_SECOND_SETTLEMENT_ROAD = 1, // second phase settlement will produce resources
        // ROLL_DICE own State
        ROLL_BUILD_TRADE = 2
    }
}

