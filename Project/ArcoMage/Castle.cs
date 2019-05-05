using System;

namespace ArcoMage
{
    class Castle
    {
        public int Tower { get; private set; }
        public int Wall { get; private set;}

        public Castle(int tower, int wall)
        {
            if (tower <= 0)
                throw new ArgumentException("Tower should be positive.");
            if (wall < 0)
                throw new ArgumentException("Wall should be non-negative");
            Tower = tower;
            Wall = wall;
        }

        public void TakeDamage(int damage)
        {
            if (damage > Wall)
                TakeTowerDamage(damage - Wall);
            Wall = damage < Wall ? Wall - damage : 0;
        }

        public void TakeTowerDamage(int damage) => Tower = Tower > damage ? Tower - damage : 0;
    }
}
