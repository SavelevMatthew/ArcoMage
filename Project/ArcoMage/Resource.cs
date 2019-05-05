namespace ArcoMage
{
    public class Resource
    {
        public int Source { get; private set; }
        public int Count { get; private set; }

        public Resource(int source = 1, int count = 0)
        {
            Source = source;
            Count = count;
        }

        public void TakeDamage(int sourceDamage, int countDamage)
        {
            Source = sourceDamage > Source ? 1 : Source - sourceDamage;
            Count = countDamage > Count ? 0 : Count - countDamage;
        }

        public void Update() => Count += Source;
    }
}
