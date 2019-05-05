using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcoMage.Cards
{
    class Generator
    {
        public static Card GenerateRandomCard()
        {
            return Card.GenerateRandomCard();
        }

        public static Card[] GenerateDeck(int size)
        {
            var deck = new Card[size];
            for (var i = 0; i < size; i++)
            {
                deck[i] = GenerateRandomCard();
            }
            return deck;
        }
    }
}
