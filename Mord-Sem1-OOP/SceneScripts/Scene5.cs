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
    internal sealed class Scene5 : Scene
    {
        private TileGrid _tileGrid;
        private StatsGui _statsGui;

        private Button gameExitButton;
        private Vector2 screenCenter;



        public Scene5(ContentManager content) : base(content) { }

        public override void Initialize()
        {
            screenCenter = new Vector2(GameWorld._graphics.PreferredBackBufferWidth / 2,
                                        GameWorld._graphics.PreferredBackBufferHeight / 2 + 100);

            gameExitButton = new Button(screenCenter, "Quit game", GlobalTextures.Textures[TextureNames.GuiBasicButton],
                                    () => QuitGame());

            GameWorld.Instantiate(gameExitButton);


            sceneData.tileGrid = _tileGrid = new TileGrid(new Vector2(32, 64), 64, 32, 21);
            sceneData.statsGui = _statsGui = new StatsGui();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); //Handles the GameObject list

        }

        public override void Draw()
        {
            base.Draw();
        }
        public override void DrawScene()
        {
            //Draw UI
            GameWorld._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            gameExitButton.Draw();
            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.DeathTitle], (screenCenter - new Vector2(256, 300)), Color.White);
            GameWorld._spriteBatch.End();
        }

        private void QuitGame()
        {
            Global.gameWorld.Exit();
        }
    }
}
