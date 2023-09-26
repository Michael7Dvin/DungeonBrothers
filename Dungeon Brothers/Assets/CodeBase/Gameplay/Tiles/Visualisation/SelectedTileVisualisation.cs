using System;
using CodeBase.Gameplay.Characters;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation
{
    public class SelectedTileVisualisation : ISelectedTileVisualisation
    {
        private readonly ITileSelector _tileSelector;
        
        public SelectedTileVisualisation(ITileSelector tileSelector)
        {
            _tileSelector = tileSelector;
        }

        public void Initialize()
        {
            _tileSelector.CurrentTile.Changed += VisualizeSelectedTile;
            _tileSelector.PreviousTile.Changed += ResetLastTile;
        }

        public void Disable()
        {
            _tileSelector.CurrentTile.Changed += VisualizeSelectedTile;
            _tileSelector.PreviousTile.Changed += ResetLastTile;
        }
        
        
        private void VisualizeSelectedTile(Tile tile)
        {
            if (tile == _tileSelector.PreviousTile.Value)
                return;
            
            tile.TileView.SwitchOutLine(true);
            tile.TileView.ChangeOutLineColor(Color.yellow);
        }

        private void ResetLastTile(Tile tile)
        {
            _tileSelector.PreviousTile.Value.TileView.ResetTileView();
        }
    }
}