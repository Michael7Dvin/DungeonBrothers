using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Services.Logger;
using UnityEngine;
using VContainer;

namespace CodeBase.Gameplay.Tiles
{
    public class TileLogic
    {
        private ICustomLogger _customLogger;
        
        public bool IsOccupied { get; private set; }
        public bool IsWalkable { get; private set; }
        
        public Vector2Int Coordinates { get; private set; }
        public ICharacter Character { get; private set; }


        public TileLogic(bool isOccupied, bool isWalkable, Vector2Int coordinates)
        {
            IsOccupied = isOccupied;
            IsWalkable = isWalkable;
            Coordinates = coordinates;
        }

        [Inject]
        public void Inject(ICustomLogger customLogger) =>
            _customLogger = customLogger;
        
        
        public void Release()
        {
            Character = null;
            IsOccupied = false;
            IsWalkable = true;
        }
        
        public void Occupy(ICharacter character)
        {
            if (IsOccupied)
                _customLogger.LogError(new Exception("Tile is occupied"));

            IsOccupied = true;
            IsWalkable = false;

            Character = character;
        }
    }
}