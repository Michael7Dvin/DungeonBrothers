using System.Collections.Generic;
using NSubstitute;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Gameplay.Characters.Logic.Deaths;
using Project.CodeBase.Gameplay.Characters.Logic.Healths;
using Project.CodeBase.Gameplay.Characters.Logic.Movement;
using Project.CodeBase.Gameplay.Characters.View.Move;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Services.Random;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.Logger;
using Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using UnityEngine;

namespace Project.CodeBase.Tests.EditMode
{
    public class Create
    {
        public static ICustomLogger CustomLogger() =>
            new CustomLogger(new LogWriter());


        public static Health Health(int healthPoints)
        {
            Death death = Death();
            Health health = new(healthPoints, death);
            health.Inject(CustomLogger());
            return health;
        }

        public static Death Death()
        {
            GameObject gameObject = new();
            return new Death(gameObject);
        }

        public static List<Tile> TileMap(int rows, int columns)
        {
            List<Tile> tiles = new(rows * columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var tile = Tile(new Vector2Int(i, j));
                    tiles.Add(tile);
                }
            }

            return tiles;
        }

        public static IMapService MapService() => 
            new Gameplay.Services.Map.MapService();

        public static ICharactersProvider CharactersProvider() => 
            new CharactersProvider(CustomLogger());

        public static ITurnQueue TurnQueue(ICharactersProvider charactersProvider) => 
            new Gameplay.Services.TurnQueue.TurnQueue(new RandomService(), charactersProvider);

        public static TileView TileView(Material material) => 
            new(material);
        
        public static TileLogic TileLogic(Vector2Int coordinate) =>
            new(false, true, coordinate);
        
        public static CharacterDamage CharacterDamage(CharacterAttackType characterAttackType, 
            CharacterStats characterStats, 
            int damage, 
            int bonusDamagePerMainStat, 
            int bonusDamagePerLevel)
        {
            CharacterDamage characterDamage = new CharacterDamage
            {
                CurrentDamage = damage,
                CharacterAttackType = characterAttackType,
            };
            
            characterDamage.Construct(bonusDamagePerMainStat, bonusDamagePerLevel, characterStats, CustomLogger());
            return characterDamage;
        }

        public static CharacterStats CharacterStats(int level = 1,
            int initiative = 1, 
            int strength = 1, 
            int dexterity = 1,
            int intelligence = 1,
            MainAttributeID mainAttributeID = MainAttributeID.Strength)
        {
            CharacterStats characterStats = new CharacterStats
            {
                Initiative = initiative,
                Level = level,
                Strength = strength,
                Dexterity = dexterity,
                Intelligence = intelligence,
                MainAttributeID = mainAttributeID,
            };
            return characterStats;
        }
        
        public static Tile Tile(Vector2Int coordinates)
        {
            GameObject prefab = new GameObject();
            prefab.AddComponent<SpriteRenderer>();
            Tile tile = prefab.AddComponent<Tile>();
            
            TileView tileView = TileView(tile.GetComponent<Material>());
            TileLogic tileLogic = TileLogic(coordinates);

            tile.Construct(tileLogic, tileView);
            return tile;
        }

        public static IMapService MapService(List<Tile> tiles)
        {
            IMapService mapService = MapService();
            mapService.ResetMap(tiles);
            return mapService;
        }
        
        public static IMapService MapService(int rows, int columns)
        {
            List<Tile> tiles = TileMap(rows, columns); 
            IMapService mapService = MapService();
            mapService.ResetMap(tiles);
            return mapService;
        }
        
        public static ITurnQueue TurnQueue(params ICharacter[] characters)
        {
            ICharactersProvider charactersProvider = CharactersProvider();

            ITurnQueue turnQueue = TurnQueue(charactersProvider);
            turnQueue.Initialize();

            foreach (ICharacter character in characters) 
                charactersProvider.Add(character, null);
            
            return turnQueue;
        }

        public static ICharacter CharacterForTurnQueue(int level, int initiative)
        {
            ICharacter character = Substitute.For<ICharacter>();
            CharacterStats characterStats = CharacterStats(level: level, initiative: initiative);

            character.Stats.Returns(characterStats);
            
            Health health = Health(20);
            character.Logic.Health.Returns(health);

            return character;
        }

        public static List<Tile> TilePath(Vector2Int start, Vector2Int end)
        {
            List<Tile> path = new();
            
            for (Vector2Int coordinate = start; coordinate != end;)
            {
                IterateByXAndCreateTile();
                IterateByYAndCreateTile();

                void IterateByXAndCreateTile()
                {
                    if (coordinate.x != end.x)
                    {
                        if (coordinate.x > end.x)
                            coordinate.x--;
                        else
                            coordinate.x++;

                        path.Add(Tile(coordinate));
                    }
                }

                void IterateByYAndCreateTile()
                {
                    if (coordinate.y != end.y)
                    {
                        if (coordinate.y > end.y)
                            coordinate.y--;
                        else
                            coordinate.y++;

                        path.Add(Tile(coordinate));
                    }
                }
            }

            return path;
        }

        public static IMovement Movement(Tile startTile, bool isMoveThroughObstacles, int startMovePoints)
        {
            ICharacter character = Substitute.For<ICharacter>();
            IMovementView movementView = Substitute.For<IMovementView>();
            character.IsInBattle = true;
            
            Movement movement = new(character, movementView, isMoveThroughObstacles, startMovePoints);
            movement.Teleport(startTile);
            return movement;
        }
    }
}