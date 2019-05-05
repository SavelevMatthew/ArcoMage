using System;
using System.Collections.Generic;

namespace ArcoMage
{
    class Player
    {
        public Dictionary<string, Resource> Resources;
        public Castle Castle { get; private set; }
        public Card[] Deck { get; private set; }
        private int Cursor { get; set; }

        public Player(Dictionary<string, Resource> res, Castle cast, Card[] deck)
        {
            Deck = deck;
            Resources = res;
            Castle = cast;
        }

        private Card DropCard()
        {
            var card = Deck[Cursor];
            Deck[Cursor] = Card.GiveEmptyCard();
            return card;
        }

        private Card DestroyCard()
        {
            Deck[Cursor] = Card.GiveEmptyCard();
            return Card.GiveEmptyCard();
        }

        public Card MakeStep()
        {
            var key = Console.ReadKey().Key;
            while(true)
            {
                switch (key)
                {
                    case ConsoleKey.D:
                        CursorRight();
                        break;
                    case ConsoleKey.A:
                        CursorLeft();
                        break;
                    case ConsoleKey.Enter:
                        return DropCard();
                    case ConsoleKey.Spacebar:
                        return DestroyCard();
                }

                key = Console.ReadKey().Key;
            }
        }

        private void MoveCursor(int shift) => 
            Cursor = (Cursor + shift + Deck.Length) % Deck.Length;
        private void CursorRight() => MoveCursor(1);
        private void CursorLeft() => MoveCursor(-1);
    }
}
