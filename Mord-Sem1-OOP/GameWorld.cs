using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace MordSem1OOP
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private AnimatedCounter counter;
        public static float DeltaTime {  get; private set; }

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AnimatedCounter.numberPillarSprite = Content.Load<Texture2D>("numberPillarSprite");
            counter = new AnimatedCounter(new Vector2((GraphicsDevice.Viewport.Width - AnimatedCounter.numberPillarSprite.Width) / 2, (GraphicsDevice.Viewport.Height - AnimatedCounter.numberPillarSprite.Height)));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            counter.LoadContent(Content);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) 
                Exit();

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            InputManager.HandleInput(this);


            counter.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            spriteBatch.Begin();

            counter.Draw(spriteBatch);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}