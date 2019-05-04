using System;
using System.Collections.Generic;

namespace ArcoMage
{
    class Game
    {
        public Player Player1;
        public Player Player2;
        private readonly Func<Player, bool> playerIsWin;
        private bool GameOver => playerIsWin(Player1) || playerIsWin(Player2);
        private bool? IsFirstPlayer;

        public Game(int towerHealth, int wallHealth, int deckSize, Dictionary<string, Resource> startResources, 
            Func<Player, bool> winCondition)
        {
            playerIsWin = winCondition;
            var playerDeck = GenerateDeck(deckSize);
            var resources = new Resources(startResources);
            Player1 = new Player(resources, new Castle(towerHealth, wallHealth), playerDeck);
            playerDeck = GenerateDeck(deckSize);
            resources = new Resources(startResources);
            Player2 = new Player(resources, new Castle(towerHealth, wallHealth), playerDeck);
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

        public Card[] GenerateDeck(int size)
        {
            var deck = new Card[size];
            for(var i = 0; i < size; i++)
            {
                deck[i] = Card.GenerateRandomCard();
            }
            return deck;
        }

        public int GetWinner() => IsFirstPlayer == null ? 
            throw new Exception("Game did not start") : IsFirstPlayer == true ? 1 : 2;
    }
}
