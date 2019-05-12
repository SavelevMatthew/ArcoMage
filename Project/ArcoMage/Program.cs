﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
                ["res1"] = new Resource(1, 10),
                ["res2"] = new Resource(2, 20),
                ["res3"] = new Resource(3, 30)
            };
            Func<Player, Player, bool> winCondition = (p1, p2) => p2.Castle.TowerHealth <= 0 
                                                          || p1.Castle.TowerHealth > 200;
            var game = new Game(100, 25, 6, res, winCondition);

            // var form = new Window(game);
            //Application.Run(form);
            game.Play();
            var winner = game.GetWinner();
        }
    }
}