using System;

namespace ArcoMage
{
    class Game
    {
        public Player Player1;
        public Player Player2;
        private readonly Func<Player, bool> playerIsWin;
        private bool GameOver => playerIsWin(Player1) || playerIsWin(Player2);
        private bool? IsFirstPlayer;

        public Game(int towerHealth, int wallHealth, Resources startResources, 
            Func<Player, bool> winCondition)
        {
            var playerDeck = new Card[6];
            playerIsWin = winCondition;
            Player1 = new Player(startResources, new Castle(towerHealth, wallHealth), playerDeck);
            Player2 = new Player(startResources, new Castle(towerHealth, wallHealth), playerDeck);
        }
        public void Play()
        {
            var isFirstPlayer = true;
            while(!GameOver)
            {
                if (isFirstPlayer)
                    Player1.Play().Drop()(Player1, Player2);
                else
                    Player2.Play().Drop()(Player2, Player1);
                isFirstPlayer = !isFirstPlayer;
            }
            IsFirstPlayer = isFirstPlayer;
        }

        public int GetWinner() => IsFirstPlayer == null ? 
            throw new Exception("Game did not start") : IsFirstPlayer == true ? 1 : 2;
    }
}
