using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.Factories.TileFactory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Gameplay.Services.MapGenerator
{
    public class MapGenerator : IMapGenerator
    {
        private const int ColumnsCount = 8;
        private const int RowsCount = 8;
        private const int DistanceBetweenTiles = 1;
        
        private readonly ITileFactory _tileFactory;
        private readonly IObjectResolver _objectResolver;

        public MapGenerator(ITileFactory tileFactory, IObjectResolver objectResolver)
        {
            _tileFactory = tileFactory;
            _objectResolver = objectResolver;
        }

        private Vector3 CenteringOffset => new(-3.5f, -3.5f, 0f);

        public async UniTask<List<Tile>> GenerateMap()
        {
            List<Tile> tiles = new(ColumnsCount * RowsCount);

            GameObject root = _objectResolver.Instantiate(new GameObject("Tiles"));
       
            for (int row = 0; row < RowsCount; row++)
            {
                for (int col = 0; col < ColumnsCount; col++)
                {
                    Vector3 position = new Vector3(col * DistanceBetweenTiles, row * DistanceBetweenTiles);
                    position += CenteringOffset;

                    Vector2Int coordinates = new Vector2Int(col, row);
                    
                    Tile tile = await _tileFactory.Create(position, coordinates, root.transform);
                    tiles.Add(tile);
                }
            }
            
            return tiles;
        }
    }
}