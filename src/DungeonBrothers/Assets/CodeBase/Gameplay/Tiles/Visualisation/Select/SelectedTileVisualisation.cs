using System;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation.Select
{
    public class SelectedTileVisualisation : ISelectedTileVisualisation
    {
        private readonly ITileSelector _tileSelector;
        private readonly ITurnQueue _turnQueue;
        private readonly ICustomLogger _customLogger;
        
        private readonly CompositeDisposable _disposable = new();

        private readonly TileColorConfig _tileColorConfig;
        
        public SelectedTileVisualisation(ITileSelector tileSelector,
            ITurnQueue turnQueue,
            ICustomLogger customLogger,
            IStaticDataProvider staticDataProvider)
        {
            _tileSelector = tileSelector;
            _turnQueue = turnQueue;
            _customLogger = customLogger;
            _tileColorConfig = staticDataProvider.TileColorConfig;
        }

        public void Initialize()
        {
            _tileSelector.CurrentTile
                .Skip(1)
                .Where(tile => tile != _tileSelector.PreviousTile.Value)
                .Where(tile => tile != null)
                .Subscribe(VisualizeSelectedTile)
                .AddTo(_disposable);
            
            _tileSelector.PreviousTile
                .Skip(1)
                .Where(tile => tile != null)
                .Subscribe(tile =>
                {
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
            tile.View.SwitchOutLine(true);
            tile.View.ChangeOutLineColor(_tileColorConfig.SelectedTile);
        }

        private void ResetTileView(Tile tile) => 
            tile.View.SwitchOutLine(false);


        private void ResetTileWithCharacter(Tile previousTile)
        {
            switch (previousTile.Logic.Character.CharacterTeam)
            {
                case CharacterTeam.Enemy:
                    previousTile.View.ChangeOutLineColor(_tileColorConfig.EnemyTile);
                    break;
                case CharacterTeam.Ally:
                    previousTile.View.ChangeOutLineColor(_tileColorConfig.AllyTile);
                    break;
                default:
                    _customLogger.LogError(
                        new Exception($"{previousTile.Logic.Character.CharacterTeam}, not found"));
                    break;
            }
        }

        private bool TryResetTileView(Tile previousTile) =>
            previousTile.Logic.Character != _turnQueue.ActiveCharacter.Value;
    }
}