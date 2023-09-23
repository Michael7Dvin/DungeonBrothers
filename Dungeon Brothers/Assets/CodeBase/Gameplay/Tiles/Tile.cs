using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Services.Logger;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class Tile : MonoBehaviour
    {
        private ICustomLogger _customLogger;
        public bool IsOccupied { get; private set; }
        
        public void Construct(Vector2Int coordinates,
            TileView tileView)
        {
            Coordinates = coordinates;

            TileView = tileView;
        }

        
        public void Inject(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        public TileView TileView { get; private set; }
        public Vector2Int Coordinates { get; private set; }
        
        public ICharacter Character { get; private set; }

        public void Occupy(ICharacter character)
        {
            if (IsOccupied)
                _customLogger.LogError(new Exception("Tile is occupied"));

            IsOccupied = true;

            Character = character;
        }
    }
}
