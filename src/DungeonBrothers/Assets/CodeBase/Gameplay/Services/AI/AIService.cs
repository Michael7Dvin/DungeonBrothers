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
        private readonly IMeleeBehaviour _meleeBehaviour;
        private readonly CompositeDisposable _disposable = new();

        public AIService(ITurnQueue turnQueue,
            IMeleeBehaviour meleeBehaviour)
        {
            _turnQueue = turnQueue;
            _meleeBehaviour = meleeBehaviour;
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

        private async void DoTurn()
        {
            ICharacter character = _turnQueue.ActiveCharacter.Value;
            
            switch (character.CharacterDamage.CharacterAttackType)
            {
                case CharacterAttackType.Melee:
                    await _meleeBehaviour.DoTurn();
                    break;
                case CharacterAttackType.Ranged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}