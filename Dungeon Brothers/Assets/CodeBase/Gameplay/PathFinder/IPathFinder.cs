using CodeBase.Gameplay.Services.PathFinder;
using UnityEngine;

namespace CodeBase.Gameplay.PathFinder
{
    public interface IPathFinder
    {
        PathFindingResults CalculatePaths(Vector2Int start, int maxDistance, bool isMoveThroughObstacles);
    }
}