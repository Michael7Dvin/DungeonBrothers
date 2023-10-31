using Project.CodeBase.Infrastructure.Services.SceneLoader;
using Project.CodeBase.Infrastructure.StateMachines.Common.States;

namespace Project.CodeBase.Infrastructure.StateMachines.App.States
{
    public class GameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;

        public GameplayState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter() => 
            _sceneLoader.Load(SceneType.Level);

        public void Exit()
        {
        }
    }
}