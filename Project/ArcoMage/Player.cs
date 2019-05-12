using System;
using System.Collections.Generic;
using ArcoMage.Cards;

namespace ArcoMage
{
    public class Player
    {
        public readonly Dictionary<string, Resource> Resources;
        public Castle Castle { get; private set; }
        public Card[] Deck { get; private set; }
        public int Cursor { get; private set; }

        public Player(Dictionary<string, Resource> res, Castle cast, Card[] deck)
        {
            Deck = deck;
            Resources = res;
            Castle = cast;
        }

        public Card DropCard()
        {
            var card = Deck[Cursor];
            Deck[Cursor] = Generator.GenerateRandomCard();
            return card;
        }

        public Card DestroyCard()
        {
            Deck[Cursor] = Generator.GenerateRandomCard();
            return Card.GiveEmptyCard();
        }

        

        private void MoveCursor(int shift) => 
            Cursor = (Cursor + shift + Deck.Length) % Deck.Length;
        public void CursorRight() => MoveCursor(1);
        public void CursorLeft() => MoveCursor(-1);
    }
}
