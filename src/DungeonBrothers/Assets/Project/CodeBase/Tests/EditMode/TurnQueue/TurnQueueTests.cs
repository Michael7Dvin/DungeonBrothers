using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Services.TurnQueue;

namespace Project.CodeBase.Tests.EditMode.TurnQueue
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