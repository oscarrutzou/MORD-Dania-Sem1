using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts.Gui
{
    internal class StatsGui : Gui
    {
        public override void Draw()
        {
            SpriteBatch spriteBatch = GameWorld._spriteBatch;

            Vector2 position = Vector2.One * 20;
            float rowSpacing = 16;
            int row = 0;

            spriteBatch.DrawString(GlobalTextures.arialFont, $"{Global.activeScene.sceneData.sceneStats.money} gold", position + new Vector2(0, rowSpacing * row++), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        public override void Update()
        {

        }
    }
}
