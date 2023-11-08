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
        public List<GameObject> objectsToCreate = new List<GameObject>();
        public List<GameObject> objectsToDestroy = new List<GameObject>();



        public Scene(ContentManager content)
        {
            this.content = content;
        }

        #region Methods

        public void Initialize()
        {
            Enemy targetEnemy = new Enemy(EnemyType.Normal, content);
            gameObjects.Add(targetEnemy);
            gameObjects.Add(new Tower_Arrow(new Vector2(50, 300), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
        }

        public void LoadContent()
        {
            //Call LoadContent on every GameObject in the active scene.
            foreach (GameObject gameObject in gameObjects)
                gameObject.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            //All GameObjects to be added, are added to the active scene.
            foreach (GameObject gameObject in objectsToCreate)
                gameObjects.Add(gameObject);
            objectsToCreate.Clear();

            //All GameObjects to be destroyed, are removed from the active scene.
            foreach (GameObject gameObject in objectsToDestroy)
                gameObjects.Remove(gameObject); // Change this line
            objectsToDestroy.Clear();

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
