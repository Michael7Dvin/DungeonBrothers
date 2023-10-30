using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.Logic.Movement;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Services.PathFinder;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Gameplay.Tiles;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Services.Move
{
    public class MoverService : IMoverService
    {
        private readonly IPathFinder _pathFinder;
        private readonly ITurnQueue _turnQueue;
        private readonly IMapService _mapService;
        
        private readonly CompositeDisposable _disposable = new();
        private readonly ReactiveCommand<ICharacter> _isMoved = new();

        public MoverService(IPathFinder pathFinder, ITurnQueue turnQueue, IMapService mapService)
        {
            _pathFinder = pathFinder;
            _turnQueue = turnQueue;
            _mapService = mapService;
        }

        public PathFindingResults PathFindingResults { get; private set; }
        public IObservable<ICharacter> IsMoved => _isMoved;

        public void Enable()
        {
            _turnQueue.ActiveCharacter
                .Skip(1)
                .Subscribe(character =>
                {
                    IMovement characterMovement = character.Logic.Movement;
                    
                    ResetAvailableMovePoints(characterMovement);
                    RecalculatePaths(characterMovement);
                })
                .AddTo(_disposable);
        }

        public void Disable() => 
            _disposable.Clear();

        public async UniTask Move(Tile destinationTile)
        {
            ICharacter character = _turnQueue.ActiveCharacter.Value;
            IMovement characterMovement = character.Logic.Movement;

            if (destinationTile.Logic.Coordinates == characterMovement.Coordinates)
                return;

            if (PathFindingResults.IsMovableAt(destinationTile.Logic.Coordinates) == false)
                return;
            
            List<Vector2Int> coordinatesPath = 
                PathFindingResults.GetPathTo(destinationTile.Logic.Coordinates, characterMovement.IsMoveThroughObstacles);

            List<Tile> tilesPath = GetTilesPathFromCoordinatesPath(coordinatesPath);

            if (characterMovement.CanMove(tilesPath))
                return;
            
            await characterMovement.Move(tilesPath);

            _isMoved.Execute(character);
            RecalculatePaths(characterMovement);
        }

        private List<Tile> GetTilesPathFromCoordinatesPath(List<Vector2Int> coordinatesPath)
        {
            List<Tile> tilesPath = new();

            foreach (Vector2Int coordinates in coordinatesPath)
            {
                _mapService.TryGetTile(coordinates, out Tile tile);
                tilesPath.Add(tile);
            }

            return tilesPath;
        }

        private void ResetAvailableMovePoints(IMovement characterMovement) =>
            characterMovement.ResetAvailableMovePoints();
        
        private void RecalculatePaths(IMovement characterMovement)
        {
            Vector2Int startPosition = characterMovement.Coordinates;
            bool isMoveThroughObstacles = characterMovement.IsMoveThroughObstacles;

            PathFindingResults pathFindingResults =
                _pathFinder.CalculatePaths(startPosition, characterMovement.AvailableMovePoints, isMoveThroughObstacles);
            
            _pathFinder.SetPathFindingResults(pathFindingResults);

            PathFindingResults = pathFindingResults;
        }
    }
}