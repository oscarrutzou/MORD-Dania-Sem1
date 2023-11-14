using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MordSem1OOP.SceneScripts;
using MordSem1OOP.Scripts;
using MordSem1OOP.Scripts.Waves;
using Mx2L.MonoDebugUI;
using System;
using System.Collections.Generic;

namespace MordSem1OOP
{
    public class GameWorld : Game
    {
        protected static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        private static Scene[] scenes = new Scene[9];
        public int activeScene; //Used to call the methods in the current scene

        private Camera camera;
        public Camera Camera { get => camera; }

        public Path path;
        public Waypoint currWaypoint;


        public GameWorld()
        {
            Global.gameWorld = this;

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }




        #region Standard Methods

        protected override void Initialize()
        {
            //ChangeScreenSize();
            GenerateScenes();
            activeScene = 8;
            Global.activeScene = scenes[activeScene]; //Very important since this sets what scene data that the code should use

            camera = new Camera(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2));
            GlobalTextures.LoadContent(Content); //This must be read before scenes[].Initialize, because that line attempts to load a texture.
            scenes[activeScene].Initialize();

            //WaveManager.CreateWaves(); //This method defines how many waves are in the game.
            //WaveManager.Begin(0); //Start the first wave


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        
        protected override void Update(GameTime gameTime)
        {
            InputManager.HandleInput(this, camera);
            //WaveManager.Update(gameTime);
            

            scenes[activeScene].Update(gameTime);


            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            scenes[activeScene].DrawScene();

            base.Draw(gameTime);
        }

        #endregion

        #region Spawning & Despawning Methods
        /// <summary>
        /// Adds a GameObject to the list of GameObjects to be added in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject to be added</param>
        public static void Instantiate(GameObject gameObject) => Global.activeScene.sceneData.gameObjectsToAdd.Add(gameObject);


        /// <summary>
        /// Adds a collection of GameObjects to the list of GameObjects to be added in the next update.
        /// </summary>
        /// <param name="gameObject">The GameObject collection to be added</param>
        public static void Instantiate(GameObject[] gameObjects) => Global.activeScene.sceneData.gameObjectsToAdd.AddRange(gameObjects);
        #endregion

        /// <summary>
        /// Initializes the different scenes. 
        /// If you want to add more scenes, do it here.
        /// </summary>
        private void GenerateScenes()
        {
            scenes[0] = new StartScene(Content);
            scenes[1] = new GameScene(Content);
            scenes[2] = new Scene1(Content); //Michael's scene
            scenes[3] = new Scene2(Content); //Oscar's scene
            scenes[4] = new Scene3(Content); //Gaming's scene
            scenes[5] = new Scene4(Content); //David's scene
            scenes[6] = new Scene5(Content); //Jacob's scene
            scenes[7] = new TileGridTest(Content);
            scenes[8] = new Map1(Content);
        }

        public void Fullscreen()
        {
            _graphics.HardwareModeSwitch = false;
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //camera.SetOrigin(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2));
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        public void WindowedScreen()
        {
            _graphics.HardwareModeSwitch = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            //camera.SetOrigin(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2));
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        public string DebugResolution()
        {
            return $"{_graphics.PreferredBackBufferWidth}, {_graphics.PreferredBackBufferHeight}";
        }
    }
}