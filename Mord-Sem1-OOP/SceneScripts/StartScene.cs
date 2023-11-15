using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MordSem1OOP.Scripts;
using static System.Net.Mime.MediaTypeNames;

namespace MordSem1OOP.SceneScripts
{
    internal sealed class StartScene : Scene
    {

        private TileGrid _tileGrid;
        private StatsGui _statsGui;

        private Button startButton;
        private Vector2 screenCenter;

        public StartScene(ContentManager content) : base(content) { }

        public override void Initialize()
        {
            screenCenter = new Vector2(GameWorld._graphics.PreferredBackBufferWidth / 2,
                                        GameWorld._graphics.PreferredBackBufferHeight / 2);

            startButton = new Button(screenCenter, "Start Game!", GlobalTextures.Textures[TextureNames.GuiBasicButton],
                                    () => LoadGameScene()); 

            GameWorld.Instantiate(startButton);



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
            startButton.Draw();
            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.GameTitle], (screenCenter - new Vector2(256, 150)), Color.White);
            GameWorld._spriteBatch.End();
        }

        private void LoadGameScene()
        {
            GameWorld.scenes[8].Initialize(); 
            Global.gameWorld.activeScene = 8; 
            Global.activeScene = GameWorld.scenes[8]; 
        }
    }
}
