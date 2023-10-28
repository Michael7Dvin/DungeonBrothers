using UniRx;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Services.PathFinder
{
    public interface IPathFinder
    {
        PathFindingResults CalculatePaths(Vector2Int start, int maxDistance, bool isMoveThroughObstacles);
        public IReadOnlyReactiveProperty<PathFindingResults> PathFindingResults { get; }

        public void SetPathFindingResults(PathFindingResults pathFindingResults);
    }
}