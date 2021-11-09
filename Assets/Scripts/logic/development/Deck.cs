using System;
using System.Collections.Generic;
using System.Text;

namespace logic.development
{
    public class Deck
    {
        /*
         *  Deck contains 14 knights, 5 victory points, 2 monopolies, 2 road building and 2 year plenty.
         */
        private static readonly int[] startingCards = {14, 5, 2, 2, 2};
        private List<int> cards = new List<int>();


        public Deck()
        {
            for (var i = 0; i < startingCards.Length; i++)
            {
                for (var j = 0; j < startingCards[i]; j++)
                {
                    cards.Add(i);
                }
            }
        }

        public int TakeCard()
        {
            var random = new Random();
            int index = random.Next(cards.Count);
            var card = cards[index];
            cards.RemoveAt(index);
            return card;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var card in cards)
            {
                sb.Append(card);
            }

            return sb.ToString();
        }
    }
}