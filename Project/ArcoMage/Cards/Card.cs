using System;
using System.Collections.Generic;
using System.Drawing;
using ArcoMage.Graphics;

namespace ArcoMage.Cards
{
    public class Card
    {
        public readonly string Description;
        public readonly Dictionary<string, int> Cost;
        private readonly Action<Player, Player> _effect;
        public readonly Color Color;

        public Card(Action<Player, Player> effect, Dictionary<string, int> costs, string description)
        {
            Color = Window.GetRandomColor();
            Description = description;
            foreach (var cost in costs)
            {
                if (cost.Value < 0)
                    throw new Exception("Incorrect cost exception!");
            }
            _effect = effect;
            Cost = costs;
        }
        

        public Action<Player, Player> Drop() => _effect;
        
        public static Card GiveEmptyCard() => new Card( (p1, p2) => { }, new Dictionary<string, int>(), "Empty Card!");

        public bool CanBeDropped(Player player)
        {
            foreach (var resource in Cost)
            {
                if (!player.Resources.ContainsKey(resource.Key) || player.Resources[resource.Key].Count < resource.Value)
                    return false;
            }
            return true;
        }
    }
}
