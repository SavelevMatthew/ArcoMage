using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcoMage.Cards
{
    static class Generator
    {
        public static readonly Card EmptyCard = DeckArcoMage.EmptyCard; 

        private static Random random = new Random();

        public static Card GenerateRandomCard() => 
            DeckArcoMage.Deck[Math.Abs((int)random.NextDouble() * DeckArcoMage.Deck.Count) % DeckArcoMage.Deck.Count];

        public static Card[] GenerateDeck(int size)
        {
            var deck = new Card[size];
            for (var i = 0; i < size; i++)
               deck[i] = GenerateRandomCard();
            return deck;
        }
    }
}
