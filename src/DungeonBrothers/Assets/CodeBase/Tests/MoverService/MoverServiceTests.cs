using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace CodeBase.Tests.MoverService
{
    public class MoverServiceTests
    {
        [Test]
        public void WhenCharacterMoving_AndHaveEnoughMovePoints_ThenCharacterShouldBeOnChosenTile()
        {
            // Arrange.
            ICharacter character = Substitute.For<ICharacter>();
            character
                .When(_ => _.UpdateCoordinate(Arg.Any<Vector2Int>()))
                .Do(_ => character.Coordinate.Returns(_.Arg<Vector2Int>()));
            
            character.MovementStats.Returns(new MovementStats(5, false));

            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);
            IMapService mapService = Setup.MapService(Create.TileMap(4, 4));
            IMoverService moverService = Setup.MoverService(mapService, turnQueue);
            
            turnQueue.Initialize();
            moverService.Enable();
            charactersProvider.Add(character, null);
            turnQueue.SetFirstTurn();
            
            // Act.
            if (mapService.TryGetTile(new Vector2Int(1, 1), out Tile tile)) 
                moverService.Move(tile);

            // Assert.
            character.Coordinate.Should().Be(tile.Logic.Coordinates);
        }
        
        [Test]
        public void WhenCharacterMoving_AndCanMoveThroughObstacles_ThenCharacterShouldBeOnChosenTileIgnoringObstacles()
        {
            // Arrange.
            ICharacter character = Substitute.For<ICharacter>();
            character.MovementStats.Returns(new MovementStats(5, true));
            
            character.When(_ => _.UpdateCoordinate(Arg.Any<Vector2Int>()))
                .Do(_ => character.Coordinate.Returns(_.Arg<Vector2Int>()));
            
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);
            IMapService mapService = Setup.MapService(Create.TileMap(4, 4));
            IMoverService moverService = Setup.MoverService(mapService, turnQueue);
            
            turnQueue.Initialize();
            moverService.Enable();
            charactersProvider.Add(character, null);
            turnQueue.SetFirstTurn();
            Setup.ObstaclesAroundZeroPosition(mapService, character);
            
            // Act.
            if (mapService.TryGetTile(new Vector2Int(2, 1), out Tile tile)) 
                moverService.Move(tile);

            // Assert.
            character.Coordinate.Should().Be(tile.Logic.Coordinates);
        }
    }
}