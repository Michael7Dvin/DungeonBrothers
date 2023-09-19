using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.Services.Factories.UI;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly ICommonUIFactory _commonUIFactory;
        private readonly IGameStateMachine _gameStateMachine;

        public InitializationState(ICommonUIFactory commonUIFactory, IGameStateMachine gameStateMachine)
        {
            _commonUIFactory = commonUIFactory;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            await _commonUIFactory.Create();
            _gameStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}