using System;
using System.Collections.Generic;

namespace ArcoMage
{
    class Game
    {
        public enum Condition
        {
            NotStarted,
            InGame,
            FirstPlayerWin,
            SecondPlayerWin
        }

        public Condition Status = Condition.NotStarted;
        public readonly int TowerHealth;
        public readonly int WallHealth;
        public readonly Player Player1;
        public readonly Player Player2;
        public Player CurrentPlayer { get; private set; }
        private readonly Func<Player, Player, bool> _winCondition;
        public bool GameOver => Status == Condition.FirstPlayerWin || Status == Condition.SecondPlayerWin;

        public Game(int towerHealth, int wallHealth, int deckSize, Dictionary<string, Resource> startResources, 
            Func<Player, Player, bool> winCondition)
        {
            this._winCondition = winCondition;
            var playerDeck = Cards.Generator.GenerateDeck(deckSize);
            TowerHealth = towerHealth;
            WallHealth = wallHealth;
            Player1 = new Player(startResources, new Castle(towerHealth, wallHealth), playerDeck);
            var player2Resource = new Dictionary<string, Resource>();
            foreach (var res in startResources)
            {
                player2Resource.Add(res.Key, new Resource(res.Value.Source, res.Value.Count));
            }
            playerDeck = Cards.Generator.GenerateDeck(deckSize);
            Player2 = new Player(player2Resource, new Castle(towerHealth, wallHealth), playerDeck);
            CurrentPlayer = Player1;
        }

        public void CheckWinner()
        {
            if (_winCondition(Player1, Player2))
                Status = Condition.FirstPlayerWin;
            else if (_winCondition(Player2, Player1))
                Status = Condition.SecondPlayerWin;
        }

        public void SwapPlayers()
        {
            CurrentPlayer = (CurrentPlayer == Player1) ? Player2 : Player1;
        }

        public Player GetOpponent() => (CurrentPlayer == Player1) ? Player2 : Player1;

        public int GetWinner()
        {
            switch (Status)
            {
                case Condition.FirstPlayerWin:
                    return 1;
                case Condition.SecondPlayerWin:
                    return 2;
                default:
                    throw new Exception("Game wasn't finished!");
            }
        }

        public void UpdateResources()
        {
            foreach (var res in Player1.Resources)
            {
                res.Value.Update();
            }

            foreach (var res in Player2.Resources)
            {
                res.Value.Update();
            }
        }
    }
}
