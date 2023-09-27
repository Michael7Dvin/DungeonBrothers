using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

using CodeBase.Gameplay.Characters;
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
            IMapService mapService = new Gameplay.Services.Map.MapService();
            return mapService;
        }
        public static Character Character(int level,
            int intelligence, 
            int strength, 
            int dexterity,
            int initiative,
            int movablePoints,
            bool isMoveThroughObstacles)
        {
            Character character = new GameObject().AddComponent<Character>();
            
            character.Construct(new CharacterID(),new CharacterTeam(),
                new CharacterStats(level, intelligence, strength, dexterity, initiative, movablePoints, isMoveThroughObstacles),
                new CharacterLogic());
            
            return character;
        }

        public static CharactersProvider CharactersProvider()
        {
            CharactersProvider charactersProvider = new CharactersProvider();
            return charactersProvider;
        }

        public static TurnQueue TurnQueue(CharactersProvider charactersProvider)
        {
            TurnQueue turnQueue = new TurnQueue(new RandomService(), charactersProvider,
                new CustomLogger(new LogWriter()));
            return turnQueue;
        }

        public static TileView TileView(Material material)
        {
            TileView tileView = new TileView(material);
            return tileView;
        }

        public static IPathFinder PathFinder(IMapService mapService)
        {
            return new PathFinder(mapService);
        }
        
        public static IMoverService MoverService(IPathFinder pathFinder, IMapService mapService, ITurnQueue turnQueue)
        {
            return new MoverService(pathFinder, mapService, turnQueue);
        }

        public static TileLogic TileLogic(Vector2Int coordinate)
        {
            return new TileLogic(false, true, coordinate);
        }
    }
}