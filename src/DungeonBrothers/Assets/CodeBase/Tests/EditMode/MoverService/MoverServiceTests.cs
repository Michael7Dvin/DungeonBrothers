using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Tiles;
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
            Tile tile = Setup.Tile(new Vector2Int(2, 1));

                // Act.
            moverService.Move(tile);

            // Assert.
            character.Coordinate.Should().Be(tile.Logic.Coordinates);
        }

        [Test]
        public void WhenCharacterMoving_AndCanMoveThroughObstacles_ThenCharacterShouldBeOnChosenTileIgnoringObstacles()
        {
            // Arrange.
            ICharacter character = Setup.CharacterForMovement(5, true);

            IMapService mapService = Setup.MapService(4, 4);
            Setup.ObstaclesAroundZeroPosition(mapService);
            
            IMoverService moverService = Setup.MoverService(character, mapService);
            Tile tile = Setup.Tile(new Vector2Int(2, 2));
            
            // Act.
            moverService.Move(tile);

            // Assert.
            character.Coordinate.Should().Be(tile.Logic.Coordinates);
        }
    }
}