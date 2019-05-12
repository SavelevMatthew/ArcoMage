using System;

namespace ArcoMage
{
    public class Castle
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

        public void AddTower(int towerPlus)
        {
            if (towerPlus < 0)
                throw new ArgumentException("towerPlus should be non-negative");
            TowerHealth += towerPlus;
        }
        public void AddWall(int wallPlus)
        {
            if (wallPlus < 0)
                throw new ArgumentException("wallPlus should be non-negative");
            WallHealth += wallPlus;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException("damage should be non-negative");
            if (damage > WallHealth)
                TakeTowerDamage(damage - WallHealth);
            WallHealth = damage < WallHealth ? WallHealth - damage : 0;
        }

        public void TakeTowerDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException("damage should be non-negative");
            TowerHealth = TowerHealth > damage ? TowerHealth - damage : 0;
        }
    }
}
