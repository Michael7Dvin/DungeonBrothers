using CodeBase.Common.Observables;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.Services.InteractionsService
{
    public interface IInteractionService
    {
        public void Enable();

        public void Disable();
        
        public IReadOnlyObservable<Tile> CurrentTile { get; }
    }
}