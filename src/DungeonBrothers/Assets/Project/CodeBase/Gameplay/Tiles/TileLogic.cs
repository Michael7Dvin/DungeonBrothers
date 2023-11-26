using System;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Infrastructure.Services.Logger;
using UniRx;
using UnityEngine;
using VContainer;

namespace Project.CodeBase.Gameplay.Tiles
{
    public class TileLogic
    {
        private ICustomLogger _customLogger;
        private readonly CompositeDisposable _disposable = new();
        private readonly ReactiveProperty<bool> _isOccupied = new();

        public Direction PassageDirection;
        public bool IsPassage;
        public bool IsEnterInNewRoom;
        public bool IsEnterInPreviousRoom;

        [Inject]
        public void Inject(ICustomLogger customLogger) =>
            _customLogger = customLogger;

        public TileLogic(bool isOccupied,
            bool isWalkable, 
            Vector2Int coordinates)
        {
            _isOccupied.Value = isOccupied;
            IsWalkable = isWalkable;
            Coordinates = coordinates;
        }

        public IReadOnlyReactiveProperty<bool> IsOccupied => _isOccupied;
        public bool IsWalkable { get; private set; }
        public Vector2Int Coordinates { get; private set; }
        public ICharacter Character { get; private set; }
        
        public void Release()
        {
            _disposable.Clear();
            Character = null;
            _isOccupied.Value = false;
            IsWalkable = true;
        }
        
        public void Occupy(ICharacter character)
        {
            if (_isOccupied.Value)
                _customLogger.LogError(new Exception("Tile is occupied"));

            _isOccupied.Value = true;
            IsWalkable = false;

            Character = character;

            character.Logic.Death.Died
                .Subscribe(_ => Release())
                .AddTo(_disposable);
        }
    }
}