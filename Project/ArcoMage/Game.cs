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
        public readonly Player Player1;
        public readonly Player Player2;
        private readonly Func<Player, Player, bool> winCondition;
        private bool GameOver => Status == Condition.FirstPlayerWin || Status == Condition.SecondPlayerWin;

        public Game(int towerHealth, int wallHealth, int deckSize, Dictionary<string, Resource> startResources, 
            Func<Player, Player, bool> winCondition)
        {
            this.winCondition = winCondition;
            var playerDeck = DeckGenerator.GenerateDeck(deckSize);
            
            Player1 = new Player(startResources, new Castle(towerHealth, wallHealth), playerDeck);
            playerDeck = DeckGenerator.GenerateDeck(deckSize);
            Player2 = new Player(startResources, new Castle(towerHealth, wallHealth), playerDeck);
        }
        public void Play()
        {
            Status = Condition.InGame;
            var currentPlayer = Player1;
            var nextPlayer = Player2;
            while(!GameOver)
            {
                foreach (var r in currentPlayer.Resources)
                    r.Value.Update();
                foreach (var r in nextPlayer.Resources)
                    r.Value.Update();
                currentPlayer.MakeStep().Drop()(currentPlayer, nextPlayer);
                CheckWinner();
                SwapPlayers(ref currentPlayer, ref nextPlayer);             
            }
        }

        public void CheckWinner()
        {
            if (winCondition(Player1, Player2))
                Status = Condition.FirstPlayerWin;
            else if (winCondition(Player2, Player1))
                Status = Condition.SecondPlayerWin;
        }

        public void SwapPlayers(ref Player p1, ref Player p2)
        {
            var temp = p2;
            p2 = p1;
            p1 = temp;
        }



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
        
    }

    class DeckGenerator
    {
        public static Card[] GenerateDeck(int size)
        {
            var deck = new Card[size];
            for (var i = 0; i < size; i++)
            {
                deck[i] = Card.GenerateRandomCard();
            }
            return deck;
        }
    }
}
