using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ArcoMage.Tests
{
    [TestFixture]
    class CardTests
    {
        private Card card;
        private Action<Player, Player> effect = (p1, p2) => { };

        [Test]
        public void CanBeDropped()
        {
            var correctCosts = new Dictionary<string, int> { ["stones"] = 5 };
            card = new Card(effect, correctCosts);
            var deck = new[] {card};
            var res = new Resources(new Dictionary<string, Resource> { ["stones"] = new Resource() });
            var p = new Player(res, new Castle(10,5), deck);
            Assert.IsFalse(p.Deck[0].CanBeDropped(p));
            res = new Resources(new Dictionary<string, Resource> { ["stones"] = new Resource(1, 5) });
            p = new Player(res, new Castle(1,1), deck);
            Assert.IsTrue(p.Deck[0].CanBeDropped(p));
        }

        [Test]
        public void CardCorrectInit()
        {
            var correctCosts = new Dictionary<string, int> { ["stones"] = 5 };
            card = new Card(effect, correctCosts);
            Assert.AreEqual(card.Cost, correctCosts);
        }
        [Test]
        public void CardInCorrectInit()
        {
            Exception ex = null;
            var inCorrectCosts = new Dictionary<string, int> {["stones"] = -5};
            try
            {
                card = new Card(effect, inCorrectCosts);
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.NotNull(ex);
        }
    }
}
