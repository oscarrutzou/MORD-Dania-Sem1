using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
                             0);
        }

        public static void DrawRectangleFrame(SpriteBatch spriteBatch, Vector2 position, Rectangle rectangle, Color color, float thickness, float angle)
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

        public static void DrawRectangle(SpriteBatch spriteBatch, Vector2 position, Rectangle rectangle, float angle, Color color)
        {
            rectangle.X += (int)position.X;
            rectangle.Y += (int)position.Y;

            DrawRectangle(spriteBatch, rectangle, angle, color);
        }
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, float angle, Color color)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            spriteBatch.Draw(pixel, rectangle, null, color, angle, Vector2.Zero, SpriteEffects.None, 1);
        }

        private static Texture2D CreateCircleTexture(GraphicsDevice graphicsDevice, int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(graphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }

        public static void DrawCircle(SpriteBatch spriteBatch, Vector2 position, float radius, Color color)
        {
            Texture2D circleTexture = CreateCircleTexture(spriteBatch.GraphicsDevice, (int)radius);
            Vector2 origin = new Vector2(circleTexture.Width / 2, circleTexture.Height / 2);
            spriteBatch.Draw(circleTexture, position, null, color * 0.5f, 0, origin, 1, SpriteEffects.None, 1);
        }

        private static void CreateThePixel(SpriteBatch spriteBatch)
        {
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
        }
    }
}
