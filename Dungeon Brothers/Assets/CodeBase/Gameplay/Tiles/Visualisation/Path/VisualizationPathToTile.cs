using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation.Path
{
    public class VisualizationPathToTile : IVisualizationPathToTile
    {
        private readonly ITileSelector _tileSelector;
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;

        private readonly TileColorConfig _tileColorConfig;

        private readonly List<Tile> _lastTiles = new();

        public VisualizationPathToTile(ITileSelector tileSelector, 
            IPathFinder pathFinder,
            IMapService mapService,
            IStaticDataProvider staticDataProvider)
        {
            _tileSelector = tileSelector;
            _pathFinder = pathFinder;
            _mapService = mapService;
            _tileColorConfig = staticDataProvider.TileColorConfig;
        }

        public void Initialize()
        {
            _tileSelector.CurrentTile.Changed += Visualize;
        }

        public void Disable()
        {
            _tileSelector.CurrentTile.Changed -= Visualize;
        }
        
        private void Visualize(Tile currentTile)
        {
            if (currentTile == _tileSelector.PreviousTile.Value)
            {
                _lastTiles.Clear();
                return;
            }
            
            ResetLastTiles();
            
            PathFindingResults pathFindingResults = _pathFinder.PathFindingResults.Value;
            
            if (pathFindingResults.WalkableCoordinates.Contains(currentTile.Coordinates) == false)
                return;
            
            List<Vector2Int> _tilesCoordinates = _pathFinder.PathFindingResults.Value.GetPathTo(currentTile.Coordinates);

            foreach (var coordinate in _tilesCoordinates)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile))
                {
                    _lastTiles.Add(tile);
                    tile.TileView.ChangeHighlightColor(_tileColorConfig.PathToTileColor);
                }
            }
        }

        private void ResetLastTiles()
        {
            foreach (var tile in _lastTiles) 
                tile.TileView.ChangeHighlightColor(_tileColorConfig.WalkableColorTile);
            
            _lastTiles.Clear();
        }
    }
}