using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.Gameplay.Services.AI.Behaviours;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using UniRx;

namespace _Project.CodeBase.Gameplay.Services.AI
{
    public class AIService : IAIService
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IAttackBehaviour _attackBehaviour;
        private readonly CompositeDisposable _disposable = new();

        public AIService(ITurnQueue turnQueue,
            IAttackBehaviour attackBehaviour)
        {
            _turnQueue = turnQueue;
            _attackBehaviour = attackBehaviour;
        }

        public void Initialize()
        {
            _turnQueue.NewTurnStarted
                .Where(_ => _turnQueue.ActiveCharacter.Value.Team == CharacterTeam.Enemy)
                .Subscribe(_ => DoTurn())
                .AddTo(_disposable);
        }

        public void Disable() =>
            _disposable.Clear();

        private async void DoTurn() => 
            await _attackBehaviour.DoTurn();
    }
}