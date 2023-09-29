using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace CodeBase.Tests.Moving
{
    public class MoverServiceTests
    {
        [Test]
        public void WhenCharacterMoving_AndHeHaveEnoughMovablePoints_ThenCharacterShouldBeOnChosenTile()
        {
            // Arrange.
            Character character = Create.Character(1, 1, 1, 1, 1, 4, false);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);
            IMapService mapService = Setup.MapService(Create.TileMap(4, 4));
            IMoverService moverService = Setup.MoverService(mapService, turnQueue);
            
            turnQueue.Initialize();
            moverService.Enable();
            // Act.
            charactersProvider.Add(character, null);
            
            turnQueue.SetFirstTurn();

            if (mapService.TryGetTile(new Vector2Int(3, 1), out Tile tile))
                moverService.Move(tile);

                // Assert.
            character.Coordinate.Should().Be(tile.TileLogic.Coordinates);
        }
        
        [Test]
        public void WhenCharacterMoving_AndHeMoveThroughObstacles_ThenCharacterShouldBeOnChosenTileIgnoringObstacles()
        {
            // Arrange.
            Character character = Create.Character(1, 1, 1, 1, 1, 4, true);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);
            IMapService mapService = Setup.MapService(Create.TileMap(4, 4));
            IMoverService moverService = Setup.MoverService(mapService, turnQueue);
            
            turnQueue.Initialize();
            moverService.Enable();

            Setup.ObstaclesAroundZeroPosition(mapService, character);
            // Act.
            charactersProvider.Add(character, null);
            
            turnQueue.SetFirstTurn();
            
            if (mapService.TryGetTile(new Vector2Int(2, 1), out Tile tile)) 
                moverService.Move(tile);

            // Assert.
            character.Coordinate.Should().Be(tile.TileLogic.Coordinates);
        }

        [Test]
        public void WhenCharacterMoving_AndHeMoveOnOccupiedTile_ThenCharacterShouldBeOnHisInitialTile()
        {
            // Arrange.
            Character character = Create.Character(1, 1, 1, 1, 1, 4, false);
            CharactersProvider charactersProvider = Create.CharactersProvider();
            ITurnQueue turnQueue = Setup.TurnQueue(charactersProvider);
            IMapService mapService = Setup.MapService(Create.TileMap(4, 4));
            IMoverService moverService = Setup.MoverService(mapService, turnQueue);
            
            turnQueue.Initialize();
            moverService.Enable();
            
            if (mapService.TryGetTile(new Vector2Int(0,1), out Tile obstacleTileOnRight))
                obstacleTileOnRight.TileLogic.Occupy(character);
            // Act.
            Vector2Int initialPosition = character.Coordinate;
            
            charactersProvider.Add(character, null);
            
            turnQueue.SetFirstTurn();
            
            if (mapService.TryGetTile(new Vector2Int(0, 1), out Tile tile)) 
                moverService.Move(tile);

            // Assert.
            character.Coordinate.Should().Be(initialPosition);
        }
    }
}