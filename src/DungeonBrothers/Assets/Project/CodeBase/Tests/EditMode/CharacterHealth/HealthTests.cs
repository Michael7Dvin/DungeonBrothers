using System;
using FluentAssertions;
using NUnit.Framework;
using Project.CodeBase.Gameplay.Characters.Logic.Healths;

namespace Project.CodeBase.Tests.EditMode.CharacterHealth
{
    public class HealthTests
    {
        [Test]
        public void WhenTakingDamage_AndDamageIsNotNegative_ThenHealthPointsShouldBeDecreased()
        {
            // Arrange.
            const int initialHealthPoints = 20;
            const int damage = 5;
            
            Health health = Create.Health(initialHealthPoints);
            
            // Act.
            health.TakeDamage(damage);

            // Assert.
            health.HealthPoints.Value.Should().Be(initialHealthPoints - damage);
        }
        
        [Test]
        public void WhenTakingDamage_AndDamageIsNegative_ThenShouldThrowArgumentException()
        {
            // Arrange.
            const int initialHealthPoints = 20;
            const int damage = -5;
            
            Health health = Create.Health(initialHealthPoints);
            
            // Act.
            Action takeDamageAction = () => health.TakeDamage(damage);

            // Assert.
            takeDamageAction.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void WhenHealthHeal_AndHealIsNotNegative_ThenHealthPointsShouldBeIncrease()
        {
            // Arrange.
            const int initialHealthPoints = 20;
            const int damage = 10;
            const int heal = 5;
            
            Health health = Create.Health(initialHealthPoints);
            
            // Act.
            health.TakeDamage(damage);
            health.Heal(heal);

            // Assert.
            health.HealthPoints.Value.Should().Be(initialHealthPoints - damage + heal);
        }
        
        [Test]
        public void WhenHealthHeal_AndHealIsNegative_ThenShouldThrowArgumentException()
        {
            // Arrange.
            const int initialHealthPoints = 20;
            const int damage = 10;
            const int heal = -5;
            
            Health health = Create.Health(initialHealthPoints);
            
            // Act.
            health.TakeDamage(damage);
            Action healAction = () => health.Heal(heal);

            // Assert.
            healAction.Should().Throw<ArgumentException>();
        }
    }
}