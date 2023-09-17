using Infrastructure.CodeBase.StateMachine.Interfaces;
using UnityEngine;

namespace Infrastructure.GameFSM
{
    public class GameplayState : IState
    {
        public void Exit()
        {
            
        }

        public void Enter()
        {
           Debug.Log("gameplay");
        }
    }
}