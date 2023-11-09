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
            SceneData tempSceneData = Global.activeScene.sceneData;

            Enemy enemy1 = new Enemy(EnemyType.Strong, new Vector2(100, 50));
            tempSceneData.gameObjects.Add(enemy1);
            tempSceneData.enemies.Add(enemy1);

            Enemy enemy2 = new Enemy(EnemyType.Fast, new Vector2(30, 100));
            tempSceneData.gameObjects.Add(enemy2);
            tempSceneData.enemies.Add(enemy2);

            Tower acherTower = new Tower(new Vector2(300, 200), 1f, 300f, "Placeholder\\Parts\\beam6");
            tempSceneData.gameObjects.Add(acherTower);
            tempSceneData.towers.Add(acherTower);
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
