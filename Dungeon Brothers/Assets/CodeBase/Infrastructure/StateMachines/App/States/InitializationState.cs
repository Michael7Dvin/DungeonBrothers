using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.StateMachines.App.FSM;
using DG.Tweening;

namespace CodeBase.Infrastructure.StateMachines.App.States
{
    public class InitializationState : IState
    {
        private readonly IAppStateMachine _appStateMachine;

        public InitializationState(IAppStateMachine appStateMachine)
        {
            _appStateMachine = appStateMachine;
        }

        public void Enter()
        {
            DOTween.Init();
            
            _appStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}