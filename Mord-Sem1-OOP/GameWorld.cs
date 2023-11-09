using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MordSem1OOP.SceneScripts;
using MordSem1OOP.Scripts;
using Mx2L.MonoDebugUI;
using System;
using System.Collections.Generic;

namespace MordSem1OOP
{
    public class GameWorld : Game
    {
        protected static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        private static Scene[] scenes = new Scene[7];
        public int activeScene; //Used to call the methods in the current scene

        private Camera camera;

        public Path path;
        public Waypoint currWaypoint;

        public GameWorld()
        {
            Global.gameWorld = this;

            scenes[0] = new StartScene(Content);
            scenes[1] = new GameScene(Content);
            scenes[2] = new Scene1(Content); //Michael's scene
            scenes[3] = new Scene2(Content); //Oscar's scene
            scenes[4] = new Scene3(Content); //Gaming's scene
            scenes[5] = new Scene4(Content); //David's scene
            scenes[6] = new Scene5(Content); //Jacob's scene
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #region Standard Methods

        protected override void Initialize()
        {
            camera = new Camera(_graphics);
            activeScene = 2;
            Global.activeScene = scenes[activeScene]; //Very important since this sets what scene data that the code should use
            
            scenes[activeScene].Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //Send all content textures to a hashset of textures contained in the Sprite class
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.HandleInput(this, camera);

            scenes[activeScene].Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            _spriteBatch.Begin(transformMatrix: camera.GetMatrix());
            scenes[activeScene].Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Spawning & Despawning Methods
        /// <summary>
        /// Adds a GameObject to the list of GameObjects to be added in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be added</param>
        public void Instantiate(GameObject gameObject) => Global.activeScene.sceneData.gameObjectsToAdd.Add(gameObject);


        /// <summary>
        /// Adds a collection of GameObjects to the list of GameObjects to be added in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject collection to be added</param>
        public void Instantiate(GameObject[] gameObjects) => Global.activeScene.sceneData.gameObjectsToAdd.AddRange(gameObjects);


        /// <summary>
        /// Adds a GameObject to the list of GameObjects to be removed in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be removed</param>
        //public void Destroy(GameObject gameObject) => scenes[activeScene].objectsToDestroy.Add(gameObject);


        /// <summary>
        /// Adds a collection of GameObjects to the list of GameObjects to be removed in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be removed</param>
        //public void Destroy(GameObject[] gameObjectsToDestroy)
        //{
        //    foreach (GameObject gameObjectToDestroy in gameObjectsToDestroy)
        //    {
        //        scenes[activeScene].gameObjects.gam;
        //    }
        //    //GameObject[] gameObjects) => scenes[activeScene].gameObjects.AddRange(gameObjects
        //}

        #endregion

        #region Scene management Methods

        /// <summary>
        /// Clears the list of GameObjects in a scene
        /// </summary>
        /// <param name="sceneNumber">The scene whose GameObjects will be removed</param>
        //public static void ResetScene(int sceneNumber)
        //{
        //    foreach (GameObject gameObject in scenes[sceneNumber].gameObjects)
        //        scenes[sceneNumber].objectsToCreate.Add(gameObject);
        //}

        /// <summary>
        /// Unloads all assets and loads assets for every GameObject in the chosen scene
        /// </summary>
        /// <param name="sceneNumber">The chosen scene</param>
        public void ChangeScene(int sceneNumber)
        {
            if (sceneNumber >= scenes.Length)
                throw new ArgumentOutOfRangeException("sceneNumber", sceneNumber, "Chosen scene is out of bounds of the array");

            Content.Unload();
            activeScene = sceneNumber;

            //Set all sprite textures somehow?
        }
        #endregion
    }
}