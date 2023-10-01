using System.Collections.Generic;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.InteractionsService;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation.Path
{
    public class VisualizationPathToTile : IVisualizationPathToTile
    {
        private readonly ITileSelector _tileSelector;
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly IInteractionService _interactionService;
        private readonly CompositeDisposable _disposable = new();

        private readonly TileColorConfig _tileColorConfig;

        private readonly List<Tile> _lastTiles = new();

        public VisualizationPathToTile(ITileSelector tileSelector, 
            IPathFinder pathFinder,
            IMapService mapService,
            IInteractionService interactionService,
            IStaticDataProvider staticDataProvider)
        {
            _tileSelector = tileSelector;
            _pathFinder = pathFinder;
            _mapService = mapService;
            _interactionService = interactionService;
            _tileColorConfig = staticDataProvider.TileColorConfig;
        }

        public void Initialize()
        {
            _tileSelector.CurrentTile
                .Skip(1)
                .Where(tile => _interactionService.IsInteract == false)
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
            List<Vector2Int> _tilesCoordinates =
                _pathFinder.PathFindingResults.Value.GetPathTo(currentTile.Logic.Coordinates);

            foreach (Vector2Int coordinate in _tilesCoordinates)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile))
                {
                    _lastTiles.Add(tile);
                    tile.View.ChangeHighlightColor(_tileColorConfig.PathToTileColor);
                }
            }
        }

        private void ResetLastTiles()
        {
            foreach (Tile tile in _lastTiles) 
                tile.View.ChangeHighlightColor(_tileColorConfig.WalkableColorTile);
            
            _lastTiles.Clear();
        }
    }
}