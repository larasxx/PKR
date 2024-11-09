using PKR.Models;
using System.Collections.Generic;
using System.Linq;

namespace PKR.Models
{
    public class PokerGame
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public Deck Deck { get; private set; } = new Deck();
        public void ResetDeck()
        {
            Deck = new Deck();
        }
        public int BigBlind { get; set; }
        public int SmallBlind { get; set; }
        public int Pot { get; set; }

        public void DealHands()
        {
            foreach (var player in Players)
            {
                player.ClearHand();
                if (!player.IsFolded)
                {
                    player.DealCard(Deck.DrawCard());
                    player.DealCard(Deck.DrawCard());
                }
            }
        }

        public void CollectBlinds()
        {
            var smallBlindPlayer = Players.FirstOrDefault(p => p.IsSmallBlind);
            var bigBlindPlayer = Players.FirstOrDefault(p => p.IsBigBlind);

            if (smallBlindPlayer != null)
            {
                smallBlindPlayer.Chips -= SmallBlind;
                Pot += SmallBlind;
            }

            if (bigBlindPlayer != null)
            {
                bigBlindPlayer.Chips -= BigBlind;
                Pot += BigBlind;
            }
        }

        public void PlayerAction(Player player, string action, int amount = 0)
        {
            switch (action.ToLower())
            {
                case "bet":
                    player.Chips -= amount;
                    Pot += amount;
                    break;
                case "call":
                    player.Chips -= amount;
                    Pot += amount;
                    break;
                case "fold":
                    player.IsFolded = true;
                    break;
            }
        }
    }
}
