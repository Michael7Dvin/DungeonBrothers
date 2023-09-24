using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.GameFSM.FSM;
using DG.Tweening;

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

            DOTween.Init();
        }

        public void Exit()
        {
        }
    }
}