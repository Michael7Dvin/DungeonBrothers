using Project.CodeBase.Infrastructure.StateMachines.App.FSM;
using Project.CodeBase.Infrastructure.StateMachines.App.States;
using VContainer.Unity;

namespace Project.CodeBase.Infrastructure.StateMachines.App
{
    public class AppBootstrapper : IInitializable
    {
        private readonly IAppStateMachine _stateMachine;
        
        private AppBootstrapper(IAppStateMachine stateMachine,
            InitializationState initializationState,
            GameplayState gameplayState)
        {
            _stateMachine = stateMachine;
            
            _stateMachine.Add(initializationState);
            _stateMachine.Add(gameplayState);
        }

        public void Initialize() => 
            _stateMachine.Enter<InitializationState>();
    }
}