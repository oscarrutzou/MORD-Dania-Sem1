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
        /// <summary>
        /// Prevents multiple click when clicking a button
        /// </summary>
        public static MouseState previousMouseState;


        public static Vector2 mousePosition;
        public static Vector2 mousePositionOnScreen;
        public static Tower selectedTower;
        public static Tile selectedTile;
        public static void HandleInput(Game game, Camera camera)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            //Sets the mouse position
            mousePosition = GetMousePositionInWorld();
            mousePositionOnScreen = new Vector2(mouseState.X, mouseState.Y);

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
                Global.activeScene.sceneData.buildGui.ChangeTowerIndex(1);
            if (keyboardState.IsKeyDown(Keys.D2))
                Global.activeScene.sceneData.buildGui.ChangeTowerIndex(2);


            if (keyboardState.IsKeyDown(Keys.Space))
            {
               
            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                CheckButtons();
                CheckTowers();
            }

            previousMouseState = mouseState;

            
        }
        /// <summary>
        /// Scuffed. Is gonna get fixed 
        /// </summary>
        private static void CheckButtons()
        {
            if (Global.activeScene.sceneData.buttons != null)
            {
                foreach (Button button in Global.activeScene.sceneData.buttons)
                {
                    if (button.IsMouseOver())
                    {
                        button.OnClick();
                        return;  // Return early if a button was clicked
                    }
                }
            }
        }
        private static void CheckTowers()
        {
            // Only check for towers if no button was clicked
            if (!IsMouseOverButton())
            {
                if (Global.activeScene.sceneData.towers == null) return; //There isn't any towers in the scene yet

                Global.activeScene.sceneData.tileGrid.GetTile(mousePosition, out selectedTile);

                if (selectedTile is EntityTile entityTile)
                {
                    if (entityTile.GameObject is Tower tower)
                    {
                        selectedTower = tower;
                    }
                    else
                    {
                        selectedTower = null;
                    }
                }
                else
                {
                    selectedTower = null;
                }
            }
        }

        /// <summary>
        /// Make this to a IsMouseOverGui. So its also if you try to click on stuff like a stat menu. And make it into one with CheckButton
        /// </summary>
        /// <returns></returns>
        public static bool IsMouseOverButton()
        {
            if (Global.activeScene.sceneData.buttons != null)
            {
                foreach (Button button in Global.activeScene.sceneData.buttons)
                {
                    if (button.IsMouseOver())
                    {
                        return true;
                    }
                }
            }

            return false;
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
