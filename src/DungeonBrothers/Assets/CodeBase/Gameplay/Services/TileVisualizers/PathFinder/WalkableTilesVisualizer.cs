using System.Collections.Generic;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Services.TileVisualizers.PathFinder
{
    public class WalkableTilesVisualizer : IWalkableTilesVisualizer
    {
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly IMoverService _moverService;
        private readonly ITurnQueue _turnQueue;
        private readonly TileColorsConfig _tileColorsConfig;

        private readonly CompositeDisposable _disposable = new();
        private readonly List<Tile> _lastTiles = new();

        public WalkableTilesVisualizer(IPathFinder pathFinder,
            IMapService mapService,
            ITurnQueue turnQueue,
            IStaticDataProvider staticDataProvider)
        {
            _pathFinder = pathFinder;
            _mapService = mapService;
            _turnQueue = turnQueue;
            _tileColorsConfig = staticDataProvider.TileColorsConfig;
        }

        public void Initialize()
        {
            _turnQueue.NewTurnStarted
                .Where(_ => _turnQueue.ActiveCharacter.Value.Team == CharacterTeam.Enemy)
                .Subscribe(_ => ResetLastTilesView())
                .AddTo(_disposable);
            
            _pathFinder.PathFindingResults
                .Skip(1)
                .Where(_ => _turnQueue.ActiveCharacter.Value.Team == CharacterTeam.Ally)
                .Subscribe(VisualizeWalkableTiles)
                .AddTo(_disposable);
        }

        public void Disable()
        {
            _disposable.Clear();
        }

        private void VisualizeWalkableTiles(PathFindingResults pathFindingResults)
        {
            ResetLastTilesView();
            
            foreach (Vector2Int coordinate in pathFindingResults.WalkableCoordinates)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile))
                {
                    _lastTiles.Add(tile);
                    
                    tile.View.SwitchHighlight(true);
                    tile.View.ChangeHighlightColor(_tileColorsConfig.WalkableColorTile);
                }
            }
        }

        private void ResetLastTilesView()
        {
            foreach (Tile tile in _lastTiles) 
            {
                if (_turnQueue.ActiveCharacter.Value.Coordinate == tile.Logic.Coordinates) 
                    continue;
                
                tile.View.ResetTileView();
            }
                
            _lastTiles.Clear();
        }
    }
}