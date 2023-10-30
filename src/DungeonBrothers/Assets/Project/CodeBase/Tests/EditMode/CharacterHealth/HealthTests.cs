using FluentAssertions;
using NUnit.Framework;
using Project.CodeBase.Gameplay.Characters.Logic.Health;

namespace Project.CodeBase.Tests.EditMode.CharacterHealth
{
    public class HealthTests
    {
        [Test]
        public void WhenHealthTakeDamage_AndDamageNotNegative_ThenHealthPointsShouldBeDecrease()
        {
            // Arrange.
            Health health = Create.Health();
            health.Construct(20);
            
            // Act.
            health.TakeDamage(5);

            // Assert.
            health.HealthPoints.Value.Should().NotBe(20);
        }
        
        [Test]
        public void WhenHealthTakeDamage_AndDamageNegative_ThenHealthPointsShouldBeNotChange()
        {
            // Arrange.
            Health health = Create.Health();
            health.Construct(20);
            
            // Act.
            health.TakeDamage(-1);

            // Assert.
            health.HealthPoints.Value.Should().Be(20);
        }
        
        [Test]
        public void WhenHealthHeal_AndHealNotNegative_ThenHealthPointsShouldBeIncrease()
        {
            // Arrange.
            Health health = Create.Health();
            health.Construct(20);
            
            // Act.
            health.TakeDamage(10);
            health.Heal(5);

            // Assert.
            health.HealthPoints.Value.Should().Be(15);
        }
        
        [Test]
        public void WhenHealthHeal_AndHealNegative_ThenHealthPointsShouldBeNotChange()
        {
            // Arrange.
            Health health = Create.Health();
            health.Construct(20);
            
            // Act.
            health.TakeDamage(10);
            health.Heal(-5);

            // Assert.
            health.HealthPoints.Value.Should().Be(10);
        }
    }
}