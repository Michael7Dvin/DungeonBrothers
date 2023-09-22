using System.Collections.Generic;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Random;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logging;
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
            IMapService mapService = new Gameplay.Services.MapService.MapService();
            return mapService;
        }
        public static ICharacter Character(int level,
            int intelligence, 
            int strength, 
            int dexterity,
            int initiative)
        {
            Character character = new Character();
            
            character.Construct(new CharacterID(),
                new CharacterStats(level, intelligence, strength, dexterity, initiative),
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
    }
}