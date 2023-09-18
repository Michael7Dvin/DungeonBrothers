using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.UnitsProvider;
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
            ICharacter characterWithLessInitiative = Create.Character(1, 1, 1, 1, 1);
            ICharacter characterWithBiggerInitiative = Create.Character(1, 1, 1, 1, 5);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);

            // Act.
            charactersProvider.Add(characterWithLessInitiative, null);
            charactersProvider.Add(characterWithBiggerInitiative, null);

            // Assert.
            turnQueue.Characters.Last().Should().Be(characterWithBiggerInitiative);
        }
        
        [Test]
        public void WhenAddingInQueue_AndInitiativeEqualsAndOneLevelIsBigger_ThenBiggerLevelShouldBeLast()
        {
            // Arrange.
            ICharacter characterWithLessLevel = Create.Character(1, 1, 1, 1, 1);
            ICharacter characterWithBiggerLevel = Create.Character(4, 1, 1, 1, 1);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);

            // Act.
            charactersProvider.Add(characterWithLessLevel, null);
            charactersProvider.Add(characterWithBiggerLevel, null);

            // Assert.
            turnQueue.Characters.Last().Should().Be(characterWithBiggerLevel);
        }
        
        [Test]
        public void WhenAddingInQueue_AndInitiativeAndLevelEqualsAndOneTotalStatsIsBigger_ThenBiggerTotalStatsShouldBeLast()
        {
            // Arrange.
            ICharacter characterWithLessTotalStats = Create.Character(4, 1,1,1,1);
            ICharacter characterWithBiggerTotalStats = Create.Character(4, 1, 5, 1, 1);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);

            // Act.
            charactersProvider.Add(characterWithLessTotalStats, null);
            charactersProvider.Add(characterWithBiggerTotalStats, null);

            // Assert.
            turnQueue.Characters.Last().Should().Be(characterWithBiggerTotalStats);
        }
        
    }
}