using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.Services.Providers.LevelData;
using CodeBase.Infrastructure.Services.Providers.ServiceProvider;
using CodeBase.Infrastructure.Services.SceneLoading;
using UnityEngine;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IServiceProvider _serviceProvider;

        public GameplayState(ISceneLoader sceneLoader, 
            IServiceProvider serviceProvider)
        {
            _sceneLoader = sceneLoader;
            _serviceProvider = serviceProvider;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneType.Level);
            

            await _serviceProvider.LevelDataProvider.Value.CreateLevel();
        }

        public void Exit()
        {
        }
    }
}