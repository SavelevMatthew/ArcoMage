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
            return Card.GiveEmptyCard();
        }

        public static Card[] GenerateDeck(int size)
        {
            var deck = new Card[size];
            for (var i = 0; i < size; i++)
            {
                deck[i] = new Card(Card.GiveEmptyCard().Drop(),
                    new Dictionary<string, int> { ["res1"] = 30, ["res2"] = 20, ["res3"] = 123123, },
                    "asdasdjaksdjaksjdklasjdkljaskd");
            }
            return deck;
        }
    }
}
