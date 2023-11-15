using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MordSem1OOP.Scripts.Interface;
using MordSem1OOP.Scripts.Towers;
using MordSem1OOP.Scripts.Waves;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace MordSem1OOP.Scripts
{
    public class StatsGui : Gui
    {
        private int _healthBarLength = 250;
        private int _healthBarHeight = 20;

        private Vector2 leftScreenPosition = Vector2.One * 20;
        private float rowSpacing = 16;
        private int row = 0;
        Sprite towerSprite;

        private Vector2 startBottomRightPos;
        Button lvlUpBtn;
        Button sellBtn;
        Button waveBtn;
        private bool hasStartedFirstWave = false;
        //ISprite towerSprite;
        public void DrawHealthBar(Vector2 position)
        {
            float maxHealth = Global.activeScene.sceneData.sceneStats.maxHealth;
            float health = Global.activeScene.sceneData.sceneStats.Health;

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
            towerSprite = new Sprite(GlobalTextures.Textures[TextureNames.GuiBasicTowerStats], false);
            //towerSprite.Color = Color.White;
            //towerSprite.Scale = 1f;

            string towerKillsText = $"Tower has killed: {InputManager.selectedTower.towerData.towerKills}";

            Vector2 textPosition = position + new Vector2(10, 15);

            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                                  towerKillsText,
                                  textPosition,
                                  Color.White,
                                  0,
                                  Vector2.Zero,
                                  1,
                                  SpriteEffects.None,
                                  1);


            Vector2 upgradeBtn = new Vector2(300, 100);
            UpgradeTowerBtn(upgradeBtn);

            Vector2 sellBtnPos = new Vector2(300, 150);
            SellTowerBtn(sellBtnPos);
        }

        private void SellTowerBtn(Vector2 position)
        {
            if (sellBtn == null) 
            {
                sellBtn = new Button(position,
                     $"Sell tower: {InputManager.selectedTower.towerData.CalculateSellAmount()} gold",
                     GlobalTextures.Textures[TextureNames.GuiBasicButton],
                     () => ActionSellTowerBtn());

                Global.activeScene.sceneData.buttons.Add(sellBtn);
            }

            sellBtn.Draw();
        }

        private void ActionSellTowerBtn()
        {
            InputManager.selectedTower.towerData.SellTower();
            Global.activeScene.sceneData.tileGrid.RemoveTile(InputManager.selectedTile.GridPosition);
            InputManager.selectedTower.IsRemoved = true;
            InputManager.selectedTower = null;
        }

        private void UpgradeTowerBtn(Vector2 position)
        {
            if (lvlUpBtn == null)
            {
                lvlUpBtn = new Button(position,
                     $"Level Up: {InputManager.selectedTower.towerData.CalculateLevelUpBuyAmount()} gold",
                     GlobalTextures.Textures[TextureNames.GuiBasicButton],
                     () => ActionUpgradeTowerBtn());

                Global.activeScene.sceneData.buttons.Add(lvlUpBtn);
            }

            lvlUpBtn.Draw();
        }

        private void ActionUpgradeTowerBtn()
        {
            InputManager.selectedTower.LevelUpTower();

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
                                        1f,
                                        SpriteEffects.None,
                                        1);
        }

        public void WaveButton()
        {
            Texture2D waveBtnTexture = GlobalTextures.Textures[TextureNames.GuiBasicButton];

            if (waveBtn == null)
            {
                startBottomRightPos = Global.gameWorld.Camera.BottomRight - new Vector2(waveBtnTexture.Width / 2 + 10, waveBtnTexture.Height / 2 + 10);
                

                waveBtn = new Button(startBottomRightPos, "Start Wave!", waveBtnTexture, () => StartWaveBtnAction());
                Global.activeScene.sceneData.buttons.Add(waveBtn);
            }

            waveBtn.Draw();
        }

        private void StartWaveBtnAction()
        {
            if (!hasStartedFirstWave)
            {
                WaveManager.Begin(0); //Start the first wave
                hasStartedFirstWave = true;
            }
            else
            {
                WaveManager.StartNextWave();
            }
        }

        public void WorldDraw()
        {
            if (InputManager.selectedTower == null) return;

            DrawTowerRing();
        }


        public void ScreenDraw()
        {
            leftScreenPosition = Vector2.One * 20;
            rowSpacing = 16;
            row = 0;

            DrawHealthBar(leftScreenPosition + new Vector2(0, rowSpacing * row++));
            leftScreenPosition.Y += 10;
            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                                              $"{Global.activeScene.sceneData.sceneStats.money} gold",
                                              leftScreenPosition + new Vector2(0, rowSpacing * row++),
                                              Color.White,
                                              0,
                                              Vector2.Zero,
                                              1.2f,
                                              SpriteEffects.None,
                                              1);

            leftScreenPosition.Y += 20;

            if (InputManager.selectedTower != null)
            {
                Vector2 towerStatsPos = leftScreenPosition + new Vector2(0, rowSpacing * row++);
                DrawTowerStats(towerStatsPos);
                towerSprite.Draw(towerStatsPos, 0f);

            }

            WaveButton();

        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in Global.activeScene.sceneData.buttons)
            {
                button.Update(gameTime);
            }
        }

        public override void Draw()
        {

        }
    }
}
