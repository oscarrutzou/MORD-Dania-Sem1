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
    public class Scene : GameWorld
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        public List<GameObject> objectsToCreate = new List<GameObject>();
        public List<GameObject> objectsToDestroy = new List<GameObject>();


        #region Methods
        protected override void LoadContent()
        {
            //Call LoadContent on every GameObject in the active scene.
            foreach (GameObject gameObject in gameObjects)
                gameObject.LoadContent(Content);
        }
        
        protected override void Update(GameTime gameTime)
        {
            //All GameObjects to be added, are added to the active scene.
            foreach (GameObject gameObject in objectsToCreate)
                gameObjects.Add(gameObject);
            objectsToCreate.Clear();

            //All GameObjects to be destroyed, are removed from the active scene.
            foreach (GameObject gameObject in objectsToDestroy)
                gameObjects.Add(gameObject);
            objectsToDestroy.Clear();

            //Call update on every GameObject in the active scene.
            foreach (GameObject gameObject in gameObjects)
                gameObject.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Call draw on every GameObject in the active scene.
            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(_spriteBatch);
        }
        #endregion


    }
}
