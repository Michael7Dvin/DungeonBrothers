using CodeBase.Gameplay.Services.TurnQueue;
using VContainer;

namespace CodeBase.UI.TurnQueue.Button
{
    public class SkipTurnViewModel
    {
        private ITurnQueue _turnQueue;
        
        [Inject]
        public void Inject(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }

        public void SkipTurn() =>
            _turnQueue.SetNextTurn();
    }
}