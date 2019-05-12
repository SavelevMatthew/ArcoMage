using System;
using System.Collections.Generic;

namespace ArcoMage
{
    public class Card
    {
        public readonly string Description;
        public readonly Dictionary<string, int> Cost;
        private readonly Action<Player, Player> Effect;

        public Card(Action<Player, Player> effect, Dictionary<string, int> costs, string description)
        {
            Description = description;
            foreach (var cost in costs)
            {
                if (cost.Value < 0)
                    throw new Exception("Incorrect cost exception!");
            }
            Effect = effect;
            Cost = costs;
        }
        

        public Action<Player, Player> Drop() => Effect;
        public static Card GiveEmptyCard() => new Card( (p1, p2) => { }, new Dictionary<string, int>(), "Описание процесса,\n например блалалалалалала\nфылрвлфывр");

        public bool CanBeDropped(Player player)
        {
            foreach (var resource in Cost)
            {
                if (player.Resources[resource.Key].Count < resource.Value)
                    return false;
            }
            return true;
        }
    }
}
