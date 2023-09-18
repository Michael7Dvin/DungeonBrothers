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
            ICharacter character1 = Create.Character(1, 1 ,1 ,1 ,1);
            ICharacter character2 = Create.Character(1, 1, 1, 1, 5);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);

            // Act.
            charactersProvider.Add(character1);
            charactersProvider.Add(character2);

            // Assert.
            turnQueue.Units.Last().Should().Be(character2);
        }
        
        [Test]
        public void WhenAddingInQueue_AndInitiativeEqualsAndOneLevelIsBigger_ThenBiggerLevelShouldBeLast()
        {
            // Arrange.
            ICharacter character1 = Create.Character(1, 1 ,1 ,1 ,1);
            ICharacter character2 = Create.Character(4, 1, 1, 1, 1);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);

            // Act.
            charactersProvider.Add(character1);
            charactersProvider.Add(character2);

            // Assert.
            turnQueue.Units.Last().Should().Be(character2);
        }
        
        [Test]
        public void WhenAddingInQueue_AndInitiativeAndLevelEqualsAndOneTotalStatsIsBigger_ThenBiggerTotalStatsShouldBeLast()
        {
            // Arrange.
            ICharacter character1 = Create.Character(4, 1 ,1 ,1 ,1);
            ICharacter character2 = Create.Character(4, 1, 5, 1, 1);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);

            // Act.
            charactersProvider.Add(character1);
            charactersProvider.Add(character2);

            // Assert.
            turnQueue.Units.Last().Should().Be(character2);
        }
        
    }
}