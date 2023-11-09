using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    public class SceneData
    {
        public GameTime gameTime; //Dont think we use this one
        public SceneStats sceneStats; //Stats for the scene such as money or kills
        public SpriteBatch debugSpriteBatch; 
        public List<GameObject> gameObjects = new List<GameObject>(); //All gameObjects in the scene
        public List<GameObject> gameObjectsToAdd = new List<GameObject>(); //All objects that should be added to the scene

        //These are to better distingush what kind of gameObjects it should 
        public List<Tower> towers = new List<Tower>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Projectile> projectiles = new List<Projectile>();
    }
}
