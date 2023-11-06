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
        public static GameStats gameStats;
        public static SpriteBatch debugSpriteBatch { get; set; }
        public static List<Tower> towers { get; set; }
        public static List<Enemy> enemies { get; set; }
        public static List<IProjectile> projectiles { get; set; }
    }
}
