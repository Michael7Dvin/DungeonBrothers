using System.Collections.Generic;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Tests
{
    public class Setup
    {
        public static Tile Tile(Vector2Int coordinates)
        {
            Tile tile = Create.Tile();
            tile.Construct(coordinates);
            return tile;
        }

        public static IMapService MapService(List<Tile> tiles)
        {
            IMapService mapService = Create.MapService();
            mapService.ResetMap(tiles);
            return mapService;
        }
    }
}