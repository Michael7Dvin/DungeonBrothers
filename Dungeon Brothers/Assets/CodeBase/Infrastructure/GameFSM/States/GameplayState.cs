using System.Collections.Generic;
using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IMapGenerator _mapGenerator;
        private readonly IMapService _mapService;

        public GameplayState(ISceneLoader sceneLoader, IMapGenerator mapGenerator, IMapService mapService)
        {
            _sceneLoader = sceneLoader;
            _mapGenerator = mapGenerator;
            _mapService = mapService;
        }

        public async void Enter()
        {
            _sceneLoader.Load(SceneType.Level);
            
            List<Tile> tiles = await _mapGenerator.GenerateMap();
            _mapService.ResetMap(tiles);
        }

        public void Exit()
        {
        }
    }
}