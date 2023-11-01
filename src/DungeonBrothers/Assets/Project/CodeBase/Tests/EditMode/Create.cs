﻿using System.Collections.Generic;
using NSubstitute;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Gameplay.Characters.Logic.Deaths;
using Project.CodeBase.Gameplay.Characters.Logic.Healths;
using Project.CodeBase.Gameplay.Characters.Logic.Movement;
using Project.CodeBase.Gameplay.Characters.View.Move;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Services.Move;
using Project.CodeBase.Gameplay.Services.PathFinder;
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
        
        public static Tile Tile()
        {
            GameObject prefab = new GameObject();
            prefab.AddComponent<SpriteRenderer>();
            Tile tile = prefab.AddComponent<Tile>();
            
            return tile;
        }

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
            new Gameplay.Services.TurnQueue.TurnQueue(new RandomService(), charactersProvider);

        public static TileView TileView(Material material) => 
            new(material);

        public static IPathFinder PathFinder(IMapService mapService) => 
            new PathFinder(mapService);

        public static IMoverService MoverService(IPathFinder pathFinder, ITurnQueue turnQueue, IMapService mapService) => 
            new Gameplay.Services.Move.MoverService(pathFinder, turnQueue, mapService);

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

        public static Movement Movement(ICharacter character, int movePoints, bool isMoveThroughObstacles)
        {
            IMovementView movementView = Substitute.For<IMovementView>();
            return new Movement(character, movementView, isMoveThroughObstacles, movePoints);
        }
    }
}