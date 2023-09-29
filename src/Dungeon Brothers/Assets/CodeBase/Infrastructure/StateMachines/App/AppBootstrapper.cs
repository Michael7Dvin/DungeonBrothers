using CodeBase.Infrastructure.StateMachines.App.FSM;
using CodeBase.Infrastructure.StateMachines.App.States;
using CodeBase.Infrastructure.StateMachines.Gameplay.States;
using VContainer.Unity;

namespace CodeBase.Infrastructure.StateMachines.App
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