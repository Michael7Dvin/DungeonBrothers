using System;
using CodeBase.Infrastructure.Services.Logger;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Gameplay.Characters.Logic
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        private readonly ReactiveProperty<int> _healthPoints = new();
        
        private ICustomLogger _customLogger;

        private const int _minHealth = 0; 
        
        private readonly ReactiveCommand _died = new();
        public int MaxHealthPoints { get; private set; }
        
        public void Construct(int healthPoints)
        {
            _healthPoints.Value = healthPoints;
            MaxHealthPoints = healthPoints;
        }

        public IReactiveCommand<Unit> Died => _died;
        public IReadOnlyReactiveProperty<int> HealthPoints => _healthPoints;

        [Inject]
        public void Inject(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        
        public void TakeDamage(int value)
        {
            if (value < 0)
                _customLogger.LogError(new Exception($"Damage taken: {value} - Damage can't be less than zero"));

            _healthPoints.Value = Mathf.Clamp(_healthPoints.Value - value, _minHealth, MaxHealthPoints);
            TryDie();
        }

        private void TryDie()
        {
            if (_healthPoints.Value == 0)
            {
                _died.Execute();
                Destroy(gameObject);
            }
        }

        public void Heal(int value)
        {
            if (value < 0)
                _customLogger.LogError(new Exception($"Heal taken: {value} - Heal can't be less than zero"));

            _healthPoints.Value = Mathf.Clamp(_healthPoints.Value + value, _minHealth, MaxHealthPoints);
        }
    }
}