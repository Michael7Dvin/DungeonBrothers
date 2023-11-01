using System.Collections.Generic;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Tiles;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Services.PathFinder
{
    public class PathFinder : IPathFinder
    {
        private readonly IMapService _mapService;

        private readonly ReactiveProperty<PathFindingResults> _pathFindingResults = new();

        private Dictionary<Vector2Int, Vector2Int?> _paths;
        private Dictionary<Vector2Int, int> _distances;
        private Dictionary<Vector2Int, Tile> _obstacles;
        private Queue<Vector2Int> _toVisit;

        public IReadOnlyReactiveProperty<PathFindingResults> PathFindingResults => _pathFindingResults;

        public PathFinder(IMapService mapService)
        {
            _mapService = mapService;
        }

        private void PrepareToStartCalculatingPaths(Vector2Int start)
        {
            _paths = new();
            _distances = new();
            _obstacles = new();
            _toVisit = new();

            _toVisit.Enqueue(start);
            _distances.Add(start, 0);
            _paths.Add(start, null);
        }

        public PathFindingResults CalculatePathsByDistance(Vector2Int start, int maxDistance,
            bool isMoveThroughObstacles)
        {
            PrepareToStartCalculatingPaths(start);

            while (_toVisit.Count > 0)
            {
                Vector2Int calculatingTile = _toVisit.Dequeue();

                foreach (Tile neighbor in _mapService.GetNeighbors(calculatingTile))
                {
                    Vector2Int neighborCoordinates = neighbor.Logic.Coordinates;
                    bool isNeighborWalkable = neighbor.Logic.IsWalkable;
                    int distance = _distances[calculatingTile] + 1;

                    if (isNeighborWalkable == false)
                    {
                        if (distance <= maxDistance)
                            _obstacles[neighborCoordinates] = neighbor;

                        if (isMoveThroughObstacles == false)
                            continue;
                    }

                    if (distance <= maxDistance) 
                        AddTileToPath(neighborCoordinates, calculatingTile, distance);
                }
            }

            _obstacles.Remove(start);
            return new PathFindingResults(_paths, _obstacles);
        }

        public PathFindingResults CalculatePathsByTarget(Vector2Int start, Tile tile, bool isMoveThroughObstacles)
        {
            PrepareToStartCalculatingPaths(start);

            while (_toVisit.Count > 0)
            {
                Vector2Int calculatingTile = _toVisit.Dequeue();

                foreach (Tile neighbor in _mapService.GetNeighbors(calculatingTile))
                {
                    Vector2Int neighborCoordinates = neighbor.Logic.Coordinates;
                    bool isNeighborWalkable = neighbor.Logic.IsWalkable;
                    int distance = _distances[calculatingTile] + 1;

                    if (neighbor == tile)
                    {
                        _toVisit.Clear();
                        _paths[neighborCoordinates] = calculatingTile;
                        break;
                    }

                    if (isNeighborWalkable == false)
                    {
                        _obstacles[neighborCoordinates] = neighbor;

                        if (isMoveThroughObstacles == false)
                            continue;
                    }

                    AddTileToPath(neighborCoordinates, calculatingTile, distance);
                }
            }

            _obstacles.Remove(start);
            return new PathFindingResults(_paths, _obstacles);
        }

        private void AddTileToPath(Vector2Int neighborCoordinates, Vector2Int calculatingTile, int distance)
        {
            if (_paths.ContainsKey(neighborCoordinates) == false)
            {
                _paths[neighborCoordinates] = calculatingTile;
                _distances[neighborCoordinates] = distance;
                _toVisit.Enqueue(neighborCoordinates);
            }
            else if (_distances[neighborCoordinates] > distance)
            {
                _paths[neighborCoordinates] = calculatingTile;
                _distances[neighborCoordinates] = distance;
            }
        }

        private bool TryContinueCalculatingPath(Vector2Int neighborCoordinates)
        {
            if (_paths.ContainsKey(neighborCoordinates) == false)
            {
                _toVisit.Enqueue(neighborCoordinates);
                return true;
            }

            return false;
        }
        
        public void SetPathFindingResults(PathFindingResults pathFindingResults) =>
            _pathFindingResults.SetValueAndForceNotify(pathFindingResults);
    }
}