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
        public SceneData sceneData = new SceneData();
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
            SceneData tempSceneData = Global.activeScene.sceneData;

            // Clear the gameObjects list
            tempSceneData.gameObjects.Clear();

            // Add all towers, enemies, and projectiles to the gameObjects list
            tempSceneData.gameObjects.AddRange(tempSceneData.towers.Where(tower => !tower.IsRemoved));
            tempSceneData.gameObjects.AddRange(tempSceneData.enemies.Where(enemy => !enemy.IsRemoved));
            // Need to cast the gameobject IProjectile interface to a gameobject to be able to see if its should be removed from the list 

            tempSceneData.gameObjects.AddRange(tempSceneData.projectiles.Where(projectile => !projectile.IsRemoved));

            // All GameObjects to be added, are added to the active scene.
            foreach (GameObject gameObject in tempSceneData.gameObjectsToAdd)
            {
                if (gameObject is Tower tower)
                {
                    tempSceneData.towers.Add(tower);
                }
                else if (gameObject is Enemy enemy)
                {
                    tempSceneData.enemies.Add(enemy);
                }
                else if (gameObject is Projectile projectile)
                {
                    tempSceneData.projectiles.Add(projectile);
                }
            }
            tempSceneData.gameObjectsToAdd.Clear();

            // Call update on every GameObject in the active scene.
            foreach (GameObject gameObject in tempSceneData.gameObjects)
                gameObject.Update(gameTime);
        }


        /// <summary>
        /// Calls Draw on every GameObject in the scene
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in Global.activeScene.sceneData.gameObjects)
                gameObject.Draw(spriteBatch);
        }


        #endregion


    }
}
