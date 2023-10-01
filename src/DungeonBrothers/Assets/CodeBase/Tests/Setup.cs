using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Tiles;
using UnityEngine;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using NSubstitute;

namespace CodeBase.Tests
{
    public class Setup
    {
        public static Tile Tile(Vector2Int coordinates)
        {
            Tile tile = Create.Tile();
            
            TileView tileView = Create.TileView(tile.GetComponent<Material>());
            TileLogic tileLogic = Create.TileLogic(coordinates);

            tile.Construct(tileLogic, tileView);
            return tile;
        }

        public static IMapService MapService(List<Tile> tiles)
        {
            IMapService mapService = Create.MapService();
            mapService.ResetMap(tiles);
            return mapService;
        }
        
        public static IMapService MapService(int rows, int columns)
        {
            List<Tile> tiles = Create.TileMap(rows, columns); 
            IMapService mapService = Create.MapService();
            mapService.ResetMap(tiles);
            return mapService;
        }
        
        public static ITurnQueue TurnQueue(params ICharacter[] characters)
        {
            ICharactersProvider charactersProvider = Create.CharactersProvider();

            ITurnQueue turnQueue = Create.TurnQueue(charactersProvider);
            turnQueue.Initialize();

            foreach (ICharacter character in characters) 
                charactersProvider.Add(character, null);
            
            return turnQueue;
        }

        public static IMoverService MoverService(ICharacter character, int mapRows, int mapColumns)
        {
            ITurnQueue turnQueue = TurnQueue(character);
            IMapService mapService = MapService(mapRows, mapColumns);

            IPathFinder pathFinder = Create.PathFinder(mapService);
            IMoverService moverService = Create.MoverService(pathFinder, mapService, turnQueue);
            moverService.Enable();

            turnQueue.SetFirstTurn();
            
            return moverService;
        }

        public static IMoverService MoverService(ICharacter character, IMapService mapService)
        {
            ITurnQueue turnQueue = TurnQueue(character);

            IPathFinder pathFinder = Create.PathFinder(mapService);
            IMoverService moverService = Create.MoverService(pathFinder, mapService, turnQueue);
            moverService.Enable();

            turnQueue.SetFirstTurn();
            
            return moverService;
        }

        public static void ObstaclesAroundZeroPosition(IMapService mapService)
        {
            ICharacter character1 = Substitute.For<ICharacter>();
            ICharacter character2 = Substitute.For<ICharacter>();
            
            if (mapService.TryGetTile(new Vector2Int(0, 1), out Tile obstacleTileOnRight))
                obstacleTileOnRight.Logic.Occupy(character1);
            if (mapService.TryGetTile(new Vector2Int(1, 0), out Tile obstacleTileOnTop))
                obstacleTileOnTop.Logic.Occupy(character2);
        }

        public static ICharacter CharacterForMovement(int movePoints, bool isMoveThroughObstacles)
        {
            ICharacter character = Substitute.For<ICharacter>();
            character
                .When(_ => _.UpdateCoordinate(Arg.Any<Vector2Int>()))
                .Do(_ => character.Coordinate.Returns(_.Arg<Vector2Int>()));

            character.MovementStats.Returns(new MovementStats(movePoints, isMoveThroughObstacles));
            return character;
        }

        public static ICharacter CharacterForTurnQueue(int level, int initiative)
        {
            ICharacter character = Substitute.For<ICharacter>();
            character
                .CharacterStats
                .Returns(new CharacterStats(level, 1, 1, 1, initiative));

            return character;
        }
    }
}