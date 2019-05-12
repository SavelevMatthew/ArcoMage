using System.Collections.Generic;

namespace ArcoMage.Cards
{
    public class CardDataBase
    {
        public const string Brick = "brick";
        public const string Magic = "magic";
        public const string Animals = "animals";
        public static readonly List<Card> Deck = new List<Card>()
        {
            [0] = new Card((p1, p2) => {
                p1.Resources[Brick].TakeDamage(0, 8);
                p2.Resources[Brick].TakeDamage(0, 8);
            },
                            GenerateCost(), "All players lose 8 bricks"),
            [1] = new Card((p1, p2) => p1.Resources[Brick].AddResource(1, 0),
                            GenerateCost(3), "+1 Mine"),
            [2] = new Card((p1, p2) => {
                if (p1.Resources[Brick].Source > p2.Resources[Brick].Source)
                    p1.Resources[Brick].AddResource(1, 0);
                p1.Resources[Brick].AddResource(1, 0);
            },
                            GenerateCost(4), "if Mine > enemy's Mine then +2 Mine else +1 Mine"),
            [3] = new Card((p1, p2) => { p1.Resources[Brick].AddResource(1, 0); p1.Castle.AddWall(4); },
                            GenerateCost(7), "+1 Mine, +4 Wall"),
            [4] = new Card((p1, p2) => p1.Castle.AddWall(6),
                            GenerateCost(3, 0, 6), "+6 Wall"),
            [5] = new Card((p1, p2) => {
                if (p1.Resources[Brick].Source < p2.Resources[Brick].Source)
                    p1.Resources[Brick].AddResource(p2.Resources[Brick].Source - p1.Resources[Brick].Source, 0);
            }, GenerateCost(5), "if Mine < enemy's Mine then Mine = enemy's Mine"),
            [6] = new Card((p1, p2) => p1.Castle.AddWall(3),
                            GenerateCost(2), "+3 Wall"),
            [7] = new Card((p1, p2) => p1.Castle.AddWall(4),
                            GenerateCost(3), "+4 Wall"),
            [8] = new Card((p1, p2) => p1.Castle.AddWall(6),
                            GenerateCost(5), "+6 Wall"),
            [9] = new Card((p1, p2) => p1.Castle.AddWall(9),
                            GenerateCost(10), "+9 Wall"),
            [10] = new Card((p1, p2) => p1.Castle.AddWall(12),
                            GenerateCost(13), "+12 Wall"),
            [11] = new Card((p1, p2) => p1.Castle.AddWall(15),
                            GenerateCost(16), "+15 Wall"),
            [12] = new Card((p1, p2) => { p1.Resources[Animals].AddResource(1, 4); p2.Resources[Animals].AddResource(1, 0); },
                            GenerateCost(2), "All players add 1 menagerie, You add 4 animals"),
            [13] = new Card((p1, p2) => { if (p1.Castle.WallHealth == 0) p1.Castle.AddWall(3); p1.Castle.AddWall(3); },
                            GenerateCost(3), "if Wall == 0 then +6 Wall else +3 Wall"),
            [14] = new Card((p1, p2) => { p1.Resources[Brick].TakeDamage(1, 0); p2.Resources[Brick].TakeDamage(1, 0); },
                            GenerateCost(), "-1 to all players Mine"),
            [15] = new Card((p1, p2) => p2.Resources[Brick].TakeDamage(1, 0),
                            GenerateCost(4), "-1 enemy Mine"),
            [16] = new Card((p1, p2) => p1.Resources[Brick].AddResource(2, 0),
                            GenerateCost(6), "+2 Mine"),
            [17] = new Card((p1, p2) => { p1.Castle.AddWall(5); p1.Resources[Animals].AddResource(1, 0); },
                             GenerateCost(9), "+6 Wall, +1 menagerie"),
            [18] = new Card((p1, p2) => { p1.Castle.AddWall(7); p1.Resources[Magic].AddResource(0, 7); },
                            GenerateCost(9), "+7 Wall, +7 crystal"),
            [19] = new Card((p1, p2) => { p1.Castle.AddWall(6); p1.Castle.AddTower(3); },
                            GenerateCost(11), "+6 Wall, +3 Tower"),
            [20] = new Card((p1, p2) => { p1.Castle.AddWall(8); p1.Castle.AddTower(5); },
                            GenerateCost(15), "+8 Wall, +5 Tower"),
            [21] = new Card((p1, p2) => { p1.Castle.AddWall(20); p1.Castle.AddTower(8); },
                            GenerateCost(24), "+20 Wall, +8 Tower"),
        };

        public static Dictionary<string, int> GenerateCost(int br = 0, int mag = 0, int anim = 0) => 
            new Dictionary<string, int> {[Brick] = br, [Magic] = mag, [Animals] = anim};

    }
}
