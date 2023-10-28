using _Project.CodeBase.Infrastructure.Services.SceneLoader;
using _Project.CodeBase.Infrastructure.StateMachines.Common.States;

namespace _Project.CodeBase.Infrastructure.StateMachines.App.States
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