using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcoMaig
{
    class Card
    {
        readonly Action<Player, Player> Effect;
        readonly Func<Player, bool> CanPlay;

        public Card(Action<Player, Player> effect, Func<Player, bool> canPlay)
        {
            Effect = effect;
            CanPlay = canPlay;
        }

        public Card(){ }

        public  Action<Player, Player> Play() => Effect;
        public static Card GiveEmptyCard() => new Card( (p1, p2) => { }, (p) => true);
    }
}
