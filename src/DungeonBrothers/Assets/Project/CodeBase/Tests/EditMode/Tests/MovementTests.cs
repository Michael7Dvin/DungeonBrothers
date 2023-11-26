using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Project.CodeBase.Gameplay.Characters.Logic.Movement;
using Project.CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace Project.CodeBase.Tests.EditMode.Tests
{
    public class MovementTests
    {
        [Test]
        public void WhenCheckingCanMove_AndPathLengthLessThanAvailableMovePoints_ThenReturnTrue()
        {
            // Arrange.
            Vector2Int pathStart = new Vector2Int(0, 0);
            Vector2Int pathEnd = new Vector2Int(5, 0);
            List<Tile> path = Create.TilePath(pathStart, pathEnd);

            const int movePoints = 10;
            IMovement movement = Create.Movement(path.First(), false, movePoints);

            // Act.
            bool canMove = movement.CanMove(path);

            // Assert.
            canMove.Should().Be(true);
        }
        
        [Test]
        public void WhenCheckingCanMove_AndPathLengthEqualsAvailableMovePoints_ThenReturnTrue()
        {
            // Arrange.
            Vector2Int pathStart = new Vector2Int(0, 0);
            Vector2Int pathEnd = new Vector2Int(5, 0);
            List<Tile> path = Create.TilePath(pathStart, pathEnd);

            const int movePoints = 5;
            IMovement movement = Create.Movement(path.First(), false, movePoints);

            // Act.
            bool canMove = movement.CanMove(path);

            // Assert.
            canMove.Should().Be(true);
        }

        [Test]
        public void WhenCheckingCanMove_AndPathLengthMoreThanAvailableMovePoints_ThenReturnFalse()
        {
            // Arrange.
            Vector2Int pathStart = new Vector2Int(0, 0);
            Vector2Int pathEnd = new Vector2Int(5, 0);
            List<Tile> path = Create.TilePath(pathStart, pathEnd);

            const int movePoints = 3;
            IMovement movement = Create.Movement(path.First(), false, movePoints);

            // Act.
            bool canMove = movement.CanMove(path);
            
            // Assert.
            canMove.Should().Be(false);
        }
        
        [Test]
        public void WhenCheckingCanMove_AndStartTileEqualsDestinationTile_ThenReturnFalse()
        {
            // Arrange.
            Tile tile = Create.Tile(Vector2Int.zero);
            List<Tile> path = new() { tile };

            const int movePoints = 3;
            IMovement movement = Create.Movement(path.First(), false, movePoints);

            // Act.
            bool canMove = movement.CanMove(path);
            
            // Assert.
            canMove.Should().Be(false);
        }

        [Test]
        public void WhenMoving_ThenAvailableMovePointsDecreaseByPathLength()
        {
            // Arrange.
            Vector2Int pathStart = new Vector2Int(0, 0);
            Vector2Int pathEnd = new Vector2Int(3, 0);
            List<Tile> path = Create.TilePath(pathStart, pathEnd);

            const int movePoints = 5;
            IMovement movement = Create.Movement(path.First(), false, movePoints);

            // Act.
            movement.Move(path);

            // Assert.
            movement.AvailableMovePoints.Should().Be(movePoints - path.Count);
        }
    }
}