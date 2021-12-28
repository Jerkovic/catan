using System;
using System.Collections.Generic;
using System.Text;

namespace Catan.DevelopmentCards
{
    public class Deck
    {
        /*
         *  Deck contains 14 knights, 5 victory points, 2 monopolies, 2 road building and 2 year plenty.
         */
        private static readonly int[] StartingCards = {14, 5, 2, 2, 2};
        private readonly List<int> _cards = new List<int>();


        public Deck()
        {
            for (var i = 0; i < StartingCards.Length; i++)
            {
                for (var j = 0; j < StartingCards[i]; j++)
                {
                    _cards.Add(i);
                }
            }
        }

        public CardTypeEnum TakeCard()
        {
            // todo handle empty deck
            var random = new Random();
            var index = random.Next(_cards.Count);
            var card = _cards[index];
            _cards.RemoveAt(index);
            return (CardTypeEnum) card;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var card in _cards)
            {
                sb.Append(card);
            }

            return sb.ToString();
        }
    }
}