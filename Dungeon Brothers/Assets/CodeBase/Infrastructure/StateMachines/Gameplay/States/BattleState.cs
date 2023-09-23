using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.TurnQueue;

namespace CodeBase.Infrastructure.StateMachines.Gameplay.States
{
    public class BattleState : IState
    {
        private readonly ITurnQueue _turnQueue;

        public BattleState(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }

        public void Enter() => 
            _turnQueue.SetFirstTurn();

        public void Exit()
        {
        }
    }
}