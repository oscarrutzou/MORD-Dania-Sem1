using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MordSem1OOP.Scripts.Interface;
using MordSem1OOP.Scripts.Towers;
using MordSem1OOP.Scripts.Waves;
using Mx2L.MonoDebugUI;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace MordSem1OOP.Scripts
{
    public class StatsGui : Gui
    {
        private int _healthBarLength = 250;
        private int _healthBarHeight = 20;
        private Rectangle healthBarRectangle;
        private Vector2 leftScreenPosition = Vector2.One * 20;
        private float rowSpacing = 16;
        private int row = 0;
        private Sprite towerSprite;

        private Vector2 startBottomRightPos;
        /// <summary>
        /// Show dmg on bullet insted
        /// </summary>
        private Button lvlUpBtn;
        string lvlUpBtnText;


        private Button sellBtn;
        string sellBtnText;

        private Button waveBtn;
        private bool hasStartedFirstWave = false;

        //Text for the Wave number text
        private float waveNumberTextScale = 1f;
        private bool scaleUp = true;
        /// <summary>
        /// Can be used to reset when the "Wave (number)" should appear.
        /// </summary>
        public int nextWaveTextExpansionCount = 2;

        private Button[] selectTowerbuttons = new Button[2]; //Used to init each button only once in the correct position.

        #region HealthBar + Extra strings
        public void DrawHealthBar(Vector2 position)
        {
            float maxHealth = Global.activeScene.sceneData.sceneStats.maxHealth;
            float health = Global.activeScene.sceneData.sceneStats.Health;

            healthBarRectangle = new Rectangle((int)position.X, (int)position.Y, _healthBarLength, _healthBarHeight);

            float healthWidth = health / maxHealth * _healthBarLength;
            Rectangle healthFill = healthBarRectangle;

            int padding = 4;
            healthFill.Width = (int)healthWidth;
            healthFill.X += padding;
            healthFill.Y += padding;
            healthFill.Width -= padding * 2;
            healthFill.Height -= padding * 2;

            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.Pixel], healthBarRectangle, new Color(57, 57, 57));
            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.Pixel], healthFill, Color.Red);
        }

        private void DrawStringUnderHealthBar()
        {
            Vector2 goldPos = leftScreenPosition + new Vector2(5, rowSpacing * row++);
            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                                  $"{Global.activeScene.sceneData.sceneStats.money} gold",
                                  goldPos,
                                  Color.White,
                                  0,
                                  Vector2.Zero,
                                  1.2f,
                                  SpriteEffects.None,
                                  1);

            Vector2 totalKillPos = goldPos + new Vector2(_healthBarLength / 2, 0);
            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                      $"{Global.activeScene.sceneData.sceneStats.killCount} kills",
                      totalKillPos,
                      Color.White,
                      0,
                      Vector2.Zero,
                      1.2f,
                      SpriteEffects.None,
                      1);
        }
        #endregion

        #region TowerStats
        public void DrawTowerStats(Vector2 position)
        {
            towerSprite = new Sprite(GlobalTextures.Textures[TextureNames.GuiBasicTowerStats], false);
            towerSprite.Draw(position, 0f, 1f, 0.5f);
            DrawTextInTowerStats(position);


            Vector2 upgradeBtn = position + new Vector2(towerSprite.Rectangle.Width / 2 - 1, 155);
            UpgradeTowerBtn(upgradeBtn);

            Vector2 sellBtnPos = upgradeBtn + new Vector2(0, 55);
            SellTowerBtn(sellBtnPos);
            
        }

        private void DrawTextInTowerStats(Vector2 position)
        {
            string towerKillsText = $"Tower has killed: {InputManager.selectedTower.towerData.towerKills}";
            string towerProjectileDmgText = $"Tower projectile dmg: {InputManager.selectedTower.ProjectileDmg}";

            Vector2 killTextPos = position + new Vector2(20, 32);
            Vector2 towerProjectileDmgPos = killTextPos + new Vector2(0, 57);

            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                                  towerKillsText,
                                  killTextPos,
                                  Color.Black,
                                  0,
                                  Vector2.Zero,
                                  1f,
                                  SpriteEffects.None,
                                  1);

            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                      towerProjectileDmgText,
                      towerProjectileDmgPos,
                      Color.Black,
                      0,
                      Vector2.Zero,
                      1f,
                      SpriteEffects.None,
                      1);
        }

        private void SellTowerBtn(Vector2 position)
        {
            sellBtnText = $"Sell tower: {InputManager.selectedTower.towerData.CalculateSellAmount()} gold";
            if (sellBtn == null) 
            {
                sellBtn = new Button(position,
                     sellBtnText,
                     GlobalTextures.Textures[TextureNames.GuiBasicButton],
                     () => ActionSellTowerBtn());

                Global.activeScene.sceneData.buttons.Add(sellBtn);
            }
            sellBtn.text = sellBtnText;
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
            if (!InputManager.selectedTower.towerData.IsMaxLvl())
            {
                lvlUpBtnText = $"Level Up: {InputManager.selectedTower.towerData.CalculateLevelUpBuyAmount()} gold";
            }
            else
            {
                lvlUpBtnText = "Max Level";
            }

            if (lvlUpBtn == null)
            {
                lvlUpBtn = new Button(position,
                     lvlUpBtnText,
                     GlobalTextures.Textures[TextureNames.GuiBasicButton],
                     () => ActionUpgradeTowerBtn());
                Global.activeScene.sceneData.buttons.Add(lvlUpBtn);
            }
            lvlUpBtn.text = lvlUpBtnText;

            lvlUpBtn.Draw();
        }

        private void ActionUpgradeTowerBtn()
        {
            InputManager.selectedTower.LevelUpTower();

        }
        #endregion

        #region Other Btn's
        public void SelectTowerBtn(int selectionIndex)
        {
            Texture2D waveBtnTexture = GlobalTextures.Textures[TextureNames.GuiBasicButton];
            Vector2 topRightPos = Global.gameWorld.Camera.TopRight + new Vector2(-(waveBtnTexture.Width / 2 + 10), waveBtnTexture.Height);

            //The button can only be init once (Have to be here since this is called in the ScreenDraw
            if (selectTowerbuttons[selectionIndex] == null)
            {
                switch (selectionIndex)
                {
                    case 0:
                        selectTowerbuttons[selectionIndex] = new Button(topRightPos,
                                                                        $"Gun Turret price: {GunTurret.towerBuyAmount}",
                                                                        waveBtnTexture,
                                                                        () => SelectTowerBtnAction(selectionIndex));
                        break;

                    case 1:
                        topRightPos += new Vector2(0, waveBtnTexture.Height + 10); //Makes the button go down a little from btn 0.
                        selectTowerbuttons[selectionIndex] = new Button(topRightPos,
                                                                        $"Cannon Turret price: {CannonTurret.towerBuyAmount}",
                                                                        waveBtnTexture,
                                                                        () => SelectTowerBtnAction(selectionIndex));
                        break;
                }
                //Adds to data of the scene, if we want to access or remove it easily
                Global.activeScene.sceneData.buttons.Add(selectTowerbuttons[selectionIndex]);
            }

            selectTowerbuttons[selectionIndex].Draw();
        }

        private void SelectTowerBtnAction(int index)
        {
            Global.activeScene.sceneData.buildGui.ChangeTowerIndex(index + 1);
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
                //WaveManager.StartNextWave();
            }
            else
            {
                WaveManager.StartNextWave();
            }
        }
        #endregion

        public void DrawTowerRing()
        {
            Sprite radiusRing = new Sprite(GlobalTextures.Textures[TextureNames.TowerEffect_RadiusRing]);

            Vector2 drawPosition = InputManager.selectedTower.Position - radiusRing.Origin;


            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.TowerEffect_RadiusRing],
                                        drawPosition,
                                        null,
                                        Color.OrangeRed * 0.5f,
                                        0,
                                        Vector2.Zero,
                                        1f,
                                        SpriteEffects.None,
                                        1);
        }

        private void NextWaveText(Vector2 position)
        {
            string newxtWaveText = $"Wave: {WaveManager.batchCount}";
            Vector2 textSize = GlobalTextures.arialFont.MeasureString(newxtWaveText);
            Vector2 textPosition = (position + new Vector2(GameWorld._graphics.PreferredBackBufferWidth / 2, 50)) - (textSize * waveNumberTextScale) / 2;

            if (waveNumberTextScale < 1.5f && scaleUp)
            {
                waveNumberTextScale += 0.01f;
            }
            else
            {
                scaleUp = false;
                waveNumberTextScale -= 0.01f;
                if (waveNumberTextScale <= 1f)
                {
                    scaleUp = true;
                    nextWaveTextExpansionCount++;

                }
            }

            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont,
                      newxtWaveText,
                      textPosition,
                      Color.White,
                      0,
                      Vector2.Zero,
                      waveNumberTextScale,
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


            leftScreenPosition = Vector2.One * 20;
            rowSpacing = 16;
            row = 0;

            DrawHealthBar(leftScreenPosition + new Vector2(0, rowSpacing * row++));

            leftScreenPosition.Y += 10;
            DrawStringUnderHealthBar();


            leftScreenPosition.Y += 20;
            
            if (InputManager.selectedTower != null)
            {
                Vector2 towerStatsPos = leftScreenPosition + new Vector2(0, rowSpacing * row++);
                DrawTowerStats(towerStatsPos);
            }

            SelectTowerBtn(0);
            SelectTowerBtn(1);

            if (nextWaveTextExpansionCount < 2)
            {
                NextWaveText(Vector2.Zero);
            }
            //WaveButton();
        }


        public override void Update(GameTime gameTime)
        {
            foreach (Button button in Global.activeScene.sceneData.buttons)
            {
                button.Update(gameTime);
            }
            //goldCounter.Update(gameTime);
        }

        public override void Draw()
        {

        }
    }
}
