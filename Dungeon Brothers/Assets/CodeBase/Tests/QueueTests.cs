using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using FluentAssertions;
using NUnit.Framework;

namespace CodeBase.Tests
{
    public class QueueTests
    {
        [Test]
        public void WhenAddingInQueue_AndOneInitiativeIsBigger_ThenBiggerInitiativeShouldBeLast()
        {
            // Arrange.
            Character characterWithLessInitiative = Create.Character(1, 1, 1, 1, 1, 2, false);
            Character characterWithBiggerInitiative = Create.Character(1, 1, 1, 1, 5, 2, false);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);

            // Act.
            charactersProvider.Add(characterWithLessInitiative, null);
            charactersProvider.Add(characterWithBiggerInitiative, null);

            // Assert.
            turnQueue.Characters.Last().Should().Be(characterWithBiggerInitiative);
        }
    }
}