using Infrastructure.CodeBase.StateMachine.Interfaces;
using Infrastructure.GameFSM;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Initialization
{
    public class Bootstrapper : IInitializable
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine,
            InitializationState initializationState,
            GameplayState gameplayState)
        {
            _stateMachine = stateMachine;
            
            _stateMachine.AddState(initializationState);
            _stateMachine.AddState(gameplayState);
        }

        public void Initialize() => _stateMachine.Enter<InitializationState>();
    }
}