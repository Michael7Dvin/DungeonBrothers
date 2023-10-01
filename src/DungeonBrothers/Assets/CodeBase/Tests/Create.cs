using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using UnityEngine;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.Random;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;

namespace CodeBase.Tests
{
    public class Create
    {
        public static Tile Tile()
        {
            GameObject prefab = new GameObject();
            prefab.AddComponent<SpriteRenderer>();
            Tile tile = prefab.AddComponent<Tile>();
            
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

        public static IMapService MapService() => 
            new Gameplay.Services.Map.MapService();

        public static ICharactersProvider CharactersProvider() => 
            new CharactersProvider();

        public static ITurnQueue TurnQueue(ICharactersProvider charactersProvider) => 
            new Gameplay.Services.TurnQueue.TurnQueue(new RandomService(), charactersProvider, new CustomLogger(new LogWriter()));

        public static TileView TileView(Material material) => 
            new(material);

        public static IPathFinder PathFinder(IMapService mapService) => 
            new PathFinder(mapService);

        public static IMoverService MoverService(IPathFinder pathFinder, IMapService mapService, ITurnQueue turnQueue) => 
            new Gameplay.Services.Move.MoverService(pathFinder, mapService, turnQueue);

        public static TileLogic TileLogic(Vector2Int coordinate) =>
            new(false, true, coordinate);
    }
}