using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation
{
    public class SelectedTileVisualisation : ISelectedTileVisualisation
    {
        private readonly ITileSelector _tileSelector;
        private readonly ITurnQueue _turnQueue;
        private readonly IPathFinder _pathFinder;
        private readonly ICustomLogger _customLogger;

        private readonly TileColorConfig _tileColorConfig;
        
        public SelectedTileVisualisation(ITileSelector tileSelector,
            ITurnQueue turnQueue,
            IPathFinder pathFinder,
            ICustomLogger customLogger,
            IStaticDataProvider staticDataProvider)
        {
            _tileSelector = tileSelector;
            _turnQueue = turnQueue;
            _pathFinder = pathFinder;
            _customLogger = customLogger;
            _tileColorConfig = staticDataProvider.TileColorConfig;
        }

        public void Initialize()
        {
            _tileSelector.CurrentTile.Changed += VisualizeSelectedTile;
            _tileSelector.CurrentTile.Changed += ResetLastTile;
        }

        public void Disable()
        {
            _tileSelector.CurrentTile.Changed -= VisualizeSelectedTile;
            _tileSelector.CurrentTile.Changed -= ResetLastTile;
        }
        
        
        private void VisualizeSelectedTile(Tile tile)
        {
            if (tile == _tileSelector.PreviousTile.Value)
                return;
            
            tile.TileView.SwitchOutLine(true);
            tile.TileView.ChangeOutLineColor(_tileColorConfig.SelectedTileColor);
        }

        private void ResetLastTile(Tile tile)
        {
            Tile previousTile = _tileSelector.PreviousTile.Value;
            
            if (previousTile == null)
                return;

            if (TryResetMovableTile(previousTile))
            {
                previousTile.TileView.SwitchOutLine(false);
                return;
            }

            if (TryResetTileView(previousTile))
            {
                previousTile.TileView.ResetTileView();
                return;
            }

            ResetTileWithCharacter(previousTile);
        }

        private void ResetTileWithCharacter(Tile previousTile)
        {
            switch (previousTile.Character.CharacterTeam)
            {
                case CharacterTeam.Enemy:
                    previousTile.TileView.ChangeOutLineColor(_tileColorConfig.EnemyTile);
                    break;
                case CharacterTeam.Ally:
                    previousTile.TileView.ChangeOutLineColor(_tileColorConfig.AllyTile);
                    break;
                default:
                    _customLogger.LogError(new Exception($"{previousTile.Character.CharacterTeam}, not found"));
                    break;
            }
        }

        private bool TryResetMovableTile(Tile previousTile) =>
            _pathFinder.PathFindingResults.Value.IsMovableAt(previousTile.Coordinates);
        
        private bool TryResetTileView(Tile previousTile) =>
            previousTile.Character == null || previousTile.Character != _turnQueue.ActiveCharacter.Value;
    }
}