using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.SceneScripts
{
    internal sealed class Scene2 : Scene
    {
        public Scene2(ContentManager content) : base(content) { }

        public override void Initialize()
        {
            Enemy targetEnemy = new Enemy(EnemyType.Normal, new Vector2(300, 50), content);
            gameObjects.Add(targetEnemy);
            gameObjects.Add(new Tower_Arrow(new Vector2(50, 300), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            gameObjects.Add(new Tower_Arrow(new Vector2(400, 30), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            gameObjects.Add(new Tower_Arrow(new Vector2(10, 10), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            gameObjects.Add(new Tower_Arrow(new Vector2(600, 300), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            gameObjects.Add(new Tower_Arrow(new Vector2(600, 600), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); //Handles the GameObject list

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); //Draws all elements in the GameObject list
        }
    }
}
