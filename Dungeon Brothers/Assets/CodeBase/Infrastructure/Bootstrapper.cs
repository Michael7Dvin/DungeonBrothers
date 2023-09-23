using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States;
using VContainer.Unity;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;
        
        private Bootstrapper(IGameStateMachine stateMachine,
            InitializationState initializationState,
            LevelLoadingState levelLoadingState,
            GameplayState gameplayState)
        {
            _stateMachine = stateMachine;
            
            _stateMachine.Add(initializationState);
            _stateMachine.Add(levelLoadingState);
            _stateMachine.Add(gameplayState);
        }

        public void Initialize() => 
            _stateMachine.Enter<InitializationState>();
    }
}