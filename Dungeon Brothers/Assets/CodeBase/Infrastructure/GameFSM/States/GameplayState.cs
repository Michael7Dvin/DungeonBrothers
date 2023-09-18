using System.Collections.Generic;
using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly IMapGenerator _mapGenerator;
        private readonly IMapService _mapService;

        public GameplayState(IMapGenerator mapGenerator, IMapService mapService)
        {
            _mapGenerator = mapGenerator;
            _mapService = mapService;
        }

        public async void Enter()
        {
            List<Tile> tiles = await _mapGenerator.GenerateMap();
            _mapService.ResetMap(tiles);

            foreach (Tile tile in _mapService.GetNeighbors(new Vector2Int(3,3)))
            {
                Object.Destroy(tile.gameObject);
            }

        }

        public void Exit()
        {
        }
    }
}