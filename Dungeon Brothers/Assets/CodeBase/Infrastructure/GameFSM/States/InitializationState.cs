using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.Services.Factories.Common;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly ICommonUIFactory _commonUIFactory;
        
        public InitializationState(ICommonUIFactory commonUIFactory)
        {
            _commonUIFactory = commonUIFactory;
        }

        public async void Enter()
        {
            await _commonUIFactory.Create();
        }

        public void Exit()
        {
        }
    }
}