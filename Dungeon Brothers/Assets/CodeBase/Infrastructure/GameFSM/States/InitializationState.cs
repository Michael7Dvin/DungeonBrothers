using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.GameFSM.FSM;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public InitializationState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}