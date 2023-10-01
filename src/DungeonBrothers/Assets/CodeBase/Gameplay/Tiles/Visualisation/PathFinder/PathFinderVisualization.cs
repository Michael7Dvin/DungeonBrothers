using System.Collections.Generic;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation.PathFinder
{
    public class PathFinderVisualization : IPathFinderVisualization
    {
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly IMoverService _moverService;
        private readonly ITurnQueue _turnQueue;
        private readonly TileColorConfig _tileColorConfig;

        private readonly CompositeDisposable _disposable = new();

        private readonly List<Tile> _lastTiles = new();

        public PathFinderVisualization(IPathFinder pathFinder,
            IMapService mapService,
            ITurnQueue turnQueue,
            IStaticDataProvider staticDataProvider)
        {
            _pathFinder = pathFinder;
            _mapService = mapService;;
            _turnQueue = turnQueue;
            _tileColorConfig = staticDataProvider.TileColorConfig;
        }

        public void Initialize()
        {
            _pathFinder.PathFindingResults
                .Skip(1)
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
                    tile.View.ChangeHighlightColor(_tileColorConfig.WalkableColorTile);
                }
            }
        }

        private void ResetLastTilesView()
        {
            if (_lastTiles.Count > 0)
            {
                foreach (Tile tile in _lastTiles)
                {
                    if (_turnQueue.ActiveCharacter.Value.Coordinate == tile.Logic.Coordinates)
                        continue;

                    tile.View.ResetTileView();
                }
            }
        }
    }
}