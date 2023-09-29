using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Tiles;
using UniRx;

namespace CodeBase.Gameplay.Services.InteractionsService
{
    public class InteractionService : IInteractionService
    {
        private readonly IMoverService _moverService;
        private readonly ITileSelector _tileSelector;
        private readonly CompositeDisposable _disposable = new();
        
        public InteractionService(IMoverService moverService,
            ITileSelector tileSelector)
        {
            _moverService = moverService;
            _tileSelector = tileSelector;
        }
        
        public void Initialize()
        {
            _tileSelector.CurrentTile
                .Skip(1)
                .Where(tile => _tileSelector.PreviousTile.Value == tile)
                .Subscribe(_moverService.Move)
                .AddTo(_disposable);
        }

        public void Disable() => 
            _disposable.Clear();
    }
}