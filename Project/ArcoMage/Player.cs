using System;
using System.Collections.Generic;

namespace ArcoMage
{
    class Player
    {
        public Resources Resources;
        public static Castle Castle { get; private set; }
        public static Card[] Deck { get; private set; }
        private static int Cursor { get; set; }

        public Player(Resources res, Castle cast, Card[] deck)
        {
            Deck = deck;
            Resources = res;
            Castle = cast;
        }

        private static readonly Dictionary<ConsoleKey, Action> MovementCommands =
            new Dictionary<ConsoleKey, Action>
            {
                [ConsoleKey.D] = CursorRight,
                [ConsoleKey.A] = CursorLeft,
            };
        private static readonly Dictionary<ConsoleKey, Func<Card>> CardCommands =
            new Dictionary<ConsoleKey, Func<Card>>
            {
                [ConsoleKey.Enter] = DropCard,
                [ConsoleKey.Spacebar] = DestroyCard
            };

        private static Card DropCard()
        {
            var card = Deck[Cursor];
            Deck[Cursor] = new Card();
            return card;
        }

        private static Card DestroyCard()
        {
            Deck[Cursor] = new Card();
            return Card.GiveEmptyCard();
        }

        private static void MoveCursor(ConsoleKey key) => MovementCommands[key]();

        public Card Play()
        {
            var key = Console.ReadKey().Key;
            while(true)
            {
                if (MovementCommands.ContainsKey(key))
                    MovementCommands[key]();
                if (CardCommands.ContainsKey(key))
                    return CardCommands[key]();
                key = Console.ReadKey().Key;
            }
        }

        private static void MoveCursor(int shift) => 
            Cursor = (Cursor + shift + Deck.Length) % Deck.Length;
        private static void CursorRight() => MoveCursor(1);
        private static void CursorLeft() => MoveCursor(-1);
    }
}
