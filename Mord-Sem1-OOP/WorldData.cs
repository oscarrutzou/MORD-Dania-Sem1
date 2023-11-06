using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    static public class WorldData
    {
        public static GameStats GameStats;
        public static SpriteBatch DebugSpriteBatch { get; set; }
        public static List<Tower> Towers { get; set; }
        public static List<Enemy> Enemies { get; set; }
        public static List<IProjectile> Projectiles { get; set; }
    }
}
