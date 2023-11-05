using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace Project.CodeBase.Infrastructure.Services.Factories.TileFactory
{
    public interface ITileFactory
    {
        UniTask WarmUp();
        UniTask<Tile> Create(Vector3 position, Vector2Int coordinates, Transform parent = null);

        public UniTask<TransitionTile> CreateTransitionTile(Vector3 position, Vector2Int coordinates,
            Transform parent);
    }
}