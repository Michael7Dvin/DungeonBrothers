using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Tiles;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace CodeBase.Tests.MapService
{
    public class MapServiceTests
    {
        [Test]
        public void WhenGettingTileNeighbors_AndThere3x3TileMap_ThenCentralTileNeighborsCountShouldBe4()
        {
            // Arrange.
            List<Tile> tiles = Create.TileMap(3, 3);
            IMapService mapService = Setup.MapService(tiles);
            Vector2Int centralTileCoordinates = Vector2Int.one;

            // Act.
            List<Tile> neighbors = mapService.GetNeighbors(centralTileCoordinates);

            // Assert.
            neighbors.Count.Should().Be(4);
        }
        
        [Test]
        public void WhenGettingTileNeighbors_AndThere2x2TileMap_ThenAnyTileNeighborsCountShouldBe2()
        {
            // Arrange.
            List<Tile> tiles = Create.TileMap(2, 2);
            IMapService mapService = Setup.MapService(tiles);

            Dictionary<Tile, int> tilesNeighborsCounts = new();
            
            // Act.
            foreach (Tile tile in tiles)
            {
                List<Tile> neighbors = mapService.GetNeighbors(tile.Logic.Coordinates);
                tilesNeighborsCounts.Add(tile, neighbors.Count);
            }

            // Assert.
            tilesNeighborsCounts.All(pair => pair.Value == 2).Should().Be(true);
        }

        [Test]
        public void WhenTryingToGetTile_AndThereNoTileAtPassedCoordinates_ThenShouldReturnFalse()
        {
            // Arrange.
            IMapService mapService = Create.MapService();

            // Act.
            bool isSuccessful = mapService.TryGetTile(Vector2Int.one, out _);

            // Assert.
            isSuccessful.Should().Be(false);
        }

        [Test]
        public void WhenTryingToGetTile_AndThereNoTileAtPassedCoordinates_ThenResultShouldBeNull()
        {
            // Arrange.
            IMapService mapService = Create.MapService();
            
            // Act.
            mapService.TryGetTile(Vector2Int.left, out Tile tile);

            // Assert.
            tile.Should().Be(null);
        }
    }
}