using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.Factories.TileFactory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.MapGenerator
{
    public class MapGenerator : IMapGenerator
    {
        private const int ColumnsCount = 6;
        private const int RowsCount = 10;
        private const int DistanceBetweenTiles = 1;
        
        private readonly ITileFactory _tileFactory;

        public MapGenerator(ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
        }

        private Vector3 CenteringOffset => new(-2.5f, -4.5f, 0f);

        public async UniTask<List<Tile>> GenerateMap()
        {
            List<Tile> tiles = new(ColumnsCount * RowsCount);

            GameObject root = new("Tiles");
       
            for (int row = 0; row < RowsCount; row++)
            {
                for (int col = 0; col < ColumnsCount; col++)
                {
                    Vector3 position = new Vector3(col * DistanceBetweenTiles, row * DistanceBetweenTiles);
                    Vector2Int coordinates = new Vector2Int(col, row);
                    
                    Tile tile = await _tileFactory.Create(position, coordinates, root.transform);
                    tiles.Add(tile);
                }
            }
            
            root.transform.position += CenteringOffset;

            return tiles;
        }
    }
}