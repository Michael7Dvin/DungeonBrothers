using _Project.CodeBase.Gameplay.Services.InteractionsService;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using VContainer;

namespace _Project.CodeBase.UI.TurnQueue.Button
{
    public class SkipTurnViewModel
    {
        private ITurnQueue _turnQueue;
        private IInteractionService _interactionService;

        [Inject]
        public void Inject(ITurnQueue turnQueue,
            IInteractionService interactionService)
        {
            _turnQueue = turnQueue;
            _interactionService = interactionService;
        }

        public void SkipTurn()
        {
            if (_interactionService.IsInteract == false)
                _turnQueue.SetNextTurn();
        }
    }
}