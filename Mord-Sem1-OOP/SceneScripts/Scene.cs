using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using MordSem1OOP.Scripts;

namespace MordSem1OOP.SceneScripts
{
    public abstract class Scene
    {
        protected ContentManager content;
        public SceneData sceneData = new SceneData();

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
            tempSceneData.gameTime = gameTime;

            //Remove GameObjects marked for removal
            RemoveObjects(tempSceneData);
            tempSceneData.gameObjects.Clear();

            //Add GameObjects and sort them into the right categories
            AddObjects(tempSceneData.gameObjects);
            SortIntoCategories(tempSceneData.gameObjectsToAdd);
            tempSceneData.gameObjectsToAdd.Clear();

            // Call update on every GameObject in the active scene.
            foreach (GameObject gameObject in tempSceneData.gameObjects)
                gameObject.Update(gameTime);

            Global.activeScene.sceneData._statsGui.Update(gameTime);
                
        }

        /// <summary>
        /// Calls Draw on every GameObject in the scene
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw()
        {
            foreach (GameObject gameObject in Global.activeScene.sceneData.gameObjects)
                gameObject.Draw();
        }

        public virtual void DrawScene()
        {
            GameWorld._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            Draw();
            GameWorld._spriteBatch.End();
        }


        /// <summary>
        /// Adds each queued GameObject, to the corresponding list of their type.
        /// </summary>
        /// <param name="sceneData">Used to get the different GameObject lists, and to add the GameObjects to them.</param>
        private void SortIntoCategories(List<GameObject> gameObjectsToAdd)
        {
            foreach (GameObject obj in gameObjectsToAdd)
            {
                switch (obj)
                {
                    case Tower:
                        sceneData.towers.Add((Tower)obj);
                        break;
                    case Enemy:
                        sceneData.enemies.Add((Enemy)obj);
                        break;
                    case Projectile:
                        sceneData.projectiles.Add((Projectile)obj);
                        break;
                    //case Button:
                    //    sceneData.buttons.Add((Button)obj);
                    //    break;

                }
            }
        }
        private void AddObjects(List<GameObject> gameObjects)
        {
            gameObjects.AddRange(sceneData.towers);
            gameObjects.AddRange(sceneData.enemies);
            gameObjects.AddRange(sceneData.projectiles);
            //gameObjects.AddRange(sceneData.buttons);
        }
        private void RemoveObjects(SceneData sceneData)
        {
            sceneData.towers.RemoveAll(tower => tower.IsRemoved);
            sceneData.enemies.RemoveAll(enemy => enemy.IsRemoved);
            sceneData.projectiles.RemoveAll(projectile => projectile.IsRemoved);
            //sceneData.buttons.RemoveAll(button => button.IsRemoved);
        }

        #endregion


    }
}
