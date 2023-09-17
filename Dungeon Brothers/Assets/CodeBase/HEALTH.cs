using System;
using CodeBase.Infrastructure.Services.Logging;
using UnityEngine;
using VContainer;

namespace CodeBase
{
    public class HEALTH : MonoBehaviour
    {
        private ICustomLogger _logger;
        private I_SOME_SERVICE _someService;

        [Inject]
        public void InjectDependencies(ICustomLogger logger, I_SOME_SERVICE someService)
        {
            _logger = logger;
            _someService = someService;
        }

        public void Construct(float health)
        {
            Health = health;
        }

        public float Health { get; private set; }
        public bool IsDead { get; private set; }

        public void TakeDamage(float damage)
        {
            if (damage <= 0)
            {
                _logger.LogError($"{nameof(damage)} should be above 0");
                throw new ArgumentException($"{nameof(damage)} should be above 0");
            }

            float newHealth = Health - damage;

            if (newHealth <= 0)
            {
                Health = 0;
                IsDead = true;
            }
            
            _someService.DoSomething();
        }
    }
}