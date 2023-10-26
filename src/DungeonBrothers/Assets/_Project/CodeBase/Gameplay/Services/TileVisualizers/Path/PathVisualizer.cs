using System.Collections.Generic;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.InteractionsService;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Services.TileVisualizers.Path
{
    public class PathVisualizer : IPathVisualizer
    {
        private readonly ITileSelector _tileSelector;
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly IInteractionService _interactionService;
        private readonly CompositeDisposable _disposable = new();

        private readonly TileColorsConfig _tileColorsConfig;

        private readonly List<Tile> _lastTiles = new();

        public PathVisualizer(ITileSelector tileSelector, 
            IPathFinder pathFinder,
            IMapService mapService,
            IInteractionService interactionService,
            IStaticDataProvider staticDataProvider)
        {
            _tileSelector = tileSelector;
            _pathFinder = pathFinder;
            _mapService = mapService;
            _interactionService = interactionService;
            _tileColorsConfig = staticDataProvider.TileColorsConfig;
        }

        public void Initialize()
        {
            _tileSelector.CurrentTile
                .Skip(1)
                .Where(tile => tile != null)
                .Where(_ => _interactionService.IsInteract == false)
                .Where(tile => _pathFinder.PathFindingResults.Value.IsMovableAt(tile.Logic.Coordinates))
                .Subscribe(tile =>
                {
                    ResetLastTiles();
                    
                    if (tile == _tileSelector.PreviousTile.Value)
                        _lastTiles.Clear();
                    else
                        Visualize(tile);
                })
                .AddTo(_disposable);
        }

        public void Disable() => 
            _disposable.Clear();

        private void Visualize(Tile currentTile)
        {
            List<Vector2Int> tilesCoordinates =
                _pathFinder.PathFindingResults.Value.GetPathTo(currentTile.Logic.Coordinates, false);

            foreach (Vector2Int coordinate in tilesCoordinates)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile))
                {
                    _lastTiles.Add(tile);
                    tile.View.ChangeHighlightColor(_tileColorsConfig.PathToTile);
                }
            }
        }

        private void ResetLastTiles()
        {
            foreach (Tile tile in _lastTiles) 
                tile.View.ChangeHighlightColor(_tileColorsConfig.WalkableColorTile);
            
            _lastTiles.Clear();
        }
    }
}