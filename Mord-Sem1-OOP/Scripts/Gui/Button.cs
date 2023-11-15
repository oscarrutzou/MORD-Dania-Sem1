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
    public class Button: Gui
    {
        public string text;
        public bool IsRemoved { get; set; }

        private Action onClickAction;
        private float scale = 1f;
        private Vector2 position;
        private ISprite sprite;

        private float clickCooldown = 0.5f; // The delay between button clicks in seconds
        private float timeSinceLastClick = 0; // The time since the button was last clicked
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)(position.X - sprite.Width / 2 * Scale),
                    (int)(position.Y - sprite.Height / 2 * Scale),
                    (int)(sprite.Width * Scale),
                    (int)(sprite.Height * Scale)
                    );
            }
        }

        public float Scale { get => scale; set => scale = value; }

        public Button(Vector2 position, string text, Texture2D texture, Action onClickAction)
        {
            this.position = position;
            this.text = text;
            sprite = new Sprite(texture);
            this.onClickAction = onClickAction;
        }

        public Button(Vector2 position, string text, bool setOrgingToCenter, Texture2D texture, Action onClickAction)
        {
            this.position = position;
            this.text = text;
            sprite = new Sprite(texture, setOrgingToCenter);
            this.onClickAction = onClickAction;
        }


        public Button(Vector2 position, string text, float timeBetweenPress, Texture2D texture, Action onClickAction)
        {
            this.position = position;
            this.text = text;
            clickCooldown = timeBetweenPress;
            sprite = new Sprite(texture);
            this.onClickAction = onClickAction;
        }

        public override void Update(GameTime gameTime)
        {
            if (timeSinceLastClick < clickCooldown)
            {
                timeSinceLastClick += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (!IsMouseOver() || InputManager.mouseState.LeftButton == ButtonState.Released)
            {
                Scale = Math.Min(1, Scale + 0.01f);  // Increase the scale by 1% each frame, up to the original size
            }
        }

        public virtual void OnClick()
        {
            Scale = 0.9f;  // Shrink the button by 10%

            // Only invoke the action if enough time has passed since the last click
            if (timeSinceLastClick >= clickCooldown)
            {
                onClickAction?.Invoke(); //Invokes the action (method) that was the input from the       
                timeSinceLastClick = 0; // Reset the time since the last click
            }
        }

        public bool IsMouseOver()
        {
            return CollisionBox.Contains(InputManager.mousePositionOnScreen.ToPoint());
        }

        public override void Draw()
        {
            sprite.Draw(position, 0f, Scale);

            // Measure the size of the text
            Vector2 textSize = GlobalTextures.arialFont.MeasureString(text);

            // Calculate the position to center the text
            Vector2 textPosition = position - textSize / 2;

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
