using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Services.Logger;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Gameplay.Tiles
{
    public class TileLogic
    {
        private ICustomLogger _customLogger;
        private readonly CompositeDisposable _disposable = new();
        
        [Inject]
        public void Inject(ICustomLogger customLogger) =>
            _customLogger = customLogger;

        public TileLogic(bool isOccupied,
            bool isWalkable, 
            Vector2Int coordinates)
        {
            IsOccupied = isOccupied;
            IsWalkable = isWalkable;
            Coordinates = coordinates;
        }
        
        public bool IsOccupied { get; private set; }
        public bool IsWalkable { get; private set; }
        public Vector2Int Coordinates { get; private set; }
        public ICharacter Character { get; private set; }
        
        public void Release()
        {
            _disposable.Clear();
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

            character.Logic.Health.Died
                .Subscribe(_ => Release())
                .AddTo(_disposable);
        }
    }
}