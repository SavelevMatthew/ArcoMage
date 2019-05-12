using System;

namespace ArcoMage
{
    class Castle
    {
        public int TowerHealth { get; private set; }
        public int WallHealth { get; private set;}

        public Castle(int towerHealth, int wallHealth)
        {
            if (towerHealth <= 0)
                throw new ArgumentException("TowerHealth should be positive.");
            if (wallHealth < 0)
                throw new ArgumentException("WallHealth should be non-negative");
            TowerHealth = towerHealth;
            WallHealth = wallHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage > WallHealth)
                TakeTowerDamage(damage - WallHealth);
            WallHealth = damage < WallHealth ? WallHealth - damage : 0;
        }

        public void TakeTowerDamage(int damage) => TowerHealth = TowerHealth > damage ? TowerHealth - damage : 0;
    }
}
