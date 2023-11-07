using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    public class WorldData
    {
        public GameTime gameTime;
        public GameStats gameStats;
        public SpriteBatch debugSpriteBatch;
        public List<Tower> towers;
        public List<Enemy> enemies;
        public List<IProjectile> projectiles;
    }
}
