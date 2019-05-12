using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcoMage
{
    static class DeckArcoMage
    {
        public const string brick = "brick";
        public const string magic = "magic";
        public const string animals = "animals";

        public static readonly Card EmptyCard = new Card((p1, p2) => { }, InstallCost(), "Empty Card");
        public static readonly List<Card> Deck = new List<Card>()
        {
            [0] = new Card((p1, p2) => {
                p1.Resources[brick].TakeDamage(0, 8);
                p2.Resources[brick].TakeDamage(0, 8);
            },
                            InstallCost(), "All players lose 8 bricks"),
            [1] = new Card((p1, p2) => p1.Resources[brick].AddResource(1, 0),
                            InstallCost(3), "+1 Mine"),
            [2] = new Card((p1, p2) => {
                if (p1.Resources[brick].Source > p2.Resources[brick].Source)
                    p1.Resources[brick].AddResource(1, 0);
                p1.Resources[brick].AddResource(1, 0);
            },
                            InstallCost(4), "if Mine > enemy's Mine then +2 Mine else +1 Mine"),
            [3] = new Card((p1, p2) => { p1.Resources[brick].AddResource(1, 0); p1.Castle.AddWall(4); },
                            InstallCost(7), "+1 Mine, +4 Wall"),
            [4] = new Card((p1, p2) => p1.Castle.AddWall(6),
                            InstallCost(3, 0, 6), "+6 Wall"),
            [5] = new Card((p1, p2) => {
                if (p1.Resources[brick].Source < p2.Resources[brick].Source)
                    p1.Resources[brick].AddResource(p2.Resources[brick].Source - p1.Resources[brick].Source, 0);
            },
                            InstallCost(5), "if Mine < enemy's Mine then Mine = enemy's Mine"),
            [6] = new Card((p1, p2) => p1.Castle.AddWall(3),
                            InstallCost(2), "+3 Wall"),
            [7] = new Card((p1, p2) => p1.Castle.AddWall(4),
                            InstallCost(3), "+4 Wall"),
            [8] = new Card((p1, p2) => p1.Castle.AddWall(6),
                            InstallCost(5), "+6 Wall"),
            [9] = new Card((p1, p2) => p1.Castle.AddWall(9),
                            InstallCost(10), "+9 Wall"),
            [10] = new Card((p1, p2) => p1.Castle.AddWall(12),
                            InstallCost(13), "+12 Wall"),
            [11] = new Card((p1, p2) => p1.Castle.AddWall(15),
                            InstallCost(16), "+15 Wall"),
            [12] = new Card((p1, p2) => { p1.Resources[animals].AddResource(1, 4); p2.Resources[animals].AddResource(1, 0); },
                            InstallCost(2), "All players add 1 menagerie, You add 4 animals"),
            [13] = new Card((p1, p2) => { if (p1.Castle.WallHealth == 0) p1.Castle.AddWall(3); p1.Castle.AddWall(3); },
                            InstallCost(3), "if Wall == 0 then +6 Wall else +3 Wall"),
            [14] = new Card((p1, p2) => { p1.Resources[brick].TakeDamage(1, 0); p2.Resources[brick].TakeDamage(1, 0); },
                            InstallCost(), "-1 to all players Mine"),
            [15] = new Card((p1, p2) => p2.Resources[brick].TakeDamage(1, 0),
                            InstallCost(4), "-1 enemy Mine"),
            [16] = new Card((p1, p2) => p1.Resources[brick].AddResource(2, 0),
                            InstallCost(6), "+2 Mine"),
            [17] = new Card((p1, p2) => { p1.Castle.AddWall(5); p1.Resources[animals].AddResource(1, 0); },
                             InstallCost(9), "+6 Wall, +1 menagerie"),
            [18] = new Card((p1, p2) => { p1.Castle.AddWall(7); p1.Resources[magic].AddResource(0, 7); },
                            InstallCost(9), "+7 Wall, +7 crystal"),
            [19] = new Card((p1, p2) => { p1.Castle.AddWall(6); p1.Castle.AddTower(3); },
                            InstallCost(11), "+6 Wall, +3 Tower"),
            [20] = new Card((p1, p2) => { p1.Castle.AddWall(8); p1.Castle.AddTower(5); },
                            InstallCost(15), "+8 Wall, +5 Tower"),
            [21] = new Card((p1, p2) => { p1.Castle.AddWall(20); p1.Castle.AddTower(8); },
                            InstallCost(24), "+20 Wall, +8 Tower"),
        };

        private static Dictionary<string, int> InstallCost(int br = 0, int mag = 0, int anim = 0) =>
            new Dictionary<string, int> { [brick] = br, [magic] = mag, [animals] = anim };
    }
}
