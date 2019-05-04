namespace ArcoMage
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
