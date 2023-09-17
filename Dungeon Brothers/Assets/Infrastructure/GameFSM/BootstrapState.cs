using Infrastructure.CodeBase.StateMachine.Interfaces;
using UnityEngine;

namespace Infrastructure.GameFSM
{
    public class InitializationState : IState
    {
        private readonly IStateMachine _stateMachine;

        public InitializationState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Exit()
        {
            Debug.Log("22");
        }

        public void Enter()
        {
            _stateMachine.Enter<GameplayState>();
        }
    }
}