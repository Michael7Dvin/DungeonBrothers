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

        private readonly ReactiveCommand<Character> _isMoved = new();

        public IObservable<Character> IsMoved => _isMoved;

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
            
            character.UpdateCoordinate(tile.TileLogic.Coordinates);
            tile.TileLogic.Occupy(character);

            Move(path, character);
            
            _isMoved.Execute(character);
        }

        private void Move(List<Vector2Int> path, Character character)
        {
            List<Vector3> newPath = new();

            foreach (var point in path)
            {
                if(_mapService.TryGetTile(point, out Tile tile))
                    newPath.Add(tile.transform.position);
            }
            
            character.transform.DOPath(newPath.ToArray(), 1).Play();
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