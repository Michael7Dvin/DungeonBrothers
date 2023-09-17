using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public InitializationState(IGameStateMachine stateMachine,
            ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            _stateMachine.Enter<GameplayState>();

            await _sceneLoader.Load(SceneType.Level);
        }

        public void Exit()
        {
        }
    }
}