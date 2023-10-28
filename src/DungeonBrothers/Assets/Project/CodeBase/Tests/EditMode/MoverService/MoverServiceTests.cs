using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Services.Map;
using _Project.CodeBase.Gameplay.Services.Move;
using _Project.CodeBase.Gameplay.Tiles;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace _Project.CodeBase.Tests.EditMode.MoverService
{
    public class MoverServiceTests
    {
        [Test]
        public void WhenCharacterMoving_AndHaveEnoughMovePoints_ThenCharacterShouldBeOnChosenTile()
        {
            // Arrange.
            ICharacter character = Setup.CharacterForMovement(5, false, 1, 3);

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
            ICharacter character = Setup.CharacterForMovement(5, true, 1, 3);

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