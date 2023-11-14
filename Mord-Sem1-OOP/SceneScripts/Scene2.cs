using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MordSem1OOP.Scripts;

namespace MordSem1OOP.SceneScripts
{
    internal sealed class Scene2 : Scene
    {
        private StatsGui statsGui;

        public Scene2(ContentManager content) : base(content) { }

        public override void Initialize()
        {
            statsGui = new StatsGui();

            Enemy enemy1 = new Enemy(EnemyType.Strong, new Vector2(100, 50));
            GameWorld.Instantiate(enemy1);

            Enemy enemy2 = new Enemy(EnemyType.Fast, new Vector2(30, 100));
            GameWorld.Instantiate(enemy2);

            Enemy enemy3 = new Enemy(EnemyType.Normal, new Vector2(-600, 0));
            GameWorld.Instantiate(enemy3);

            Archer acherTower = new Archer(new Vector2(300, 0), 1f, GlobalTextures.Textures[TextureNames.Tower_Archer]);
            GameWorld.Instantiate(acherTower);

            MissileLauncher missileLauncher = new MissileLauncher(new Vector2(300, 110), 1f, GlobalTextures.Textures[TextureNames.Tower_MissileLauncher]);
            GameWorld.Instantiate(missileLauncher);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); //Handles the GameObject list
        }

        public override void Draw()
        {
            base.Draw(); //Draws all elements in the GameObject list
        }

        public override void DrawScene()
        {
            GameWorld._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: Global.gameWorld.Camera.GetMatrix());
            Draw();
            sceneData._statsGui.WorldDraw();
            GameWorld._spriteBatch.End();

            GameWorld._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            sceneData._statsGui.ScreenDraw();
            GameWorld._spriteBatch.End();
        }
    }
}
