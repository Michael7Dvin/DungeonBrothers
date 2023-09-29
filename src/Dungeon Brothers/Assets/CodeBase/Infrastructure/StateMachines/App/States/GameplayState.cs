using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.Services.SceneLoader;

namespace CodeBase.Infrastructure.StateMachines.App.States
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