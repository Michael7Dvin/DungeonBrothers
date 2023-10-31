using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Gameplay.Services.Attack;
using Project.CodeBase.Gameplay.Services.Move;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Gameplay.Tiles;
using UniRx;

namespace Project.CodeBase.Gameplay.Services.InteractionsService
{
    public class InteractionService : IInteractionService
    {
        private readonly IMoverService _moverService;
        private readonly IAttackService _attackService;
        private readonly ITileSelector _tileSelector;
        private readonly ITurnQueue _turnQueue;
        
        private readonly CompositeDisposable _disposable = new();
        
        public InteractionService(IMoverService moverService,
            IAttackService attackService,
            ITurnQueue turnQueue,
            ITileSelector tileSelector)
        {
            _moverService = moverService;
            _attackService = attackService;
            _turnQueue = turnQueue;
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
            _turnQueue.NewTurnStarted
                .Subscribe(_ =>
                {
                    if (_turnQueue.ActiveCharacter.Value.Team == CharacterTeam.Enemy)
                        IsInteract = true;
                    else
                        IsInteract = false;
                })
                .AddTo(_disposable);
            
            _tileSelector.CurrentTile
                .Skip(1)
                .Where(_ => _turnQueue.ActiveCharacter.Value.Team == CharacterTeam.Ally)
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