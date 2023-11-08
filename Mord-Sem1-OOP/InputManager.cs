using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MordSem1OOP
{
    public static class InputManager
    {
        public static GameWorld world;
        private static KeyboardState keyboardState;
        private static MouseState mouseState;

        public static Vector2 mousePosition;

        public static void HandleInput(Game game)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();


            if(keyboardState.IsKeyDown(Keys.Space))
            {
                switch (world.activeScene)
                {
                    case 0: world.Instantiate(new Enemy(EnemyType.Normal, new Vector2(100,100), world.Content)); break;
                    default: throw new Exception();    
                }
            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                // The left mouse button is pressed
                // You can get the position of the mouse click like this:
                mousePosition = new Vector2(mouseState.X, mouseState.Y);
            }
        }

    }
}
