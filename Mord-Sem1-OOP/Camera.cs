﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MordSem1OOP
{
    public class Camera
    {
        public Vector2 position;          // The camera's position in the game world.
        private float zoom;                // The zoom level of the camera.
        private Matrix transformMatrix;    // A transformation matrix used for rendering.
        public Vector2 _origin;

        public Camera(Vector2 origin)
        {
            position = Vector2.Zero;   // Initialize the camera's position at the origin.
            zoom = 1.0f;               // Initialize the camera's zoom level to 1.0
            _origin = origin;
        }


        //public Vector2 TopLeft
        //{
        //    get { return position - new Vector2(GameWorld._graphics.PreferredBackBufferWidth / 2, GameWorld._graphics.PreferredBackBufferHeight / 2); }
        //}

        public Vector2 TopRight
        {
            get { return position + new Vector2(GameWorld._graphics.PreferredBackBufferWidth, 0); }
        }

        //public Vector2 BottomLeft
        //{
        //    get { return position - new Vector2(-GameWorld._graphics.PreferredBackBufferWidth / 2, GameWorld._graphics.PreferredBackBufferHeight / 2); }
        //}

        public Vector2 TopMiddle
        {
            get { return position + new Vector2(GameWorld._graphics.PreferredBackBufferWidth / 2, 0); }
        }


        public Vector2 BottomRight
        {
            get { return position + new Vector2(GameWorld._graphics.PreferredBackBufferWidth, GameWorld._graphics.PreferredBackBufferHeight); }
        }


        public void Move(Vector2 delta)
        {
            // Update the camera's position by adding a delta vector.
            position += delta;
        }
        public Matrix GetMatrix()
        {
            // Create a transformation matrix that represents the camera's view.
            // This matrix is used to adjust rendering based on the camera's position and zoom level.

            // 1. Translate to the negative of the camera's position.
            Matrix translationMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0));

            // 2. Scale the view based on the camera's zoom level.
            Matrix scaleMatrix = Matrix.CreateScale(zoom);

            // 3. Translate the view to center it on the screen.
            // This assumes the camera view is centered within the game window.
            // The following lines center the view using the screen's dimensions.
            Matrix centerMatrix = Matrix.CreateTranslation(new Vector3(_origin.X, _origin.Y, 0));

            // Combine the matrices in the correct order to create the final transformation matrix.
            transformMatrix = translationMatrix * scaleMatrix * centerMatrix;

            return transformMatrix; // Return the transformation matrix for rendering.
        }

        public void SetOrigin(Vector2 origin)
        {
            _origin = origin;
        }
    }
}



