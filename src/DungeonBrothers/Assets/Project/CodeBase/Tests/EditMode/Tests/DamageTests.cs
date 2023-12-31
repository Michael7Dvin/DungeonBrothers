﻿using FluentAssertions;
using NUnit.Framework;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;

namespace Project.CodeBase.Tests.EditMode.Tests
{
    public class DamageTests
    {
        [Test]
        public void WhenWeGetDamage_AndHeHaveBonusDamageFromMainStat_ThenDamageShouldBeRight()
        {
            // Arrange.
            CharacterStats characterStats = Create.CharacterStats(1, 1, 5, 1, 1, MainAttributeID.Strength);

            CharacterDamage characterDamage =
                Create.CharacterDamage(CharacterAttackType.Melee, characterStats, 10, 2, 3);

            // Act.
            int damage = characterDamage.GetCharacterDamage();

            // Assert.
            damage.Should().Be(23);
        }
        
        [Test]
        public void WhenWeGetDamage_AndCurrentDamageNegative_ThenDamageShouldBeNotNegative()
        {
            // Arrange.
            CharacterStats characterStats = Create.CharacterStats(1, 1, 5, 1, 1, MainAttributeID.Strength);

            CharacterDamage characterDamage =
                Create.CharacterDamage(CharacterAttackType.Melee, characterStats, -50, 2, 3);

            // Act.
            int damage = characterDamage.GetCharacterDamage();

            // Assert.
            damage.Should().BeGreaterOrEqualTo(0);
        }
    }
}