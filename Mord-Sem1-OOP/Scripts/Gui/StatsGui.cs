using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts.Towers;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using static System.Net.Mime.MediaTypeNames;

namespace MordSem1OOP.Scripts
{
    public class StatsGui : Gui
    {
        private int _healthBarLength = 250;
        private int _healthBarHeight = 20;

        private Vector2 position = Vector2.One * 20;
        private float rowSpacing = 16;
        private int row = 0;

        public void DrawHealthBar(Vector2 position)
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

            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.Pixel], rectangle, new Color(57, 57, 57));
            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.Pixel], healthFill, Color.Red);
        }

        public void DrawTowerStats(Vector2 position)
        {
            //Sprite radiusRing = new Sprite(GlobalTextures.Textures[TextureNames.TowerEffect_RadiusRing]);

            //Vector2 drawPosition = Position - radiusRing.Origin;


            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.GuiBasicTowerStats], position, Color.Red);

            string towerKills = InputManager.selectedTower.towerData.towerKills.ToString();
            // Measure the size of the text
            //Vector2 textSize = GlobalTextures.arialFont.MeasureString(towerKills);

            // Calculate the position to center the text
            Vector2 textPosition = position + Vector2.One * 10;

            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                                  towerKills,
                                  textPosition,
                                  Color.Black,
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  100);



        }

        public void DrawTowerRing()
        {
            Sprite radiusRing = new Sprite(GlobalTextures.Textures[TextureNames.TowerEffect_RadiusRing]);

            Vector2 drawPosition = InputManager.selectedTower.Position - radiusRing.Origin;

            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.TowerEffect_RadiusRing],
                                        drawPosition,
                                        null,
                                        Color.Black,
                                        0,
                                        Vector2.Zero,
                                        1,
                                        SpriteEffects.None,
                                        1);
        }

        public void WorldDraw()
        {
            if (InputManager.selectedTower == null) return;

            DrawTowerRing();
        }

        public void ScreenDraw()
        {
            position = Vector2.One * 20;
            rowSpacing = 16;
            row = 0;

            DrawHealthBar(position + new Vector2(0, rowSpacing * row++));
            position.Y += 10;
            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                                              $"{Global.activeScene.sceneData.sceneStats.money} gold",
                                              position + new Vector2(0, rowSpacing * row++),
                                              Color.Black,
                                              0,
                                              Vector2.Zero,
                                              1,
                                              SpriteEffects.None,
                                              10);

            position.Y += 20;

            if (InputManager.selectedTower == null) return;

            DrawTowerStats(position + new Vector2(0, rowSpacing * row++));
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            
        }
    }
}
