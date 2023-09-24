using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Move
{
    public class MoverService : IMoverService
    {
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly ITurnQueue _turnQueue;

        private PathFindingResults _pathFindingResults;
        
        public MoverService(IPathFinder pathFinder, 
            IMapService mapService,
            ITurnQueue turnQueue)
        {
            _pathFinder = pathFinder;
            _mapService = mapService;
            _turnQueue = turnQueue;
        }


        public void Move(Tile tile, Character character)
        {
            if (_pathFindingResults.WalkableCoordinates.Contains(tile.Coordinates))
            {
                Vector3 position = tile.transform.position;

                character.transform.position = position;
            }
        }

        public void CalculatePaths(Character character)
        {
            Vector2Int startPosition = character.Coordinate;
            int movablePoints = character.CharacterStats.MovePoints;
            bool isMoveThroughObstacles = character.CharacterStats.IsMoveThroughObstacles;

            PathFindingResults pathFindingResults =
                _pathFinder.CalculatePaths(startPosition, movablePoints, isMoveThroughObstacles);

            _pathFindingResults = pathFindingResults;
        }
    }
}