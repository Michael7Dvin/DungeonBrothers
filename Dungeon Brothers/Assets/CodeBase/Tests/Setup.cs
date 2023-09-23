using System.Collections.Generic;
using CodeBase.Gameplay.Services.MapService;
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
    }
}