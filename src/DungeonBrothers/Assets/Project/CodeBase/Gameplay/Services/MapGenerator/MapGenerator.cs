using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.Factories.TileFactory;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Services.MapGenerator
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
                    TrySetPassageTile(tile, col, row);
                }
            }
            
            root.transform.position += CenteringOffset;

            return tiles;
        }

        private void TrySetPassageTile(Tile tile, int x, int y)
        {
            if (IsTopOrDownDoor(tile, x, y)) 
                return;

            IsLeftOrRightDoor(tile, x, y);
        }

        private bool IsTopOrDownDoor(Tile tile, int x, int y)
        {
            if (x == ColumnsCount / 2 - 1 || x == ColumnsCount / 2)
            {
                
                if (IsDownDoor(y))
                {
                    SetPassageTile(tile, Direction.Down);
                    return true;
                }

                if (IsTopDoor(y))
                {
                    SetPassageTile(tile, Direction.Top);
                    return true;
                }
            }

            return false;
        }

        private void IsLeftOrRightDoor(Tile tile, int x, int y)
        {
            if (y == RowsCount / 2 - 1 || y == RowsCount / 2)
            {
                if (IsLeftRoom(x))
                {
                    SetPassageTile(tile, Direction.Left);
                    return;
                }

                if (IsRightRoom(x)) 
                    SetPassageTile(tile, Direction.Right);
            }
        }

        private void SetPassageTile(Tile tile, Direction direction)
        {
            tile.Logic.PassageDirection = direction;
            tile.Logic.IsPassage = true;
            tile.Logic.IsEnterInNewRoom = true;
        }

        private bool IsRightRoom(int x) => 
            x == ColumnsCount - 1;

        private bool IsLeftRoom(int x) => 
            x == 0;

        private bool IsDownDoor(int y) => 
            y == 0;

        private bool IsTopDoor(int y) => 
            y == RowsCount - 1;
    }
}