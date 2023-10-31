using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.View.Move
{
    public interface IMovementView
    {
        UniTask Move(Vector2Int characterCoordinates, List<Tile> tilesPath);
    }
}