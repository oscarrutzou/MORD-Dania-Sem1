using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mx2L.MonoDebugUI;
using System.Collections.Generic;

namespace MordSem1OOP
{
    public class GameWorld : Game
    {
        protected static GraphicsDeviceManager _graphics;
        protected static SpriteBatch _spriteBatch;
        private static Scene[] scenes = new Scene[1];
        private int activeScene; //Used to call the methods in the current scene


        public GameWorld()
        {
            scenes[0] = new Scene();
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #region Standard Methods
        protected override void Initialize()
        {
            base.Initialize();
            activeScene = 0;
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            scenes[activeScene].LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.HandleInput(this);


            scenes[activeScene].Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);


            scenes[activeScene].Draw(gameTime);
            base.Draw(gameTime);
        }

        #endregion

        #region Spawning & Despawning Methods
        /// <summary>
        /// Adds a GameObject to the list of GameObjects to be added in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be added</param>
        public void Instantiate(GameObject gameObject)
        {
            scenes[activeScene].objectsToCreate.Add(gameObject);
            gameObject.LoadContent(Content);
        }

        /// <summary>
        /// Adds a collection of GameObjects to the list of GameObjects to be added in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject collection to be added</param>
        public void Instantiate(GameObject[] gameObjects)
        {
            scenes[activeScene].objectsToCreate.AddRange(gameObjects);
            foreach (GameObject gameObject in gameObjects) gameObject.LoadContent(Content);
        }

        /// <summary>
        /// Adds a GameObject to the list of GameObjects to be removed in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be removed</param>
        public void Destroy(GameObject gameObject)
        {
            scenes[activeScene].objectsToDestroy.Add(gameObject);
        }

        /// <summary>
        /// Adds a collection of GameObjects to the list of GameObjects to be removed in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be removed</param>
        public void Destroy(GameObject[] gameObjects)
        {
            scenes[activeScene].objectsToDestroy.AddRange(gameObjects);
        }
        #endregion

    }
}