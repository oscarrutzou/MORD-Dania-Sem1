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
        public static SpriteBatch debugSpriteBatch;
        public static List<Tower> towers;
        public static List<Enemy> enemies;
        public static List<IProjectile> projectiles;
    }
}
