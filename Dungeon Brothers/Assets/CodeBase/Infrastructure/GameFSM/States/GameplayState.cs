using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.TurnQueue;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly ITurnQueue _turnQueue;

        public GameplayState(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }

        public void Enter()
        {
            _turnQueue.Initialize();
            _turnQueue.SetFirstTurn();
        }

        public void Exit()
        {
        }
    }
}