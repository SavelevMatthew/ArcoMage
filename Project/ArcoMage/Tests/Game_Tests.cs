using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ArcoMage.Tests
{
    [TestFixture]
    class GameTests
    {
        private readonly Dictionary<string, Resource> _res = new Dictionary<string, Resource>
        {
            ["Bricks"] = new Resource(),
            ["Magic"] = new Resource(),
            ["Animals"] = new Resource()
        };

        private Func<Player, Player, bool> _win = (p1, p2) => p1.Castle.TowerHealth > 100;
        private Game CreateGame() => new Game(10, 10, 6, _res, _win);
        private Game _game;

        [Test]
        public void GetOpponentAndSwapTest()
        {
            _game = CreateGame();
            Assert.AreEqual(_game.CurrentPlayer, _game.Player1);
            Assert.AreEqual(_game.GetOpponent(), _game.Player2);
            _game.SwapPlayers();
            Assert.AreEqual(_game.GetOpponent(), _game.Player1);
            Assert.AreEqual(_game.CurrentPlayer, _game.Player2);
            _game.SwapPlayers();
            Assert.AreEqual(_game.CurrentPlayer, _game.Player1);
            Assert.AreEqual(_game.GetOpponent(), _game.Player2);
        }

        [TestCase(5, 5)]
        [TestCase(10, 15)]
        [TestCase(100, 115)]
        public void UpdateResourcesTest(int iteration, int expected)
        {
            _game = CreateGame();
            for (var i = 0; i < iteration; i++)
            {
                _game.UpdateResources();
            }

            foreach (var res in _game.Player1.Resources)
            {
                Assert.AreEqual(expected, res.Value.Count);
            }
            foreach (var res in _game.Player2.Resources)
            {
                Assert.AreEqual(expected, res.Value.Count);
            }

        }

        [Test]
        public void GameOverTest()
        {
            _game = CreateGame();
            Assert.False(_game.GameOver);
            _game.Status = Game.Condition.FirstPlayerWin;
            Assert.True(_game.GameOver);
            _game.Status = Game.Condition.SecondPlayerWin;
            Assert.True(_game.GameOver);
            _game.Status = Game.Condition.NotStarted;
            Assert.False(_game.GameOver);
        }

        [Test]
        public void TestWin()
        {
            _win = (p1, p2) => p1.Castle.TowerHealth == 10;
            _game = new Game(10, 5, 6, _res, _win);
            _game.Player2.Castle.TakeTowerDamage(1);
            _game.CheckWinner();
            Assert.AreEqual(Game.Condition.FirstPlayerWin, _game.Status);
            Assert.AreEqual(1, _game.GetWinner());
            _game.Player1.Castle.TakeTowerDamage(1);
            _game.Player2.Castle.AddTower(1);
            _game.CheckWinner();
            Assert.AreEqual(Game.Condition.SecondPlayerWin, _game.Status);
            Assert.AreEqual(2, _game.GetWinner());
            Exception ex = null;
            _game = new Game(1, 5, 6, _res, _win);
            try
            {
                _game.GetWinner();
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.NotNull(ex);
        }
    }
}
