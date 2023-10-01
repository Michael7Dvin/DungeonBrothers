using System.Collections.Generic;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Tiles;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.PathFinder
{
    public class PathFinder : IPathFinder
    {
        private readonly IMapService _mapService;

        private readonly ReactiveProperty<PathFindingResults> _pathFindingResults = new();
        public IReadOnlyReactiveProperty<PathFindingResults> PathFindingResults => _pathFindingResults;
        
        public PathFinder(IMapService mapService)
        {
            _mapService = mapService;
        }

        public PathFindingResults CalculatePaths(Vector2Int start, int maxDistance, bool isMoveThroughObstacles)
        {
            Dictionary<Vector2Int, Vector2Int?> paths = new();
            Dictionary<Vector2Int, int> distances = new();
            Dictionary<Vector2Int, Tile> obstacles = new();
            Queue<Vector2Int> toVisit = new();
            
            toVisit.Enqueue(start);
            distances.Add(start, 0);
            paths.Add(start, null);

            while (toVisit.Count > 0)
            {
                Vector2Int calculatingTile = toVisit.Dequeue();

                foreach (Tile neighbor in _mapService.GetNeighbors(calculatingTile))
                {
                    Vector2Int neighborCoordinates = neighbor.Logic.Coordinates;
                    bool isNeighborWalkable = neighbor.Logic.IsWalkable;
                    int distance = distances[calculatingTile] + 1;

                    if (isNeighborWalkable == false)
                    {
                        if (distance <= maxDistance)
                            obstacles[neighborCoordinates] = neighbor;

                        if (isMoveThroughObstacles == false)
                            continue;
                    }

                    if (distance <= maxDistance)
                    {
                        if (paths.ContainsKey(neighborCoordinates) == false)
                        {
                            paths[neighborCoordinates] = calculatingTile;
                            distances[neighborCoordinates] = distance;
                            toVisit.Enqueue(neighborCoordinates);
                        }
                        else if (distances[neighborCoordinates] > distance)
                        {
                            paths[neighborCoordinates] = calculatingTile;
                            distances[neighborCoordinates] = distance;
                        }
                    }
                }
            }

            obstacles.Remove(start);
            _pathFindingResults.Value = new PathFindingResults(paths, obstacles);
            return _pathFindingResults.Value;
        }
    }
}