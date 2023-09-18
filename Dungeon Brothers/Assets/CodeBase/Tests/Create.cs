using System.Collections.Generic;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Tests
{
    public class Create
    {
        public static Tile Tile()
        {
            Tile tile = new GameObject().AddComponent<Tile>();
            return tile;
        }

        public static List<Tile> TileMap(int rows, int columns)
        {
            List<Tile> tiles = new(rows * columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var tile = Setup.Tile(new Vector2Int(i, j));
                    tiles.Add(tile);
                }
            }

            return tiles;
        }

        public static IMapService MapService()
        {
            IMapService mapService = new Gameplay.Services.MapService.MapService();
            return mapService;
        }
    }
}