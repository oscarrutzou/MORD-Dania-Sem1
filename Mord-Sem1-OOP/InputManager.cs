﻿using System;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MordSem1OOP.Scripts;

namespace MordSem1OOP
{
    public static class InputManager
    {
        //public static GameWorld world;
        private static KeyboardState keyboardState;
        public static MouseState mouseState;

        public static Vector2 mousePosition;

        public static void HandleInput(Game game, Camera camera)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            //Sets the mouse position
            mousePosition = GetMousePositionInWorld();

            // Handle camera movement based on keyboard input //-- look at
            Vector2 moveDirection = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
                moveDirection.Y = -1;
            if (keyboardState.IsKeyDown(Keys.S))
                moveDirection.Y = 1;
            if (keyboardState.IsKeyDown(Keys.A))
                moveDirection.X = -1;
            if (keyboardState.IsKeyDown(Keys.D))
                moveDirection.X = 1;

            camera.Move(moveDirection * 5); // Control camera speed //-- look at

            if (keyboardState.IsKeyDown(Keys.D1))
                Global.activeScene.sceneData._buildGui.ChangeTowerIndex(1);
            if (keyboardState.IsKeyDown(Keys.D2))
                Global.activeScene.sceneData._buildGui.ChangeTowerIndex(2);


            if (keyboardState.IsKeyDown(Keys.Space))
            {
                //Should be able to add a new enemy
                //Global.activeScene.sceneData.gameObjectsToAdd.Add(new Enemy(EnemyType.Normal, new Vector2(100, 100), Global.gameWorld.Content));

            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {

                // The left mouse button is pressed
                // You can get the position of the mouse click like this:
                if (Global.activeScene.sceneData.towers == null) return;

                foreach(Button button in Global.activeScene.sceneData.buttons)
                {
                    if (button.IsMouseOver())
                    {
                        button.OnClick();
                    }
                }
            }
        }

        /// <summary>
        /// Translates the mouse's position into world space coordinates.
        /// </summary>
        /// <returns></returns>
        private static Vector2 GetMousePositionInWorld()
        {
            Vector2 pos = new Vector2(mouseState.X, mouseState.Y);
            Matrix invMatrix = Matrix.Invert(Global.gameWorld.Camera.GetMatrix());

            return Vector2.Transform(pos, invMatrix);
        } 

    }
}
