using System;
using System.Collections.Generic;

namespace ArcoMage.Cards
{
    public class Generator
    {
        public static Random Random = new Random();

        public static Card GiveEmptyCard()
        {
            var costs = CardDataBase.GenerateCost();
            return new Card((p1, p2) => { }, costs, "Empty Card");
        }

        public static Card GenerateRandomCard()
        {
            return CardDataBase.Deck[Random.Next(CardDataBase.Deck.Count)];
        }

        public static Card[] GenerateDeck(int size)
        {
            var deck = new Card[size];
            for (var i = 0; i < size; i++)
                deck[i] = GenerateRandomCard();
            return deck;
        }
    }
}
