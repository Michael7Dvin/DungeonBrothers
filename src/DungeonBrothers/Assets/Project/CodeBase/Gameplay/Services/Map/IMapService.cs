using System.Collections.Generic;
using Project.CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Services.Map
{
    public interface IMapService
    {
        void ResetMap(IEnumerable<Tile> map);
        bool TryGetTile(Vector2Int coordinates, out Tile tile);
        List<Tile> GetNeighbors(Vector2Int coordinates);
    }
}