using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.Services.Providers.SceneServicesProvider;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ISceneServicesProvider _sceneServicesProvider;

        public GameplayState(ISceneLoader sceneLoader, 
            ISceneServicesProvider sceneServicesProvider)
        {
            _sceneLoader = sceneLoader;
            _sceneServicesProvider = sceneServicesProvider;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneType.Level);
            await _sceneServicesProvider.LevelSpawner.CreateLevel();
        }

        public void Exit()
        {
        }
    }
}