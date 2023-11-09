using Microsoft.Xna.Framework;
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
        private Vector2 position;          // The camera's position in the game world.
        private float zoom;                // The zoom level of the camera.
        private Matrix transformMatrix;    // A transformation matrix used for rendering.
        private GraphicsDeviceManager _graphics;  // Reference to the GraphicsDeviceManager.

        public Camera(GraphicsDeviceManager graphics)
        {
            position = Vector2.Zero;   // Initialize the camera's position at the origin.
            zoom = 1.0f;               // Initialize the camera's zoom level to 1.0
            _graphics = graphics;
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
            Matrix centerMatrix = Matrix.CreateTranslation(new Vector3(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2, 0));

            // Combine the matrices in the correct order to create the final transformation matrix.
            transformMatrix = translationMatrix * scaleMatrix * centerMatrix;

            return transformMatrix; // Return the transformation matrix for rendering.
        }
    }
}



