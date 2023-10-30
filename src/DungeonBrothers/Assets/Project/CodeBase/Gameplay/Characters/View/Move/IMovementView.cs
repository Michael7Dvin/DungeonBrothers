using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Tiles;

namespace Project.CodeBase.Gameplay.Characters.View.Move
{
    public interface IMovementView
    {
        UniTask Move(IEnumerable<Tile> tilesPath);
    }
}