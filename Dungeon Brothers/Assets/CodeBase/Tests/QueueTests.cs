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
            Character characterWithLessInitiative = Create.Character(1, 1, 1, 1, 1);
            Character characterWithBiggerInitiative = Create.Character(1, 1, 1, 1, 5);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);

            // Act.
            charactersProvider.Add(characterWithLessInitiative, null);
            charactersProvider.Add(characterWithBiggerInitiative, null);

            // Assert.
            turnQueue.Characters.Last().Should().Be(characterWithBiggerInitiative);
        }
        
        [Test]
        public void WhenAddingInQueue_AndInitiativesEquals_ThenOneWillBeRandom()
        {
            // Arrange.
            Character characterWithSameStats1 = Create.Character(1, 1, 1, 1, 1);
            Character characterWithSameStats2 = Create.Character(1, 1, 1, 1, 1);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);

            // Act.
            charactersProvider.Add(characterWithSameStats1, null);
            charactersProvider.Add(characterWithSameStats2, null);

            // Assert.
            turnQueue.Characters.Last().Should().Be(characterWithSameStats2);
        }
    }
}