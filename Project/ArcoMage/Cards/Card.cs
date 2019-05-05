using System;
using System.Collections.Generic;

namespace ArcoMage
{
    class Card
    {
        public readonly Dictionary<string, int> Cost;
        private readonly Action<Player, Player> Effect;

        public Card(Action<Player, Player> effect, Dictionary<string, int> costs)
        {
            foreach (var cost in costs)
            {
                if (cost.Value <= 0)
                    throw new Exception("Incorrect cost exception!");
            }
            Effect = effect;
            Cost = costs;
        }
        

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
