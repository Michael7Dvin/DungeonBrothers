using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.View.Move
{
    public interface IMovementView
    {
        UniTask Move(Vector2Int characterCoordinates, Tile destinationTile);
        void StopMovement();
        void StartMovement();
    }
}