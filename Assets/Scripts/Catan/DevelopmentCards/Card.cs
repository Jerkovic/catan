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

        public void PlayCard(int turn)
        {
            Played = true;
            PlayedTurn = turn;
        }
        
        public string GetCardType()
        {
            return $"{CardType.ToString().Replace("_", " ")}";
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