using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Characters.Logic;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.Random;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using NSubstitute;
using UnityEngine;

namespace CodeBase.Tests.EditMode
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

        public static Health Health() =>
            new GameObject().AddComponent<Health>();

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

        public static IMoverService MoverService(IPathFinder pathFinder, IMapService mapService, ITurnQueue turnQueue) => 
            new Gameplay.Services.Move.MoverService(pathFinder, mapService, turnQueue);

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
            MainAttributeID mainAttributeID = MainAttributeID.Strength,
            int movePoints = 1,
            bool isMoveThroughObstacles = false)
        {
            CharacterStats characterStats = new CharacterStats
            {
                Initiative = initiative,
                Level = level,
                Strength = strength,
                Dexterity = dexterity,
                Intelligence = intelligence,
                MainAttributeID = mainAttributeID,
                MovePoints = movePoints,
                IsMoveThroughObstacles = isMoveThroughObstacles,
            };
            return characterStats;
        }
    }
}