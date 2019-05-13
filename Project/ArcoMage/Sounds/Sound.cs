using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ArcoMage.Properties;

namespace ArcoMage.Sounds
{
    public enum SoundType
    {
        Move,
        Drop,
        Delete,
        Wrong
    }

    public static class Sound
    {
      

        private static readonly Dictionary<SoundType, SoundPlayer> SoundsBase = new Dictionary<SoundType, SoundPlayer>
        {
            [SoundType.Move] = new SoundPlayer(Resources.Move),
            [SoundType.Delete] = new SoundPlayer(Resources.Delete),
            [SoundType.Drop] = new SoundPlayer(Resources.Drop),
            [SoundType.Wrong] = new SoundPlayer(Resources.Wrong)
        };

        public static void Play(SoundType s)
        {
            SoundsBase[s].Play();
        }
    }
}
