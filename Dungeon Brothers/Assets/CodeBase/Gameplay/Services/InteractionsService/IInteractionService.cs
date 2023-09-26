using CodeBase.Common.Observables;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.Services.InteractionsService
{
    public interface IInteractionService
    {
        public void Initialize();

        public void Disable();
    }
}