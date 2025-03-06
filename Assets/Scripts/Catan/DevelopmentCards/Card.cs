namespace Catan.DevelopmentCards
{
    public class DevCard
    {
        public CardTypeEnum CardType { get; }
        public bool Played { get; private set; }
        
        // TODO this needs to track in what turn it was obtained

        public DevCard(CardTypeEnum cardType)
        {
            CardType = cardType;
            Played = false;
        }

        public void PlayCard()
        {
            Played = true;
        }
        
        public string GetCardType()
        {
            return $"{CardType.ToString().Replace("_", " ")}";
        }

        public override string ToString()
        {
            return $"{CardType} (Played: {Played})";
        }
    }
}