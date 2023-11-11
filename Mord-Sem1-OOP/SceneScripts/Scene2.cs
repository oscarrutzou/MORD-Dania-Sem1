using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.SceneScripts
{
    internal sealed class Scene2 : Scene
    {


        public Scene2(ContentManager content) : base(content) { }

        public override void Initialize()
        {

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
    }
}
