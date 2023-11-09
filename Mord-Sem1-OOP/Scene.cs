using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace MordSem1OOP
{
    public class Scene
    {


        private ContentManager content;
        public List<GameObject> gameObjects = new List<GameObject>();
        //public List<GameObject> objectsToCreate = new List<GameObject>();
        //public List<GameObject> objectsToDestroy = new List<GameObject>();



        public Scene(ContentManager content)
        {
            this.content = content;
        }

        #region Methods

        public void Initialize()
        {
            Enemy enemy1 = new Enemy(EnemyType.Strong, new Vector2(100, 50), content);
            gameObjects.Add(enemy1);
            Global.enemies.Add(enemy1);

            Enemy enemy2 = new Enemy(EnemyType.Fast, new Vector2(30, 100), content);
            gameObjects.Add(enemy2);
            Global.enemies.Add(enemy2);

            Tower acherTower = new Tower(new Vector2(300,200), 1f, 300f, content, "Placeholder\\Parts\\beam6");
            gameObjects.Add(acherTower);
            //gameObjects.Add(new Tower_Arrow(new Vector2(50, 300), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            //gameObjects.Add(new Tower_Arrow(new Vector2(400, 30), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            //gameObjects.Add(new Tower_Arrow(new Vector2(10,10), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            //gameObjects.Add(new Tower_Arrow(new Vector2(600,300), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            //gameObjects.Add(new Tower_Arrow(new Vector2(600,600), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
        }

        /// <summary>
        /// Update is called every frame
        /// </summary>
        /// <param name="gameTime">Used to get the time elapsed between each frame</param>
        public void Update(GameTime gameTime)
        {
            //All GameObjects to be added, are added to the active scene.
            foreach (GameObject gameObject in Global.gameObjectsToCreate)
                gameObjects.Add(gameObject);
            Global.gameObjectsToCreate.Clear();

            //All GameObjects to be destroyed, are removed from the active scene.
            gameObjects = gameObjects.Where(gameObject => !gameObject.IsRemoved).ToList();

            //Also remove from Global.enemies if it's an Enemy
            Global.enemies = Global.enemies.Where(enemy => !enemy.IsRemoved).ToList();

            //Call update on every GameObject in the active scene.
            foreach (GameObject gameObject in gameObjects)
                gameObject.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Call draw on every GameObject in the active scene.
            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(spriteBatch);
        }


        #endregion


    }
}
