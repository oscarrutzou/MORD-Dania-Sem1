using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MordSem1OOP.Scripts.Towers;
using Mx2L.MonoDebugUI;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    public class BuildGui : Gui
    {
        private int _selectionIndex;
        private TileGrid _tileGrid;

        private int tempCost;
        private Tower _selectedTower;
        public BuildGui(TileGrid tileGrid)
        {
            _tileGrid = tileGrid;
        }

        public override void Update(GameTime gameTime)
        {
            _selectedTower = GetTower(_selectionIndex);

            if (InputManager.mouseState.LeftButton == ButtonState.Pressed && !InputManager.IsMouseOverButton())
                Build();
        }

        public void Build()
        {
            Vector2 position = InputManager.mousePosition;

            if (!_tileGrid.IsTileAvailable(position, out Vector2Int gridPosition))
                return;

            if (_selectedTower is null)
                return;

            if (!CanAffordTower()) return;

            _selectedTower.towerData.BuyTower(); //Deducts the money from the sceneStats
            GameWorld.Instantiate(_selectedTower);
            _tileGrid.Insert(_selectedTower, gridPosition);


        }

        public override void Draw()
        {
            Vector2 position = InputManager.mousePosition;

            bool canPlaceOnGrid = _tileGrid.IsTileAvailable(position, out Vector2Int gridPosition);

            if (!canPlaceOnGrid)
                return;

            Rectangle tileRect = new Rectangle(
                            (int)(gridPosition.X * _tileGrid.TileSize + _tileGrid.TileSize / 4),
                            (int)(gridPosition.Y * _tileGrid.TileSize + _tileGrid.TileSize / 4),
                            (int)(_tileGrid.TileSize / 2),
                            (int)(_tileGrid.TileSize / 2)
                        );

            tileRect.X += (int)_tileGrid.Position.X;
            tileRect.Y += (int)_tileGrid.Position.Y;
            if (InputManager.IsMouseOverButton()) return;

            Primitives2D.DrawSolidRectangle(GameWorld._spriteBatch, tileRect, 0, CanAffordTower() ? Color.Green : Color.Red);
        }

        public void ChangeTowerIndex(int index)
        {
            _selectionIndex = index;
        }

        public bool CanAffordTower()
        {
            return Global.activeScene.sceneData.sceneStats.money >= _selectedTower.towerData.BuyAmount();
        }

        private Tower GetTower(int index)
        {
            switch (index)
            {
                case 1:
                    return CreateArcher();
                case 2:
                    return CreateMissileLauncher();
                default:
                    return CreateArcher();
            }
        }

        private Archer CreateArcher()
        {
            Archer archerTower = new Archer(new Vector2(0, 0), .5f, GlobalTextures.Textures[TextureNames.Tower_Archer]);
            return archerTower;
        }

        private Tower CreateMissileLauncher()
        {
            MissileLauncher missileLauncher = new MissileLauncher(new Vector2(0, 0), .5f, GlobalTextures.Textures[TextureNames.Tower_MissileLauncher]);
            return missileLauncher;
        }

        public void AddToDebug()
        {
            DebugInfo.AddString("tileGridPosition", DebugTileGridPosition);
            DebugInfo.AddString("mousePosition", MousePosition);
        }

        public string DebugTileGridPosition()
        {
            Vector2Int gridPosition = _tileGrid.GetTileGridPosition(InputManager.mousePosition);
            return $"({gridPosition.X}, {gridPosition.Y})";
        }

        public string MousePosition()
        {
            Vector2 position = InputManager.mousePosition;
            return $"({position.X}, {position.Y})";
        }
    }
}
