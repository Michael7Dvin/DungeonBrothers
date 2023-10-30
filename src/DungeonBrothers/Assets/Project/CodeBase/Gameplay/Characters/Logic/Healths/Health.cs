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
        
        public void TakeDamage(int value)
        {
            if (value < 0)
            {
                _customLogger.LogError(new Exception($"{value} can't be less than zero"));
                return;
            }
            
            _healthPoints.Value = Mathf.Clamp(_healthPoints.Value - value, MinHealth, MaxHealthPoints);
            TryDie();
        }

        public void Heal(int value)
        {
            if (value < 0)
            {
                _customLogger.LogError(new Exception($"{value} can't be less than zero"));
                return;                
            }
            
            _healthPoints.Value = Mathf.Clamp(_healthPoints.Value + value, MinHealth, MaxHealthPoints);
        }

        private void TryDie()
        {
            if (_healthPoints.Value <= 0)
                _death.Die();
        }
    }
}