using Infrastructure.CodeBase.StateMachine.Interfaces;
using UnityEngine;

namespace Infrastructure.GameFSM
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;

        public BootstrapState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Enter()
        {
            Debug.Log("work");
        }
    }
}