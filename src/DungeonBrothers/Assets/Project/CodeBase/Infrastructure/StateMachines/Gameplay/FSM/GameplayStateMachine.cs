using _Project.CodeBase.Infrastructure.Services.Logger;
using _Project.CodeBase.Infrastructure.StateMachines.Common;
using _Project.CodeBase.Infrastructure.StateMachines.Common.States;

namespace _Project.CodeBase.Infrastructure.StateMachines.Gameplay.FSM
{
    public class GameplayStateMachine : IGameplayStateMachine
    {
        private readonly ICustomLogger _logger;
        private readonly StateMachine _stateMachine;

        public GameplayStateMachine(ICustomLogger logger)
        {
            _logger = logger;
            _stateMachine = new StateMachine();
        }

        public void Enter<TState>() where TState : IState
        {
            _logger.Log($"Gameplay State Entered: '{typeof(TState).Name}'");
            _stateMachine.Enter<TState>();
        }

        public void Enter<TState, TArgument>(TArgument argument) where TState : IStateWithArgument<TArgument>
        {
            _logger.Log($"Gameplay State Entered: '{typeof(TState).Name}'");
            _stateMachine.Enter<TState, TArgument>(argument);
        }

        public void Add<TState>(TState state) where TState : IExitableState =>
            _stateMachine.AddState(state);
    }
}