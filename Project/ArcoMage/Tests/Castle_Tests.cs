using System;
using NUnit.Framework;

namespace ArcoMage.Tests
{
    [TestFixture]
    class CastleTests
    {
        private Castle castle;

        [Test]
        public void CorrectInit()
        {
            castle = new Castle(1,1);
            Assert.AreEqual(castle.TowerHealth, 1);
            Assert.AreEqual(castle.WallHealth, 1);
        }

        [TestCase(0, 1)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        public void InCorrectInit(int tower, int wall)
        {
            ArgumentException ex = null;
            try
            {
                castle = new Castle(tower, wall);
            }
            catch (ArgumentException e)
            {
                ex = e;
            }
            Assert.NotNull(ex);
        }
        [TestCase(5, 5, 10)]
        [TestCase(10, 0, 10)]
        [TestCase(15, 0, 5)]
        [TestCase(20, 0, 0)]
        [TestCase(1000000, 0, 0)]
        public void TakeDamageTest(int damage, int expectedWall, int expectedTower)
        {
            castle = new Castle(10, 10);
            castle.TakeDamage(damage);
            Assert.AreEqual(expectedTower, castle.TowerHealth);
            Assert.AreEqual(expectedWall, castle.WallHealth);
        }
        [TestCase(10,10,10, 10)]
        [TestCase(5, 5, 5, 5)]
        public void AddTowerWallCorrectTest(int t, int w, int deltaT, int deltaW)
        {
            castle = new Castle(t,w);
            castle.AddTower(deltaT);
            castle.AddWall(deltaW);
            Assert.AreEqual(t + deltaT, castle.TowerHealth);
            Assert.AreEqual(w + deltaW, castle.WallHealth);
        }

        [TestCase(1,1,-1,-1)]
        [TestCase(1, 1, 1, -1)]
        [TestCase(1, 1, -1, 1)]
        public void AddTowerWallInCorrectTest(int t, int w, int deltaT, int deltaW)
        {
            castle = new Castle(t,w);
            Exception ex = null;
            try
            {
                castle.AddTower(deltaT);
                castle.AddTower(deltaW);
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.NotNull(ex);
        }

    }
}
