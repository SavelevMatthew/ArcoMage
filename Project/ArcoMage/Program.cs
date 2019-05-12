using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArcoMage.Cards;
using ArcoMage.Graphics;

namespace ArcoMage
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var res = new Dictionary<string, Resource>
            {
                [CardDataBase.Brick] = new Resource(1, 10),
                [CardDataBase.Magic] = new Resource(2, 20),
                [CardDataBase.Animals] = new Resource(3, 30)
            };
            bool WinCondition(Player p1, Player p2) => p2.Castle.TowerHealth <= 0 || p1.Castle.TowerHealth > 200;
            var game = new Game(100, 25, 6, res, WinCondition);
            var form = new Window(game);
            Application.Run(form);

        }
    }
}
