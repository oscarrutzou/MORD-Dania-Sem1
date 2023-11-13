using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using MordSem1OOP.Scripts.Interface;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    /// <summary>
    /// The button should stop things from happening under the button
    /// </summary>
    public class Button : GameObject
    {

        public Button(Vector2 position, float scale, Texture2D texture): base(texture)
        {
            Position = position;
            Scale = scale;


        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
