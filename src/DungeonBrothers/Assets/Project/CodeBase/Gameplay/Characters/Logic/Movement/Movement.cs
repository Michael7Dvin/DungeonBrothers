﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters.View.Move;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.Logger;
using UnityEngine;
using VContainer;

namespace Project.CodeBase.Gameplay.Characters.Logic.Movement
{
    public class Movement : IMovement
    {
        private readonly ICharacter _character;
        private readonly IMovementView _movementView;

        private ICustomLogger _logger;

        [Inject]
        public void Inject(ICustomLogger logger)
        {
            _logger = logger;
        }

        public Movement(ICharacter character,
            IMovementView movementView,
            bool isMoveThroughObstacles,
            int startMovePoints)
        {
            _movementView = movementView;
            _character = character;
            IsMoveThroughObstacles = isMoveThroughObstacles;
            StartMovePoints = startMovePoints;

            AvailableMovePoints = StartMovePoints;
        }

        public bool IsMoveThroughObstacles { get; }
        public int StartMovePoints { get; }
        public int AvailableMovePoints { get; private set; }

        public Vector2Int Coordinates => 
            OccupiedTile.Logic.Coordinates;

        private Tile OccupiedTile { get; set; }

        public void ResetAvailableMovePoints() => 
            AvailableMovePoints = StartMovePoints;

        public bool CanMove(List<Tile> tilesPath)
        {
            bool isDestinationNotAtCharacterCoordinates = tilesPath.Last().Logic.Coordinates != Coordinates;
            bool isEnoughMovePoints = tilesPath.Count <= AvailableMovePoints;

            return isDestinationNotAtCharacterCoordinates && isEnoughMovePoints;
        }

        public async UniTask Move(List<Tile> tilesPath)
        {
            if (CanMove(tilesPath) == false)
            {
                _logger.LogError(new ArgumentException($"Can't move by chosen {nameof(tilesPath)}"));
                return;
            }
            
            _movementView.StartMovement();
            
            foreach (Tile tile in tilesPath) 
                await MoveToTile(tile);

            _movementView.StopMovement();
        }

        public void Teleport(Tile destinationTile)
        {
            if (OccupiedTile != null)
                OccupiedTile.Logic.Release();

            destinationTile.Logic.Occupy(_character);
            OccupiedTile = destinationTile;
        }

        private async Task MoveToTile(Tile tile)
        {
            OccupiedTile.Logic.Release();
            await _movementView.Move(Coordinates, tile);

            if (tile.Logic.IsOccupied.Value == false)
            {
                tile.Logic.Occupy(_character);
                OccupiedTile = tile;
            }
            
            if (_character.IsInBattle)
                AvailableMovePoints--;
        }
    }
}