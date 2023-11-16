using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.SceneScripts
{
    internal sealed class EndScene : Scene
    {
        private StatsGui _statsGui;

        private Button gameExitButton;
        private Vector2 screenCenter;
        private AnimatedCounter counter;


        public EndScene(ContentManager content) : base(content) { }

        public override void Initialize()
        {

            screenCenter = new Vector2(GameWorld._graphics.PreferredBackBufferWidth / 2,
                                        GameWorld._graphics.PreferredBackBufferHeight / 2 + 100);

            gameExitButton = new Button(screenCenter + new Vector2(0, 50), "Quit game", GlobalTextures.Textures[TextureNames.GuiBasicButton],
                                    () => QuitGame());

            GameWorld.Instantiate(gameExitButton);

            sceneData.statsGui = _statsGui = new StatsGui();
            if (sceneData.sceneStats.Score < 999999)
                AnimatedCounter.numberString = sceneData.sceneStats.Score.ToString();
            else AnimatedCounter.numberString = 999999.ToString();

            counter = new AnimatedCounter(screenCenter);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); //Handles the GameObject list
            counter.Update(gameTime);

        }

        public override void DrawScene()
        {
            //Draw UI
            GameWorld._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            gameExitButton.Draw();
            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.DeathTitle], (screenCenter - new Vector2(256, 300)), Color.White);
            
            Vector2 counterPos = Vector2.Zero;
            counterPos.X -= AnimatedCounter.numberPillarSprite.Width * 3;
            counterPos.Y -= AnimatedCounter.numberPillarSprite.Height / 11;
            counter.Draw(counterPos);

            GameWorld._spriteBatch.End();
        }

        private void QuitGame()
        {
            Global.gameWorld.Exit();
        }
    }
}
