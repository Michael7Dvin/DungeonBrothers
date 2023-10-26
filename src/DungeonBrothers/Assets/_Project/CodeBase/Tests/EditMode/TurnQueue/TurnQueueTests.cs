using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using FluentAssertions;
using NUnit.Framework;

namespace CodeBase.Tests.EditMode.TurnQueue
{
    public class TurnQueueTests
    {
        [Test]
        public void WhenAddingInQueue_AndOneInitiativeIsBigger_ThenBiggerInitiativeShouldBeLast()
        {
            // Arrange.
            ICharacter characterWithLessInitiative = Setup.CharacterForTurnQueue(1, 1);
            ICharacter characterWithBiggerInitiative = Setup.CharacterForTurnQueue(1, 5);
            
            ITurnQueue turnQueue = Setup.TurnQueue(characterWithLessInitiative, characterWithBiggerInitiative);

            // Act.
            turnQueue.SetFirstTurn();

            // Assert.
            turnQueue.Characters.Last().Should().Be(characterWithBiggerInitiative);
        }
    }
}