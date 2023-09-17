using CodeBase.Common.FSM;
using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.Services.Logging;

namespace CodeBase.Infrastructure.GameFSM.FSM
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly ICustomLogger _logger;
        private readonly StateMachine _stateMachine;

        public GameStateMachine(ICustomLogger logger)
        {
            _logger = logger;
            _stateMachine = new StateMachine(_logger);
        }

        public void Enter<TState>() where TState : IState
        {
            _logger.Log($"Entered: {typeof(TState).Name}");
            _stateMachine.Enter<TState>();
        }

        public void Enter<TState, TArgument>(TArgument argument) where TState : IStateWithArgument<TArgument>
        {
            _logger.Log($"Entered: {typeof(TState).Name}");
            _stateMachine.Enter<TState, TArgument>(argument);
        }

        public void Add<TState>(TState state) where TState : IExitableState =>
            _stateMachine.AddState(state);
    }
}