using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArcoMage.Cards;
using ArcoMage.Graphics;

namespace ArcoMage
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var res = new Dictionary<string, Resource>
            {
                [CardDataBase.Brick] = new Resource(1, 10),
                [CardDataBase.Magic] = new Resource(2, 20),
                [CardDataBase.Animals] = new Resource(3, 30)
            };
            Func<Player, Player, bool> winCondition = (p1, p2) => p2.Castle.TowerHealth <= 0
                                                          || p1.Castle.TowerHealth > 200;
            var game = new Game(100, 25, 6, res, winCondition);
            var form = new Window(game);
            Application.Run(form);

            while (!game.GameOver)
            {
                // Отрисовка
                Card response;
                do
                {
                    response = game.CurrentPlayer.MakeStep();
                    // Отрисовка
                } while (response == null);
                game.CheckWinner();
                game.SwapPlayers();
            }

            //var winner = game.GetWinner();
        }
    }
}
