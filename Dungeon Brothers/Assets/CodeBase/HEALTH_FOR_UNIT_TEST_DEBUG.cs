using System;
using CodeBase.Infrastructure.Services.Logging;

namespace CodeBase
{
    public class HEALTH_FOR_UNIT_TEST_DEBUG
    {
        private readonly ICustomLogger _logger;
        private readonly I_SOME_SERVICE_FOR_UNIT_TEST_DEBUG _someService;

        public HEALTH_FOR_UNIT_TEST_DEBUG(ICustomLogger logger, I_SOME_SERVICE_FOR_UNIT_TEST_DEBUG someService)
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