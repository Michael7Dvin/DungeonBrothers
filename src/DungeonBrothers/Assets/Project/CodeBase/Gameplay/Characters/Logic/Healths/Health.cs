using System;
using Project.CodeBase.Gameplay.Characters.Logic.Deaths;
using Project.CodeBase.Infrastructure.Services.Logger;
using UniRx;
using UnityEngine;
using VContainer;

namespace Project.CodeBase.Gameplay.Characters.Logic.Healths
{
    public class Health : IHealth
    {
        private const int MinHealth = 0;
        private readonly IDeath _death;
        
        private readonly ReactiveProperty<int> _healthPoints = new();
        
        private ICustomLogger _customLogger;

        [Inject]
        public void Inject(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        
        public Health(int healthPoints, IDeath death)
        {
            _healthPoints.Value = healthPoints;
            MaxHealthPoints = healthPoints;
            
            _death = death;
        }

        public int MaxHealthPoints { get; }
        public IReadOnlyReactiveProperty<int> HealthPoints => _healthPoints;
        
        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                _customLogger.LogError(new ArgumentException($"{damage} can't be less than zero"));
                return;
            }
            
            _healthPoints.Value = Mathf.Clamp(_healthPoints.Value - damage, MinHealth, MaxHealthPoints);
            TryDie();
        }

        public void Heal(int heal)
        {
            if (heal < 0)
            {
                _customLogger.LogError(new ArgumentException($"{heal} can't be less than zero"));
                return;                
            }
            
            _healthPoints.Value = Mathf.Clamp(_healthPoints.Value + heal, MinHealth, MaxHealthPoints);
        }

        private void TryDie()
        {
            if (_healthPoints.Value <= 0)
                _death.Die();
        }
    }
}