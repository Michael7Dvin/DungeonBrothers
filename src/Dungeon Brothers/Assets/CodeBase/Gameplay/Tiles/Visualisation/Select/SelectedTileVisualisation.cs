using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation
{
    public class SelectedTileVisualisation : ISelectedTileVisualisation
    {
        private readonly ITileSelector _tileSelector;
        private readonly ITurnQueue _turnQueue;
        private readonly IPathFinder _pathFinder;
        private readonly ICustomLogger _customLogger;
        
        private readonly CompositeDisposable _disposable = new();

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
            _tileSelector.CurrentTile
                .Where(tile => tile != _tileSelector.PreviousTile.Value)
                .Subscribe(VisualizeSelectedTile)
                .AddTo(_disposable);

            _tileSelector.PreviousTile
                .Where(tile => tile != null)
                .Subscribe(tile =>
                {
                    if (TryResetMovableTile(tile))
                    {
                        ResetMovableTile(tile);
                        return;
                    }

                    if (TryResetTileView(tile))
                    {
                        ResetTileView(tile);
                        return;
                    }

                    ResetTileWithCharacter(tile);
                })
                .AddTo(_disposable);
        }

        public void Disable() => 
            _disposable.Clear();


        private void VisualizeSelectedTile(Tile tile)
        {
            tile.TileView.SwitchOutLine(true);
            tile.TileView.ChangeOutLineColor(_tileColorConfig.SelectedTileColor);
        }

        private void ResetMovableTile(Tile tile) =>
            tile.TileView.SwitchOutLine(false);

        private void ResetTileView(Tile tile) =>
            tile.TileView.ResetTileView();
        
        private void ResetTileWithCharacter(Tile previousTile)
        {
            switch (previousTile.TileLogic.Character.CharacterTeam)
            {
                case CharacterTeam.Enemy:
                    previousTile.TileView.ChangeOutLineColor(_tileColorConfig.EnemyTile);
                    break;
                case CharacterTeam.Ally:
                    previousTile.TileView.ChangeOutLineColor(_tileColorConfig.AllyTile);
                    break;
                default:
                    _customLogger.LogError(
                        new Exception($"{previousTile.TileLogic.Character.CharacterTeam}, not found"));
                    break;
            }
        }

        private bool TryResetMovableTile(Tile previousTile) =>
            _pathFinder.PathFindingResults.Value.IsMovableAt(previousTile.TileLogic.Coordinates);
        
        private bool TryResetTileView(Tile previousTile) =>
            previousTile.TileLogic.Character != _turnQueue.ActiveCharacter.Value;
    }
}