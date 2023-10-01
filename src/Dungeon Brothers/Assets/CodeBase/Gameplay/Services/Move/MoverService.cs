using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Move
{
    public class MoverService : IMoverService
    {
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly ITurnQueue _turnQueue;
        private readonly CompositeDisposable _disposable = new();

        public PathFindingResults PathFindingResults { get; private set; }

        private readonly ReactiveCommand<ICharacter> _isMoved = new();

        public IObservable<ICharacter> IsMoved => _isMoved;

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
            _turnQueue.ActiveCharacter
                .Skip(1)
                .Subscribe(character =>
                {
                    ResetMovePoints(character);
                    CalculatePaths(character);
                })
                .AddTo(_disposable);
            
            IsMoved
                .Subscribe(CalculatePaths)
                .AddTo(_disposable);
        }

        public void Disable() => 
            _disposable.Clear();

        public void Move(Tile tile)
        {
            ICharacter character = _turnQueue.ActiveCharacter.Value;

            if (tile.Logic.Coordinates == character.Coordinate)
                return;
            
            if (PathFindingResults.IsMovableAt(tile.Logic.Coordinates) == false)
                return;

            List<Vector2Int> path = PathFindingResults.GetPathTo(tile.Logic.Coordinates);
            int pathCost = path.Count;
            CurrentMovePoints -= pathCost;
                
            if (CurrentMovePoints < 0)
                return;
            
            if (_mapService.TryGetTile(character.Coordinate, out Tile previousTile)) 
                previousTile.Logic.Release();
            
            character.UpdateCoordinate(tile.Logic.Coordinates);
            tile.Logic.Occupy(character);

            Move(path, character);
            
            _isMoved.Execute(character);
        }

        private void Move(List<Vector2Int> path, ICharacter character)
        {
            List<Vector3> newPath = new();

            foreach (var point in path)
            {
                if(_mapService.TryGetTile(point, out Tile tile))
                    newPath.Add(tile.transform.position);
            }
            
            character.Transform.DOPath(newPath.ToArray(), 1).Play();
        }
        

        private void ResetMovePoints(ICharacter character) =>
            CurrentMovePoints = character.MovementStats.MovePoints;
        
        private void CalculatePaths(ICharacter character)
        {
            Vector2Int startPosition = character.Coordinate;
            bool isMoveThroughObstacles = character.MovementStats.IsMoveThroughObstacles;

            PathFindingResults pathFindingResults =
                _pathFinder.CalculatePaths(startPosition, CurrentMovePoints, isMoveThroughObstacles);

            PathFindingResults = pathFindingResults;
        }
    }
}