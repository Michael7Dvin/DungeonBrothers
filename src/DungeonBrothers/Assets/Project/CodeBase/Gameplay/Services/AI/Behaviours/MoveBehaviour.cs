﻿using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Services.Move;
using Project.CodeBase.Gameplay.Services.PathFinder;
using Project.CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Services.AI.Behaviours
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
            _pathFinder.CalculatePaths(activeCharacter.Logic.Movement.Coordinates,
                activeCharacter.Logic.Movement.StartMovePoints, false);

        private Vector2Int GetCoordinate(PathFindingResults pathFindingResults,
            ICharacter target)
        {
            foreach (var coordinate in pathFindingResults.WalkableCoordinates)
            {
                float distance = Vector2.Distance(coordinate, target.Logic.Movement.Coordinates);

                if (Vector2.Distance(_currentCoordinate, target.Logic.Movement.Coordinates) > distance) 
                    _currentCoordinate = coordinate;
            }

            return _currentCoordinate;
        }
    }
}