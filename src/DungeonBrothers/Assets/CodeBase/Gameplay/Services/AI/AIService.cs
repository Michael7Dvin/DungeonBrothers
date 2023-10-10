using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Characters.Logic;
using CodeBase.Gameplay.Services.AI.Behaviours;
using CodeBase.Gameplay.Services.TurnQueue;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Gameplay.Services.AI
{
    public class AIService : IAIService
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IEnemyBehaviour _enemyBehaviour;
        private readonly CompositeDisposable _disposable = new();

        public AIService(ITurnQueue turnQueue,
            IEnemyBehaviour enemyBehaviour)
        {
            _turnQueue = turnQueue;
            _enemyBehaviour = enemyBehaviour;
        }

        public void Initialize()
        {
            _turnQueue.NewTurnStarted
                .Where(_ => _turnQueue.ActiveCharacter.Value.CharacterTeam == CharacterTeam.Enemy)
                .Subscribe(_ => DoTurn())
                .AddTo(_disposable);
        }

        public void Disable() =>
            _disposable.Clear();

        private async void DoTurn() => 
            await _enemyBehaviour.DoTurn();
    }
}