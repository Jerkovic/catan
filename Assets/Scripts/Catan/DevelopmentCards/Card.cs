namespace Catan.DevelopmentCards
{
    public class DevCard
    {
        public CardTypeEnum CardType { get; }
        public bool Played { get; private set; }
        
        public int ObtainedTurn { get; set; }
        public int PlayedTurn { get; private set; }

        public DevCard(CardTypeEnum cardType)
        {
            CardType = cardType;
            Played = false;
            ObtainedTurn = 0;
            PlayedTurn = 0;
        }

        public bool PlayCard(int turn)
        {
            if (CanBePlayed()) return false;
            Played = true;
            PlayedTurn = turn;
            return true;
        }
        
        public string GetCardType()
        {
            return $"{CardType.ToString().Replace("_", " ")}";
        }

        public bool CanBePlayed()
        {
            // Cannot play a Victory Point card
            if (CardType == CardTypeEnum.VICTORY_POINT)
                return false;

            // Todo Cannot play the card on the same turn it was obtained

            // Cannot play a card that has already been used
            return !Played;
        }

        public string GetCardStatus()
        {
            return $"({ObtainedTurn.ToString()}, {PlayedTurn.ToString()})";
        }

        public override string ToString()
        {
            return $"{CardType}";
        }
    }
}