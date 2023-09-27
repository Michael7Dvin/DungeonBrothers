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
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Move
{
    public class MoverService : IMoverService
    {
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly ITurnQueue _turnQueue;

        public PathFindingResults PathFindingResults { get; private set; }

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
            _turnQueue.ActiveCharacter.Changed += CalculatePaths;
            IsMoved += CalculatePaths;
        }

        public void Disable()
        {
            _turnQueue.ActiveCharacter.Changed -= ResetMovePoints;
            _turnQueue.ActiveCharacter.Changed -= CalculatePaths;
            IsMoved += CalculatePaths;
        }
        
        public void Move(Tile tile)
        {
            Character character = _turnQueue.ActiveCharacter.Value;

            if (tile.TileLogic.Coordinates == character.Coordinate)
                return;
            
            if (PathFindingResults.IsMovableAt(tile.TileLogic.Coordinates) == false)
                return;

            List<Vector2Int> path = PathFindingResults.GetPathTo(tile.TileLogic.Coordinates);
            int pathCost = path.Count;
            CurrentMovePoints -= pathCost;
                
            if (CurrentMovePoints < 0)
                return;
            
            if (_mapService.TryGetTile(character.Coordinate, out Tile previousTile)) 
                previousTile.Release();
            
            Vector3 position = tile.transform.position;
            
            character.UpdateCoordinate(tile.TileLogic.Coordinates);
            tile.TileLogic.Occupy(character);

            character.CharacterAnimation.PlayMoveAnimation(position);

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

            PathFindingResults = pathFindingResults;
        }
    }
}