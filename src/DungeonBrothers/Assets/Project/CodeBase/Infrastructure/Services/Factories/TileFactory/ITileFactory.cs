using _Project.CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Factories.TileFactory
{
    public interface ITileFactory
    {
        UniTask WarmUp();
        UniTask<Tile> Create(Vector3 position, Vector2Int coordinates, Transform parent = null);
    }
}