using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcoMaig
{
    class Game
    {
        public Player Player1;
        public Player Player2;
        private Func<Player, bool> PlayerIsWin;
        private bool GameOver { get => PlayerIsWin(Player1) || PlayerIsWin(Player2); }
        private bool? IsFirstPlayer;

        public void Play()
        {
            var isFirstPlayer = true;
            while(!GameOver)
            {
                if (isFirstPlayer)
                    Player1.Play().Play()(Player1, Player2);
                else
                    Player2.Play().Play()(Player2, Player1);
                isFirstPlayer = !isFirstPlayer;
            }
            IsFirstPlayer = isFirstPlayer;
        }

        public int GetWinner() => IsFirstPlayer == null ? 
            throw new Exception("Game did not start") : IsFirstPlayer == true ? 1 : 2;
    }
}
