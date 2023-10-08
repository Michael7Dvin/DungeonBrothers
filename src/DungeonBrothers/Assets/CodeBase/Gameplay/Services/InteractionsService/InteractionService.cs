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
        
        public InteractionService(IMoverService moverService,
            IAttackService attackService,
            ITileSelector tileSelector)
        {
            _moverService = moverService;
            _attackService = attackService;
            _tileSelector = tileSelector;
        }
        
        public bool IsInteract { get; private set; }

        private async void Interact(Tile tile)
        {
            IsInteract = true;

            if (tile.Logic.Character != null)
            {
                await _attackService.Attack(tile.Logic.Character);
                IsInteract = false;
                return;
            }
            
            await _moverService.Move(tile);

            IsInteract = false;
        }
        
        public void Initialize()
        {
            _tileSelector.CurrentTile
                .Skip(1)
                .Where(_ => IsInteract == false)
                .Where(tile => tile != null)
                .Where(tile => _tileSelector.PreviousTile.Value == tile)
                .Subscribe(Interact)
                .AddTo(_disposable);
        }

        public void Disable() => 
            _disposable.Clear();
    }
}