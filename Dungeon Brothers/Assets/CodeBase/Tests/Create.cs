using System.Collections.Generic;
using CodeBase.Gameplay.Animations.Move;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.View;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.PathFinder;
using CodeBase.Gameplay.Services.Random;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

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

           MoveAnimation moveAnimation = MoveAnimation();
            
            character.Construct(new CharacterID(),new CharacterTeam(),
                new CharacterStats(level, intelligence, strength, dexterity, initiative, movablePoints, isMoveThroughObstacles),
                new CharacterLogic(), new CharacterAnimation(moveAnimation));
            
            return character;
        }

        public static MoveAnimation MoveAnimation()
        {
            return new GameObject().AddComponent<MoveAnimation>();
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
            IPathFinder pathFinder = new PathFinder(mapService);
            
            return pathFinder;
        }

        public static IMoverService MoverService(IPathFinder pathFinder, IMapService mapService, ITurnQueue turnQueue)
        {
            MoverService moverService = new MoverService(pathFinder, mapService, turnQueue);
            
            return moverService;
        }

        public static TileLogic TileLogic(Vector2Int coordinate)
        {
            return new TileLogic(false, true, coordinate);
        }
    }
}