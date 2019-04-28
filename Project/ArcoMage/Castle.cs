using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcoMaig
{
    class Castle
    {
        public int Tower { get; private set; }
        public int Wall { get; private set;}

        public Castle(int tower, int wall)
        {
            Tower = tower;
            Wall = wall;
        }

        public void TakeDamage(int damage)
        {
            Tower = damage > Wall ? Tower - (damage - Wall) : 0;
            Wall = damage > Wall ? Wall - damage : 0;
        }

        public void TakeCastleDamage(int damage) => Tower = Tower > damage ? Tower - damage : 0;
    }
}
