using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.AI.Behaviours
{
    public class MoveBehaviour : IMoveBehaviour
    {
        private readonly IPathFinder _pathFinder;
        private readonly IMoverService _moverService;
        private readonly IMapService _mapService;

        private Vector2Int _currentCoordinate;
        
        public MoveBehaviour(IPathFinder pathFinder,
            IMoverService moverService,
            IMapService mapService)
        {
            _pathFinder = pathFinder;
            _moverService = moverService;
            _mapService = mapService;
        }

        public async UniTask Move(ICharacter activeCharacter,
            ICharacter target)
        {
            _currentCoordinate = new Vector2Int(int.MaxValue, int.MaxValue);

            PathFindingResults pathFindingResults = GetPathFindingResults(activeCharacter);

            Vector2Int coordinate = GetCoordinate(pathFindingResults,target);

            if (_mapService.TryGetTile(coordinate, out Tile tile)) 
                await _moverService.Move(tile);
        }

        private PathFindingResults GetPathFindingResults(ICharacter activeCharacter) =>
            _pathFinder.CalculatePaths(activeCharacter.Coordinate,
                activeCharacter.Stats.MovePoints, false);

        private Vector2Int GetCoordinate(PathFindingResults pathFindingResults,
            ICharacter target)
        {
            foreach (var coordinate in pathFindingResults.WalkableCoordinates)
            {
                float distance = Vector2.Distance(coordinate, target.Coordinate);

                if (Vector2.Distance(_currentCoordinate, target.Coordinate) > distance) 
                    _currentCoordinate = coordinate;
            }

            return _currentCoordinate;
        }
    }
}