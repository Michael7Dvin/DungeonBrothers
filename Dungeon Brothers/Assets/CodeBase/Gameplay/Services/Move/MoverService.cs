using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.InteractionsService;
using CodeBase.Gameplay.Services.Map;
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

        public event Action<Character> IsMoved; 

        public MoverService(IPathFinder pathFinder, 
            IMapService mapService,
            ITurnQueue turnQueue)
        {
            _pathFinder = pathFinder;
            _mapService = mapService;
            _turnQueue = turnQueue;
        }
        
        public int CurrentMovePoints { get; private set; }

        public void Enable()
        {
            _turnQueue.ActiveCharacter.Changed += ResetMovePoints;
        }

        public void Disable()
        {
            _turnQueue.ActiveCharacter.Changed -= ResetMovePoints;
        }
        
        public void Move(Tile tile)
        {
            Character character = _turnQueue.ActiveCharacter.Value;
            
            if (CurrentMovePoints > 0)
                CalculatePaths(character);
            
            if (_pathFindingResults.IsMovableAt(tile.Coordinates) == false)
                return;
            
            List<Vector2Int> path = _pathFindingResults.GetPathTo(tile.Coordinates);
            int pathCost = path.Count;
            CurrentMovePoints -= pathCost;
                
            if (CurrentMovePoints <= 0)
                return;
            
            if (_mapService.TryGetTile(character.Coordinate, out Tile previousTile)) 
                previousTile.Release();
            
            Vector3 position = tile.transform.position;
            
            character.UpdateCoordinate(tile.Coordinates);
            tile.Occupy(character);
            
            character.transform.position = position;
            
            IsMoved?.Invoke(character);
        }

        private void ResetMovePoints(Character character) =>
            CurrentMovePoints = character.CharacterStats.MovePoints;
        
        private void CalculatePaths(Character character)
        {
            Vector2Int startPosition = character.Coordinate;
            bool isMoveThroughObstacles = character.CharacterStats.IsMoveThroughObstacles;

            PathFindingResults pathFindingResults =
                _pathFinder.CalculatePaths(startPosition, CurrentMovePoints, isMoveThroughObstacles);

            _pathFindingResults = pathFindingResults;
        }
    }
}