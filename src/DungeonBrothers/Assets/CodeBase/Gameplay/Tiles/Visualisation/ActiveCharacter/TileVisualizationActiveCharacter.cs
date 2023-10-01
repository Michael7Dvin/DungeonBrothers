using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation.ActiveCharacter
{
    public class TileVisualizationActiveCharacter : ITileVisualizationActiveCharacter
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMapService _mapService;
        private readonly ICustomLogger _customLogger;
        private readonly IMoverService _moverService;
        private readonly TileColorConfig _tileColorConfig;

        private readonly CompositeDisposable _disposable = new();

        private Tile _lastTile;
        private ICharacter _lastActiveCharacter;

        public TileVisualizationActiveCharacter(ITurnQueue turnQueue, 
            IMapService mapService,
            ICustomLogger customLogger,
            IMoverService moverService,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _mapService = mapService;
            _customLogger = customLogger;
            _moverService = moverService;
            _tileColorConfig = staticDataProvider.TileColorConfig;
        }

        public void Initialize()
        {
            _turnQueue.ActiveCharacter
                .Skip(1)
                .Subscribe(HighlightOutlineTile)
                .AddTo(_disposable);

            _moverService.IsMoved
                .Subscribe(HighlightOutlineTile)
                .AddTo(_disposable);
        }

        public void CleanUp() => 
            _disposable.Clear();

        private void HighlightOutlineTile(ICharacter character)
        {
            if (_lastTile != null)
                DisableHighlightOutline();
            
            if (_mapService.TryGetTile(character.Coordinate, out Tile tile))
            {
                _lastTile = tile;
                
                switch (character.CharacterTeam)
                {
                    case CharacterTeam.Enemy:
                    {
                        ActiveHighlightOutline(_lastTile);
                        _lastTile.View.ChangeHighlightColor(_tileColorConfig.EnemyTile);
                        _lastTile.View.ChangeOutLineColor(_tileColorConfig.EnemyTile);
                        break;
                    }
                    case CharacterTeam.Ally:
                    {
                        ActiveHighlightOutline(_lastTile);
                        _lastTile.View.ChangeOutLineColor(_tileColorConfig.AllyTile);
                        _lastTile.View.ChangeHighlightColor(_tileColorConfig.AllyTile);
                        break;
                    }
                    default:
                        _customLogger.LogError(new Exception($"{nameof(character.CharacterTeam)}, does not exist"));
                        break;
                
                    
                }
            }
        }

        private void ActiveHighlightOutline(Tile tile)
        {
            tile.View.SwitchHighlight(true);
            tile.View.SwitchOutLine(true);
        }
        
        private void DisableHighlightOutline()
        {
            _lastTile.View.SwitchHighlight(false);
            _lastTile.View.SwitchOutLine(false);
            _lastTile.View.ChangeOutLineColor(Color.white);
        }
    }
}