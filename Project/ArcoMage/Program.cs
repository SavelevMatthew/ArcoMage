using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
                ["res1"] = new Resource(1, 10),
                ["res2"] = new Resource(2, 20),
                ["res3"] = new Resource(3, 30)
            };
            Func<Player, bool> winCondition = player => player.Castle.Tower <= 0 
                                                          || player.Castle.Tower > 200;
            var game = new Game(100, 25, 6, res, winCondition);
            game.Play();
            var winner = game.GetWinner();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
