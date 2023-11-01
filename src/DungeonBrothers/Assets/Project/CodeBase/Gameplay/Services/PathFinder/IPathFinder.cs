using Project.CodeBase.Gameplay.Tiles;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Services.PathFinder
{
    public interface IPathFinder
    {
        PathFindingResults CalculatePathsByDistance(Vector2Int start, int maxDistance, bool isMoveThroughObstacles);
        public PathFindingResults CalculatePathsByTarget(Vector2Int start, Tile tile, bool isMoveThroughObstacles);
        
        public IReadOnlyReactiveProperty<PathFindingResults> PathFindingResults { get; }

        public void SetPathFindingResults(PathFindingResults pathFindingResults);
    }
}