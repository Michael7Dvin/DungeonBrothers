using FluentAssertions;
using NUnit.Framework;

namespace CodeBase.Tests
{
    public class HEALTH_TESTS_FOR_DEBUG
    {
        [Test]
        public void WhenTakingDamage_AndDamageMoreOrEqualCurrentHealth_ThenIsDeadShouldBeTrue()
        {
            // Arrange.
            HEALTH health = Setup.Health(100);

            // Act.
            health.TakeDamage(900);
        
            // Assert.
            health.IsDead.Should().Be(true);
        }
    }
}
