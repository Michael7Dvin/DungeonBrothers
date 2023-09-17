using Infrastructure.CodeBase.StateMachine.Interfaces;
using Infrastructure.GameFSM;
using UnityEngine;
using VContainer;

namespace Infrastructure.Initialization
{
    public class Bootstrapper : MonoBehaviour
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine,
            BootstrapState bootstrapState)
        {
            _stateMachine = stateMachine;
            
            _stateMachine.AddState(bootstrapState);
            
            Initialize();
        }

        public void Initialize() => _stateMachine.Enter<BootstrapState>();
    }
}