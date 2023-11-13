using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MordSem1OOP.Scripts
{
    internal class StatsGui : Gui
    {
        private int _healthBarLength = 250;
        private int _healthBarHeight = 20;

        public void DrawHealthBar(Vector2 position, SpriteBatch spriteBatch)
        {
            float maxHealth = Global.activeScene.sceneData.sceneStats.maxHealth;
            float health = Global.activeScene.sceneData.sceneStats.health;

            Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, _healthBarLength, _healthBarHeight);

            float healthWidth = health / maxHealth * _healthBarLength;
            Rectangle healthFill = rectangle;

            int padding = 4;
            healthFill.Width = (int)healthWidth;
            healthFill.X += padding;
            healthFill.Y += padding;
            healthFill.Width -= padding * 2;
            healthFill.Height -= padding * 2;

            spriteBatch.Draw(GlobalTextures.Textures[TextureNames.Pixel], rectangle, new Color(57, 57, 57));
            spriteBatch.Draw(GlobalTextures.Textures[TextureNames.Pixel], healthFill, Color.Red);
        }

        public override void Draw()
        {
            SpriteBatch spriteBatch = GameWorld._spriteBatch;

            Vector2 position = Vector2.One * 20;
            float rowSpacing = 16;
            int row = 0;

            DrawHealthBar(position + new Vector2(0, rowSpacing * row++), spriteBatch);
            position.Y += 10;
            spriteBatch.DrawString(GlobalTextures.arialFont, $"{Global.activeScene.sceneData.sceneStats.money} gold", position + new Vector2(0, rowSpacing * row++), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        public override void Update()
        {

        }
    }
}
