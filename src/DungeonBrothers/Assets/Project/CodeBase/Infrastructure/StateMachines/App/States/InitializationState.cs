using DG.Tweening;
using Project.CodeBase.Infrastructure.StateMachines.App.FSM;
using Project.CodeBase.Infrastructure.StateMachines.Common.States;

namespace Project.CodeBase.Infrastructure.StateMachines.App.States
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