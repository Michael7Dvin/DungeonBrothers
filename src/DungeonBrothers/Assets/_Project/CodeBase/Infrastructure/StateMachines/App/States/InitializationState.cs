using _Project.CodeBase.Infrastructure.Services.Factories.Sound;
using _Project.CodeBase.Infrastructure.StateMachines.App.FSM;
using _Project.CodeBase.Infrastructure.StateMachines.Common.States;
using DG.Tweening;

namespace _Project.CodeBase.Infrastructure.StateMachines.App.States
{
    public class InitializationState : IState
    {
        private readonly IAppStateMachine _appStateMachine;
        private readonly IAudioFactory _audioFactory;

        public InitializationState(IAppStateMachine appStateMachine,
            IAudioFactory audioFactory)
        {
            _appStateMachine = appStateMachine;
            _audioFactory = audioFactory;
        }

        public async void Enter()
        {
            DOTween.Init();

            await _audioFactory.Create();
            _appStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}