using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace CodeBase.Tests.EditMode.MoverService
{
    public class MoverServiceTests
    {
        [Test]
        public void WhenCharacterMoving_AndHaveEnoughMovePoints_ThenCharacterShouldBeOnChosenTile()
        {
            // Arrange.
            ICharacter character = Setup.CharacterForMovement(5, false);

            IMoverService moverService = Setup.MoverService(character, 4, 4);
            Vector2Int destination = new(3, 0);
            
            // Act.
            moverService.Move(destination);

            // Assert.
            character.Coordinate.Should().Be(destination);
        }

        [Test]
        public void WhenCharacterMoving_AndCanMoveThroughObstacles_ThenCharacterShouldBeOnChosenTileIgnoringObstacles()
        {
            // Arrange.
            ICharacter character = Setup.CharacterForMovement(5, true);

            IMapService mapService = Setup.MapService(4, 4);
            IMoverService moverService = Setup.MoverService(character, mapService);
            Vector2Int destination = new(2, 2);
            
            Setup.ObstaclesAroundZeroPosition(mapService);
            
            // Act.
            moverService.Move(destination);

            // Assert.
            character.Coordinate.Should().Be(destination);
        }
    }
}