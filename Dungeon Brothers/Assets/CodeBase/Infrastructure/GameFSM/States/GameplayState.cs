using CodeBase.Common.FSM.States;
using UnityEngine;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        public void Enter()
        {
           Debug.Log("gameplay");
        }

        public void Exit()
        {
        }
    }
}