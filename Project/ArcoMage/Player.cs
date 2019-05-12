using System;
using System.Collections.Generic;

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
            switch (key)
            {
                case ConsoleKey.D:
                    CursorRight();
                    return null;
                case ConsoleKey.A:
                    CursorLeft();
                    return null;
                case ConsoleKey.Enter:
                    return DropCard();
                case ConsoleKey.Spacebar:
                    return DestroyCard();
                default:
                    return null;
            }
        }

        private void MoveCursor(int shift) => 
            Cursor = (Cursor + shift + Deck.Length) % Deck.Length;
        private void CursorRight() => MoveCursor(1);
        private void CursorLeft() => MoveCursor(-1);
    }
}
