using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MordSem1OOP.Scripts;
using Mx2L.MonoDebugUI;
using System;
using System.Collections.Generic;

namespace MordSem1OOP
{
    public class GameWorld : Game
    {
        public static ContentManager content;
        protected static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        private static Scene[] scenes = new Scene[1];
        public int activeScene; //Used to call the methods in the current scene

        public Path path;
        public Waypoint currWaypoint;

        public GameWorld()
        {
            InputManager.world = this;

            scenes[0] = new Scene(Content);
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        #region Standard Methods

        protected override void Initialize()
        {


            activeScene = 0;
            scenes[activeScene].Initialize();

            path = new Path(
                new Waypoint(new Vector2(50, 50), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(100, 50), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(100, 150), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(300, 150), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(300, 75), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(500, 75), new Vector2Int(1, 1))
                );

            path.ConnectWaypoints();
            currWaypoint = path.GetWaypoint(0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //Send all content textures to a hashset of textures contained in the Sprite class
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

            _spriteBatch.Begin();

            bool loop = true;
            while (loop)
            {
                loop = currWaypoint.GetNextWaypoint(out Waypoint nextWaypoint);
                
            }
            //scenes[activeScene].Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void Bruh()
        {

        }

        #endregion

        #region Spawning & Despawning Methods
        /// <summary>
        /// Adds a GameObject to the list of GameObjects to be added in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be added</param>
        public void Instantiate(GameObject gameObject) => scenes[activeScene].objectsToCreate.Add(gameObject);


        /// <summary>
        /// Adds a collection of GameObjects to the list of GameObjects to be added in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject collection to be added</param>
        public void Instantiate(GameObject[] gameObjects) => scenes[activeScene].objectsToCreate.AddRange(gameObjects);


        /// <summary>
        /// Adds a GameObject to the list of GameObjects to be removed in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be removed</param>
        public void Destroy(GameObject gameObject) => scenes[activeScene].objectsToDestroy.Add(gameObject);


        /// <summary>
        /// Adds a collection of GameObjects to the list of GameObjects to be removed in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be removed</param>
        public void Destroy(GameObject[] gameObjects) => scenes[activeScene].objectsToDestroy.AddRange(gameObjects);

        #endregion

        #region Scene management Methods

        /// <summary>
        /// Clears the list of GameObjects in a scene
        /// </summary>
        /// <param name="sceneNumber">The scene whose GameObjects will be removed</param>
        public static void ResetScene(int sceneNumber)
        {
            foreach (GameObject gameObject in scenes[sceneNumber].gameObjects)
                scenes[sceneNumber].objectsToCreate.Add(gameObject);
        }

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