using System.Collections.Generic;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Gameplay.Services.TurnQueue;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation
{
    public class PathFinderVisualization : IPathFinderVisualization
    {
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly IMoverService _moverService;
        private readonly ITurnQueue _turnQueue;

        private List<Tile> _lastTiles = new();

        public PathFinderVisualization(IPathFinder pathFinder,
            IMapService mapService,
            ITurnQueue turnQueue)
        {
            _pathFinder = pathFinder;
            _mapService = mapService;;
            _turnQueue = turnQueue;
        }

        public void Initialize()
        {
            _pathFinder.PathFindingResults.Changed += VisualizeWalkableTiles;
        }

        public void Disable()
        {
            _pathFinder.PathFindingResults.Changed -= VisualizeWalkableTiles;
        }

        public void VisualizeWalkableTiles(PathFindingResults pathFindingResults)
        {
            ResetLastTilesView();
            _lastTiles = new List<Tile>();
            
            foreach (var coordinate in pathFindingResults.WalkableCoordinates)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile))
                {
                    _lastTiles.Add(tile);
                    
                    tile.TileView.SwitchHighlight(true);
                    tile.TileView.ChangeHighlightColor(Color.blue);
                }
            }
        }

        private void ResetLastTilesView()
        {
            if (_lastTiles.Count > 0)
            {
                foreach (var tile in _lastTiles)
                {
                    if (_turnQueue.ActiveCharacter.Value.Coordinate == tile.Coordinates)
                        continue;

                    tile.TileView.ResetTileView();
                }
            }
        }
    }
}