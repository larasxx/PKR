using System;
using System.Collections.Generic;
using System.Linq;

namespace PKR.Models
{
    public class Deck
    {
        private List<Card> _cards;
        private Random _random = new Random();

        public Deck()
        {
            _cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
            Shuffle();
        }

        public void Shuffle() => _cards = _cards.OrderBy(c => _random.Next()).ToList();

        public Card DrawCard()
        {
            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }
}
