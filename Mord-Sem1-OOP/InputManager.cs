using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MordSem1OOP
{
    public static class InputManager
    {
        private static KeyboardState keyboardState;
        private static MouseState mouseState;

        public static Vector2 mousePosition;

        public static void HandleInput(Game game, Camera camera)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            // Handle camera movement based on keyboard input //-- look at
            Vector2 moveDirection = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
                moveDirection.Y = 1;
            if (keyboardState.IsKeyDown(Keys.S))
                moveDirection.Y = -1;
            if (keyboardState.IsKeyDown(Keys.A))
                moveDirection.X = 1;
            if (keyboardState.IsKeyDown(Keys.D))
                moveDirection.X = -1;

            camera.Move(moveDirection * 5); // Control camera speed //-- look at



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
