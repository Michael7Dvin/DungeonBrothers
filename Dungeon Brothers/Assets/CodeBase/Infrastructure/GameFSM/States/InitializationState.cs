using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.GameFSM.FSM;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _stateMachine;

        public InitializationState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}