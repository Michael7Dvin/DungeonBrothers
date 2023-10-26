﻿using System;
using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.PathFinder;
using _Project.CodeBase.Gameplay.Services.Map;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using _Project.CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Services.Move
{
    public class MoverService : IMoverService
    {
        private const float AnimationDuration = 2;
        
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly ITurnQueue _turnQueue;
        private readonly CompositeDisposable _disposable = new();
        private readonly ReactiveCommand<ICharacter> _isMoved = new();

        private int _currentMovePoints;

        public MoverService(IPathFinder pathFinder, 
            IMapService mapService,
            ITurnQueue turnQueue)
        {
            _pathFinder = pathFinder;
            _mapService = mapService;
            _turnQueue = turnQueue;
        }
        
        public PathFindingResults PathFindingResults { get; private set; }
        public IObservable<ICharacter> IsMoved => _isMoved;

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
        }

        public void Disable() => 
            _disposable.Clear();

        public async UniTask Move(Tile tile)
        {
            ICharacter character = _turnQueue.ActiveCharacter.Value;

            if (tile.Logic.Coordinates == character.Coordinate)
                return;

            if (PathFindingResults.IsMovableAt(tile.Logic.Coordinates) == false)
                return;
            
            
            List<Vector2Int> path = PathFindingResults.GetPathTo(tile.Logic.Coordinates, false);
            int pathCost = path.Count;
            _currentMovePoints -= pathCost;

            if (_currentMovePoints < 0)
                return;
            
            if (_mapService.TryGetTile(character.Coordinate, out Tile characterTile)) 
                characterTile.Logic.Release();
            
            character.UpdateCoordinate(tile.Logic.Coordinates);
            tile.Logic.Occupy(character);

            await Move(path, character);
            
            _isMoved.Execute(character);
            CalculatePaths(character);
        }

        private async UniTask Move(List<Vector2Int> path, ICharacter character)
        {
            List<Vector3> newPath = new();

            foreach (var point in path)
            {
                if(_mapService.TryGetTile(point, out Tile tile))
                    newPath.Add(tile.transform.position);
            }
            
            await character.Transform
                .DOPath(newPath.ToArray(), AnimationDuration)
                .Play()
                .SetEase(Ease.OutQuart)
                .ToUniTask();
        }
        

        private void ResetMovePoints(ICharacter character) =>
            _currentMovePoints = character.Stats.MovePoints;
        
        private void CalculatePaths(ICharacter character)
        {
            Vector2Int startPosition = character.Coordinate;
            bool isMoveThroughObstacles = character.Stats.IsMoveThroughObstacles;

            PathFindingResults pathFindingResults =
                _pathFinder.CalculatePaths(startPosition, _currentMovePoints, isMoveThroughObstacles);
            
            _pathFinder.SetPathFindingResults(pathFindingResults);

            PathFindingResults = pathFindingResults;
        }
    }
}