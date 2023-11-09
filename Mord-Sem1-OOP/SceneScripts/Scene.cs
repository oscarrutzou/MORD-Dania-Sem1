using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace MordSem1OOP.SceneScripts
{
    public abstract class Scene
    {
        protected ContentManager content;
        public List<GameObject> gameObjects = new List<GameObject>();
        //public List<GameObject> objectsToCreate = new List<GameObject>();
        //public List<GameObject> objectsToDestroy = new List<GameObject>();

        public Scene(ContentManager content)
        {
            this.content = content;
        }

        #region Methods

        public abstract void Initialize();

        /// <summary>
        /// Update is called every frame
        /// </summary>
        /// <param name="gameTime">Used to get the time elapsed between each frame</param>
        public virtual void Update(GameTime gameTime)
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

        /// <summary>
        /// Calls Draw on every GameObject in the scene
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(spriteBatch);
        }


        #endregion


    }
}
