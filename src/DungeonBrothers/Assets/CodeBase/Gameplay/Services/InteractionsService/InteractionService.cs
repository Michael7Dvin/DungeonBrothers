using CodeBase.Gameplay.Services.Attack;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Gameplay.Services.InteractionsService
{
    public class InteractionService : IInteractionService
    {
        private readonly IMoverService _moverService;
        private readonly IAttackService _attackService;
        private readonly ITileSelector _tileSelector;
        private readonly CompositeDisposable _disposable = new();
        
        public bool IsInteract { get; private set; }
        
        public InteractionService(IMoverService moverService,
            IAttackService attackService,
            ITileSelector tileSelector)
        {
            _moverService = moverService;
            _attackService = attackService;
            _tileSelector = tileSelector;
        }

        private async void Interact(Tile tile)
        {
            IsInteract = true;
            
            if (tile.Logic.Character != null)
                _attackService.Attack(tile.Logic.Character);
            
            await _moverService.Move(tile.Logic.Coordinates);
            
            IsInteract = false;
        }
        
        public void Initialize()
        {
            _tileSelector.CurrentTile
                .Skip(1)
                .Where(tile => IsInteract == false)
                .Where(tile => _tileSelector.PreviousTile.Value == tile)
                .Subscribe(Interact)
                .AddTo(_disposable);
        }

        public void Disable() => 
            _disposable.Clear();
    }
}