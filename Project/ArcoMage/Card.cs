using System;
using System.Collections.Generic;

namespace ArcoMage
{
    class Card
    {
        public readonly Dictionary<string, int> Cost;
        private readonly Action<Player, Player> Effect;
        //private readonly Func<Player, bool> CanPlay;

        public Card(Action<Player, Player> effect, Dictionary<string, int> costs)
        {
            Effect = effect;
            Cost = costs;
        }

        public Card(){ }

        public Action<Player, Player> Drop() => Effect;
        public static Card GiveEmptyCard() => new Card( (p1, p2) => { }, new Dictionary<string, int>());

        public bool CanBeDropped(Player player)
        {
            foreach (var resource in Cost)
            {
                if (player.Resources[resource.Key].Count < resource.Value)
                    return false;
            }
            return true;
        }

        public static Card GenerateRandomCard() => GiveEmptyCard();
    }
}
