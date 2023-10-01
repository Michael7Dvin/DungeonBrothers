using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Gameplay.Services.InteractionsService
{
    public class InteractionService : IInteractionService
    {
        private readonly IMoverService _moverService;
        private readonly ITileSelector _tileSelector;
        private readonly CompositeDisposable _disposable = new();
        
        public bool IsInteract { get; private set; }
        
        public InteractionService(IMoverService moverService,
            ITileSelector tileSelector)
        {
            _moverService = moverService;
            _tileSelector = tileSelector;
        }

        private async UniTask Interact(Tile tile)
        {
            IsInteract = true;
            await _moverService.Move(tile.Logic.Coordinates);
            IsInteract = false;
        }
        
        public void Initialize()
        {
            _tileSelector.CurrentTile
                .Skip(1)
                .Where(tile => IsInteract == false)
                .Where(tile => _tileSelector.PreviousTile.Value == tile)
                .Subscribe(tile => Interact(tile))
                .AddTo(_disposable);
        }

        public void Disable() => 
            _disposable.Clear();
    }
}