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
    internal sealed class Scene3 : Scene
    {
        public Scene3(ContentManager content) : base(content) { }

        public override void Initialize()
        {

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
