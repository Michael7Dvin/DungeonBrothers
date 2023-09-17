using CodeBase.Common.FSM;
using CodeBase.Common.FSM.States;

namespace CodeBase.Infrastructure.GameFSM.FSM
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly StateMachine _stateMachine = new();
        
        public void Enter<TState>() where TState : IState
        {
            _stateMachine.Enter<TState>();
        }

        public void Enter<TState, TArgument>(TArgument argument) where TState : IStateWithArgument<TArgument>
        {
            _stateMachine.Enter<TState, TArgument>(argument);
        }

        public void Add<TState>(TState state) where TState : IExitableState =>
            _stateMachine.AddState(state);
    }
}