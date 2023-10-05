using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Characters.Logic;
using CodeBase.Gameplay.Services.Attack;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace CodeBase.Tests.EditMode.AttackService
{
    public class AttackServiceTests
    {
        [Test]
        public void WhenCharacterAttack_AndAnotherCharacterInAnotherTeam_ThenCharacterShouldDealDamage()
        {
            // Arrange.
            ICharacter attackingCharacter = Setup.CharacterForAttack(21, 3, CharacterAttackType.Ranged, CharacterTeam.Ally);
            ICharacter receivingCharacter = Setup.CharacterForAttack(20, 1, CharacterAttackType.Ranged, CharacterTeam.Enemy);

            IAttackService attackService = Setup.AttackService(new []{attackingCharacter, receivingCharacter}, 5);
            attackingCharacter.UpdateCoordinate(new Vector2Int(2, 0));
            receivingCharacter.UpdateCoordinate(new Vector2Int(0, 0));

            // Act.
            attackService.Attack(receivingCharacter);

            // Assert.
            receivingCharacter.CharacterLogic.Health.HealthPoints.Value.Should().NotBe(20);
        } 
        
        [Test]
        public void WhenAttackCharacterAttackAnotherCharacter_AndAnotherCharacterInHisTeam_ThenReceivingCharacterShouldNotTakeDamage()
        {
            // Arrange.
            ICharacter attackingCharacter = Setup.CharacterForAttack(21, 3, CharacterAttackType.Ranged, CharacterTeam.Ally);
            ICharacter receivingCharacter = Setup.CharacterForAttack(20, 1, CharacterAttackType.Ranged, CharacterTeam.Ally);

            IAttackService attackService = Setup.AttackService(new []{attackingCharacter, receivingCharacter}, 5);
            attackingCharacter.UpdateCoordinate(new Vector2Int(2, 0));
            receivingCharacter.UpdateCoordinate(new Vector2Int(0, 0));

            // Act.
            attackService.Attack(receivingCharacter);

            // Assert.
            receivingCharacter.CharacterLogic.Health.HealthPoints.Value.Should().Be(20);
        }
        
        [Test]
        public void WhenAttackCharacterAttackAnotherCharacter_AndAnotherCharacterIsSelf_ThenReceivingCharacterShouldNotTakeDamage()
        {
            // Arrange.
            ICharacter attackingCharacter = Setup.CharacterForAttack(21, 3, CharacterAttackType.Ranged, CharacterTeam.Ally);

            IAttackService attackService = Setup.AttackService(new []{attackingCharacter}, 5);
            attackingCharacter.UpdateCoordinate(new Vector2Int(2, 0));
    
            // Act.
            attackService.Attack(attackingCharacter);

            // Assert.
            attackingCharacter.CharacterLogic.Health.HealthPoints.Value.Should().Be(21);
        }
        
        [Test]
        public void WhenCharacterAttack_AndRangeNotEnough_ThenCharacterShouldNotDealDamage()
        {
            // Arrange.
            ICharacter attackingCharacter = Setup.CharacterForAttack(21, 3, CharacterAttackType.Melee, CharacterTeam.Ally);
            ICharacter receivingCharacter = Setup.CharacterForAttack(20, 1, CharacterAttackType.Melee, CharacterTeam.Enemy);

            IAttackService attackService = Setup.AttackService(new []{attackingCharacter, receivingCharacter}, 1);
            attackingCharacter.UpdateCoordinate(new Vector2Int(2, 0));
            receivingCharacter.UpdateCoordinate(new Vector2Int(0, 0));
    
            // Act.
            attackService.Attack(receivingCharacter);

            // Assert.
            receivingCharacter.CharacterLogic.Health.HealthPoints.Value.Should().Be(20);
        }
    }
}