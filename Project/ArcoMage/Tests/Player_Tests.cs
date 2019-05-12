using System.Collections.Generic;
using ArcoMage.Cards;
using NUnit.Framework;

namespace ArcoMage.Tests
{
    [TestFixture]
    class PlayerTests
    {
        [TestCase(0, 6, new[] { true, false, false, true })]
        [TestCase(2, 6, new[] { true, true, true, false })]
        [TestCase(0, 2, new bool[] { })]
        [TestCase(5, 6, new[] { false })]
        [TestCase(0, 1, new[] { true, true, true, true, false, true, true })]
        public void MoveCursor(int expected, int deckLength, params bool[] directions)
        {
            var player = new Player(null, null, new Card[deckLength]);
            foreach (var direction in directions)
                if (direction)
                    player.CursorRight();
                else
                    player.CursorLeft();
            Assert.AreEqual(expected, player.Cursor);
        }

        [TestCase(3, 3)]
        [TestCase(6, 3)]
        [TestCase(8, 13)]
        [TestCase(7, 0)]
        [TestCase(1, 1)]
        public void CheckCastle(int tower, int wall)
        {
            var player = new Player(null, new Castle(tower, wall), null);
            Assert.AreEqual(tower, player.Castle.TowerHealth);
            Assert.AreEqual(wall, player.Castle.WallHealth);
        }

        [Test]
        public void ResourcesTest1() =>
            TakeResourcesTest(
                new Dictionary<string, Resource> { ["a"] = new Resource(6, 8) },
                new Dictionary<string, int> { ["a"] = 4 });
        [Test]
        public void ResourcesTest2() =>
            TakeResourcesTest(
                new Dictionary<string, Resource> { ["a"] = new Resource(1, 9), ["b"] = new Resource(5, 7) },
                new Dictionary<string, int> { ["a"] = 7 });
        [Test]
        public void ResourcesTest3() =>
            TakeResourcesTest(
                new Dictionary<string, Resource> { ["a"] = new Resource(2, 7), ["b"] = new Resource(3, 6) },
                new Dictionary<string, int> { ["a"] = 2, ["b"] = 5 });

        public void TakeResourcesTest(Dictionary<string, Resource> resources,
            Dictionary<string, int> damages)
        {
            var expected = new Dictionary<string, int>();
            var player = new Player(resources, null, null);
            foreach (var res in player.Resources)
            {
                if (!damages.ContainsKey(res.Key))
                    expected[res.Key] = player.Resources[res.Key].Count;
                else
                    expected[res.Key] = player.Resources[res.Key].Count - damages[res.Key];
            }
            player.TakeResources(damages);
            foreach (var res in player.Resources)
                Assert.AreEqual(res.Value.Count, expected[res.Key]);
        }

        [Test]
        public void TestDestroy1() => TestDestroyCard(new[] {
            Generator.GenerateRandomCard(),
            CardDataBase._emptyCard,
            Generator.GenerateRandomCard()});

        [Test]
        public void TestDestroy2() => TestDestroyCard(new[] {
            Generator.GenerateRandomCard(),
            Generator.GenerateRandomCard(),
            Generator.GenerateRandomCard()});
        [Test]
        public void TestDestroy3() => TestDestroyCard(new Card[0]);
        [Test]
        public void TestDestroy4() => TestDestroyCard(new[] { CardDataBase._emptyCard });
        [Test]
        public void TestDestroy5() => TestDestroyCard(new[] {
            Generator.GenerateRandomCard(),
            CardDataBase._emptyCard,
            Generator.GenerateRandomCard(),
            CardDataBase._emptyCard,
            CardDataBase._emptyCard,
            Generator.GenerateRandomCard()});

        public void TestDestroyCard(Card[] deck)
        {
            var player = new Player(null, null, deck);
            for (var i = 0; i < deck.Length; i++)
            {
                if (player.Deck[i].Description == "Empty Card!")
                    player.DropCard();
                Assert.AreNotEqual(player.Deck[player.Cursor].Description, "Empty Card!");
                player.CursorRight();
            }
        }

        [Test]
        public void TestDrop1() => TestDropCard(new[] {
            Generator.GenerateRandomCard(),
            Generator.GenerateRandomCard()});

        [Test]
        public void TestDrop2() => TestDropCard(new[] {
            Generator.GenerateRandomCard(),
            Generator.GenerateRandomCard(),
            Generator.GenerateRandomCard() });
        [Test]
        public void TestDrop3() => TestDropCard(new[] {
            Generator.GenerateRandomCard(),
            Generator.GenerateRandomCard(),
            Generator.GenerateRandomCard(),
            Generator.GenerateRandomCard()});
        [Test]
        public void TestDrop4() => TestDropCard(new Card[0]);
        public void TestDropCard(Card[] deck)
        {
            var player = new Player(null, null, deck);
            for (var i = 0; i < deck.Length; i++)
            {
                Assert.AreEqual(deck[i], player.DropCard());
                player.CursorRight();
            }
        }
    }
}
