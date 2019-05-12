using System;

namespace ArcoMage
{
    public class Resource
    {
        public int Source { get; private set; }
        public int Count { get; private set; }

        public Resource(int source = 1, int count = 0)
        {
            if (count < 0)
                throw new ArgumentException("Incorrect resource amount!");
            Source = source;
            Count = count;
        }

        public void TakeDamage(int sourceDamage, int countDamage)
        {
            Source -= sourceDamage;
            Count = countDamage > Count ? 0 : Count - countDamage;
        }

        public void Update() => Count = (Count + Source) > 0 ? Count + Source : 0;
    }
}
