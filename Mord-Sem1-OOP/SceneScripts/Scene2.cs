﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
            Global.gameWorld.Instantiate(enemy1);

            Enemy enemy2 = new Enemy(EnemyType.Fast, new Vector2(30, 100));
            Global.gameWorld.Instantiate(enemy2);


            //Acher acherTower = new Acher(new Vector2(300, 200), 1f, 300f, GlobalTextures.Textures[TextureNames.Tower_Acher]);
            //tempSceneData.gameObjectsToAdd.Add(acherTower);

            MissileLauncher missileLauncher = new MissileLauncher(new Vector2(300, 200), 1f, 300f, GlobalTextures.Textures[TextureNames.Tower_MissileLauncher]);
            Global.gameWorld.Instantiate(missileLauncher);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); //Handles the GameObject list
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); //Draws all elements in the GameObject list
        }
    }
}
