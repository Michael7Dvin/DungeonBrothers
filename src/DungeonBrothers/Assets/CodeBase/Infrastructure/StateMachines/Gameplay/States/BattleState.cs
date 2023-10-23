using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;

namespace CodeBase.Infrastructure.StateMachines.Gameplay.States
{
    public class BattleState : IState
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMoverService _moverService;

        public BattleState(ITurnQueue turnQueue,
            IMoverService moverService)
        {
            _turnQueue = turnQueue;
            _moverService = moverService;
        }

        public void Enter()
        {
            _moverService.Enable();
            _turnQueue.SetFirstTurn();
        }
            

        public void Exit()
        {
        }
    }
}