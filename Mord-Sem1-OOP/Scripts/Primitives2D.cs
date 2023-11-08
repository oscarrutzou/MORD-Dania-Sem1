using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MordSem1OOP.Scripts
{
    public static class Primitives2D
    {
        private static Texture2D pixel;

        /*                    what does "this" do
         * public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
         */

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
        {
            // calculate the distance between the two vectors
            float distance = Vector2.Distance(point1, point2);

            // calculate the angle between the two vectors
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            spriteBatch.Draw(pixel,
                             point,
                             null,
                             color,
                             angle,
                             Vector2.Zero,
                             new Vector2(length, thickness),
                             SpriteEffects.None,
                             1);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Vector2 position, Rectangle rectangle, Color color, float thickness, float angle)
        {
            Vector2 topLeft = new Vector2(rectangle.X, rectangle.Y);
            Vector2 topRight = new Vector2(rectangle.Width, rectangle.Y);
            Vector2 bottomLeft = new Vector2(rectangle.X, rectangle.Height);
            Vector2 bottomRight = new Vector2(rectangle.Width, rectangle.Height);

            Matrix matrix = Matrix.CreateRotationZ(angle);
            topLeft = Vector2.Transform(topLeft, matrix);
            topRight = Vector2.Transform(topRight, matrix);
            bottomLeft = Vector2.Transform(bottomLeft, matrix);
            bottomRight = Vector2.Transform(bottomRight, matrix);

            DrawLine(spriteBatch, position + topLeft, position + topRight, color, thickness);
            DrawLine(spriteBatch, position + bottomLeft, position + bottomRight, color, thickness);
            DrawLine(spriteBatch, position + topLeft, position + bottomLeft, color, thickness);
            DrawLine(spriteBatch, position + topRight, position + bottomRight, color, thickness);
        }

        private static void CreateThePixel(SpriteBatch spriteBatch)
        {
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
        }
    }
}
