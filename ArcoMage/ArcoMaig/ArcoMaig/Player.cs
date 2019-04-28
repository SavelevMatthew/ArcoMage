using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcoMaig
{
    class Player
    {
        public static Resource Mine { get; private set; }
        public static Resource Magic { get; private set; }
        public static Resource Menagerie { get; private set; }
        public static Castle Castle { get; private set; }
        public static Card[] Deck { get; private set; }
        private static int Cursor { get; set; }

        private static Dictionary<ConsoleKey, Action> ComandsStep =
            new Dictionary<ConsoleKey, Action>
            {
                [ConsoleKey.D] = () => CursorRight(),
                [ConsoleKey.A] = () => CursorLeft(),
            };
        private static Dictionary<ConsoleKey, Func<Card>> ComandsCard =
            new Dictionary<ConsoleKey, Func<Card>>
            {
                [ConsoleKey.Enter] = () => GiveCard(),
                [ConsoleKey.Spacebar] = () => FCard()
            };

        private static Card GiveCard()
        {
            var card = Deck[Cursor];
            Deck[Cursor] = new Card();
            return card;
        }

        private static Card FCard()
        {
            Deck[Cursor] = new Card();
            return Card.GiveEmptyCard();
        }

        private static void MoveCursor(ConsoleKey key) => ComandsStep[key]();

        public Card Play()
        {
            var key = Console.ReadKey().Key;
            while(true)
            {
                if (ComandsStep.ContainsKey(key))
                    ComandsStep[key]();
                if (ComandsCard.ContainsKey(key))
                    return ComandsCard[key]();
                key = Console.ReadKey().Key;
            }
        }

        private static void MoveCursor(int shift) => 
            Cursor = (Cursor + shift + Deck.Length) % Deck.Length;
        private static void CursorRight() => MoveCursor(1);
        private static void CursorLeft() => MoveCursor(-1);
    }
}
