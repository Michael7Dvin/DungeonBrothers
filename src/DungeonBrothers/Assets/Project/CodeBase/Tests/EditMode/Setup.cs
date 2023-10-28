using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.Gameplay.Characters.Logic.Health;
using _Project.CodeBase.Gameplay.Services.Attack;
using _Project.CodeBase.Gameplay.Services.Map;
using _Project.CodeBase.Gameplay.Services.Move;
using _Project.CodeBase.Gameplay.Services.PathFinder;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.Logger;
using _Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using NSubstitute;
using UnityEngine;

namespace _Project.CodeBase.Tests.EditMode
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

            Health health = Create.Health();
            
            character1.Logic.Health.Returns(health);
            character2.Logic.Health.Returns(health);

            if (mapService.TryGetTile(new Vector2Int(0, 1), out Tile obstacleTileOnRight))
                obstacleTileOnRight.Logic.Occupy(character1);


            if (mapService.TryGetTile(new Vector2Int(1, 0), out Tile obstacleTileOnTop))
                obstacleTileOnTop.Logic.Occupy(character2);
        }

        public static IAttackService AttackService(ICharacter[] characters, int range)
        {
            ITurnQueue turnQueue = TurnQueue(characters);
            turnQueue.SetFirstTurn();

            IMapService mapService = MapService(3, 3);

            foreach (var character in characters)
            {
                if (mapService.TryGetTile(character.Coordinate, out Tile tile)) 
                    tile.Logic.Occupy(character);

            }

            IPathFinder pathFinder = Create.PathFinder(mapService);
            ICustomLogger customLogger = Create.CustomLogger();

            var staticDataProvider = StaticDataProviderForAttackService(range);

            IAttackService attackService = new Gameplay.Services.Attack.AttackService(turnQueue, pathFinder,
                customLogger, staticDataProvider);

            return attackService;
        }

        private static IStaticDataProvider StaticDataProviderForAttackService(int range)
        {
            IStaticDataProvider staticDataProvider = Substitute.For<IStaticDataProvider>();

            AllGameBalanceConfig allGameBalanceConfig = ScriptableObject.CreateInstance<AllGameBalanceConfig>();
            AttackRangeConfig attackRangeConfig = ScriptableObject.CreateInstance<AttackRangeConfig>();
            allGameBalanceConfig.AttackRangeConfig = attackRangeConfig;
            attackRangeConfig.MeleeRange = range;
            attackRangeConfig.RangedRange = range;
            staticDataProvider.GameBalanceConfig.Returns(allGameBalanceConfig);
            return staticDataProvider;
        }

        public static ICharacter CharacterForAttack(int damage, int healthPoints, int initiative, CharacterAttackType characterAttackType, CharacterTeam characterTeam)
        {
            ICharacter character = CharacterForMovement(5, false, 1, initiative);
            
            Health health = Create.Health();
            health.Construct(healthPoints);
            
            character.Logic.Health.Returns(health);

            var characterDamage = Create.CharacterDamage(characterAttackType, new CharacterStats(), damage, 2, 3);

            character.Damage.Returns(characterDamage);

            character.Team.Returns(characterTeam);

            return character;
        }


        public static ICharacter CharacterForMovement(int movePoints, bool isMoveThroughObstacles, int level, int initiative)
        {
            ICharacter character = CharacterForTurnQueue(level, initiative);
            
            character
                .When(_ => _.UpdateCoordinate(Arg.Any<Vector2Int>()))
                .Do(_ => character.Coordinate.Returns(_.Arg<Vector2Int>()));

            var movementStats = Create.CharacterStats(movePoints: movePoints, isMoveThroughObstacles : isMoveThroughObstacles);

            character.Stats.Returns(movementStats);
            return character;
        }

        public static ICharacter CharacterForTurnQueue(int level, int initiative)
        {
            ICharacter character = Substitute.For<ICharacter>();
            var characterStats = Create.CharacterStats(level, initiative, 1, 1, 1, MainAttributeID.Dexterity);

            character
                .Stats
                .Returns(characterStats);
            
            Health health = Create.Health();
            character.Logic.Health.Returns(health);

            return character;
        }
    }
}