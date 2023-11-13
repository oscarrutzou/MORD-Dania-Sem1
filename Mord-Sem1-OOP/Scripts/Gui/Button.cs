using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MordSem1OOP.Scripts;
using MordSem1OOP.Scripts.Interface;
using SharpDX.Direct2D1;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
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
        private string text;
        private Action onClickAction;

        public Button(Vector2 position, string text, Texture2D texture, Action onClickAction) : base(texture)
        {
            Position = position;
            Scale = 1;
            this.text = text;
            this.onClickAction = onClickAction;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsMouseOver() || InputManager.mouseState.LeftButton == ButtonState.Released)
            {
                Scale = Math.Min(1, Scale + 0.01f);  // Increase the scale by 1% each frame, up to the original size
            }
        }

        public virtual void OnClick()
        {
            Scale = 0.9f;  // Shrink the button by 10%
            onClickAction?.Invoke(); //Invokes the action (method) that was the input from the contructer
        }

        public bool IsMouseOver()
        {
            return CollisionBox.Contains(InputManager.mousePosition.ToPoint());
        }

        public override void Draw()
        {
            base.Draw();

            // Measure the size of the text
            Vector2 textSize = GlobalTextures.arialFont.MeasureString(text);

            // Calculate the position to center the text
            Vector2 textPosition = Position - textSize / 2;

            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                                              text,
                                              textPosition,
                                              Color.Black,
                                              0,
                                              Vector2.Zero,
                                              1,
                                              SpriteEffects.None,
                                              1);
        }
    }
}
