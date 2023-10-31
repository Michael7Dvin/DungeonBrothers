using Project.CodeBase.Infrastructure.Services.Logger;
using Project.CodeBase.Infrastructure.StateMachines.Common;
using Project.CodeBase.Infrastructure.StateMachines.Common.States;

namespace Project.CodeBase.Infrastructure.StateMachines.App.FSM
{
    public class AppStateMachine : IAppStateMachine
    {
        private readonly ICustomLogger _logger;
        private readonly StateMachine _stateMachine;

        public AppStateMachine(ICustomLogger logger)
        {
            _logger = logger;
            _stateMachine = new StateMachine();
        }

        public void Enter<TState>() where TState : IState
        {
            _logger.Log($"App State Entered: '{typeof(TState).Name}'");
            _stateMachine.Enter<TState>();
        }

        public void Add<TState>(TState state) where TState : IExitableState =>
            _stateMachine.AddState(state);
    }
}