using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Tiles;
using UnityEngine;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;

namespace CodeBase.Tests
{
    public class Setup
    {
        public static Tile Tile(Vector2Int coordinates)
        {
            Tile tile = Create.Tile();

            TileView tileView = Create.TileView(tile.GetComponent<Material>());

            tile.Construct(coordinates,tileView);
            return tile;
        }

        public static IMapService MapService(List<Tile> tiles)
        {
            IMapService mapService = Create.MapService();
            mapService.ResetMap(tiles);
            return mapService;
        }
        
        public static ITurnQueue TurnQueue(CharactersProvider charactersProvider)
        {
            ITurnQueue turnQueue = Create.TurnQueue(charactersProvider);
            turnQueue.Initialize();
            return turnQueue;
        }

        public static IMoverService MoverService(IMapService mapService, ITurnQueue turnQueue)
        {
            IPathFinder pathFinder = Create.PathFinder(mapService);

            IMoverService moverService = Create.MoverService(pathFinder, mapService, turnQueue);

            return moverService;
        }

        public static void ObstaclesAroundZeroPosition(IMapService mapService, Character character)
        {
            if (mapService.TryGetTile(new Vector2Int(0, 1), out Tile obstacleTileOnRight))
                obstacleTileOnRight.Occupy(character);
            if (mapService.TryGetTile(new Vector2Int(1, 0), out Tile obstacleTileOnTop))
                obstacleTileOnTop.Occupy(character);
            if (mapService.TryGetTile(new Vector2Int(1, 1), out Tile obstacleTileOnTopRight))
                obstacleTileOnTopRight.Occupy(character);
        }
    }
}