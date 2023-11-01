using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Services.PathFinder;
using Project.CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Services.AI.Behaviours
{
    public class MoveBehaviour : IMoveBehaviour
    {
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;

        private Vector2Int _currentCoordinate;
        
        public MoveBehaviour(IPathFinder pathFinder,
            IMapService mapService)
        {
            _pathFinder = pathFinder;
            _mapService = mapService;
        }

        public async UniTask Move(ICharacter activeCharacter,
            ICharacter target)
        {
            List<Vector2Int> currentPath = GetCoordinatePath(target, activeCharacter);

            if (currentPath == null)
                return;

            Vector2Int lastCoordinate = GetCoordinate(currentPath, activeCharacter);

            List<Tile> tilePath = GetTilePath(currentPath, lastCoordinate);
            
            await activeCharacter.Logic.Movement.Move(tilePath);
        }

        private Vector2Int GetCoordinate(List<Vector2Int> path,
            ICharacter activeCharacter)
        {
            int movablePoint = activeCharacter.Logic.Movement.AvailableMovePoints;

            if (movablePoint >= path.Count)
                return path[^2];
            
            return path[movablePoint - 1];
        }

        private List<Tile> GetTilePath(List<Vector2Int> path, 
            Vector2Int lastCoordinate)
        {
            List<Tile> tilePath = new();

            foreach (var coordinate in path)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile))
                    tilePath.Add(tile);
                    
                if (coordinate == lastCoordinate)
                    break;
            }

            return tilePath;
        }

        private List<Vector2Int> GetCoordinatePath(ICharacter target, 
            ICharacter activeCharacter)
        {
            Vector2Int targetCoordinates = target.Logic.Movement.Coordinates;
            
            Vector2Int activeCharacterCoordinates = activeCharacter.Logic.Movement.Coordinates;
            bool activeCharacterMoveThroughObstacles = activeCharacter.Logic.Movement.IsMoveThroughObstacles;

            if (_mapService.TryGetTile(targetCoordinates, out Tile tile))
            {
                PathFindingResults pathFinding =
                    _pathFinder.CalculatePathsByTarget(activeCharacterCoordinates, tile, activeCharacterMoveThroughObstacles);

                Vector2Int tileCoordinate = tile.Logic.Coordinates;

                return pathFinding.GetPathTo(tileCoordinate, false);
            }

            return null;
        }
    }
}