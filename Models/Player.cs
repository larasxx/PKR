using PKR.Models;
using System.Collections.Generic;

namespace PKR.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Chips { get; set; }
        public List<Card> Hand { get; private set; } = new List<Card>();
        public bool IsFolded { get; set; }
        public bool IsBigBlind { get; set; }
        public bool IsSmallBlind { get; set; }

        public void DealCard(Card card) => Hand.Add(card);

        public void ClearHand() => Hand.Clear();
    }
}
