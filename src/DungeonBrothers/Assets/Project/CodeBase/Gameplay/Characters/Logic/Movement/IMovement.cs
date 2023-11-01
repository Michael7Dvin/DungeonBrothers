using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.Logic.Movement
{
    public interface IMovement
    {
        Vector2Int Coordinates { get; }
        bool IsMoveThroughObstacles { get; }
        int StartMovePoints { get; }
        int AvailableMovePoints { get; }
        void ResetAvailableMovePoints();
        bool CanMove(List<Tile> tilePath);
        UniTask Move(List<Tile> tilePath);
        void Teleport(Tile destinationTile);
    }
}