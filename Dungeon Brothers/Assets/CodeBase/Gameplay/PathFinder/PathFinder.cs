using System.Collections.Generic;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Gameplay.Services.PathFinder
{
    public class PathFinder : IPathFinder
    {
        private readonly IMapService _mapService;

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
                    Vector2Int neighborCoordinates = neighbor.Coordinates;
                    bool isNeighborWalkable = neighbor.IsWalkable;
                    int distance = distances[calculatingTile]++;

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
            return new PathFindingResults(paths, obstacles);
        }
    }
}