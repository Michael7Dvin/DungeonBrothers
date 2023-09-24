using UnityEngine;

namespace CodeBase.Gameplay.Services.PathFinder
{
    public interface IPathFinder
    {
        PathFindingResults CalculatePaths(Vector2Int start, int maxDistance, bool isMoveThroughObstacles);
    }
}