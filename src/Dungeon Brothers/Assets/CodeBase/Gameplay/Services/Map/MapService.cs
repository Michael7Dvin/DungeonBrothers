using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Map
{
    public class MapService : IMapService
    {
        private const int MaxNeighborsCount = 8;
        
        private readonly Dictionary<Vector2Int, Tile> _tiles = new();

        private readonly Vector2Int[] _neighborsDirections = 
        {
            new (1, 0), 
            new (-1, 0), 
            new (0, 1), 
            new (0, -1),
        };

        public void ResetMap(IEnumerable<Tile> map)
        {
            _tiles.Clear();

            foreach (Tile tile in map)
            {
                _tiles[tile.Logic.Coordinates] = tile;
            }
                
        }
        
        public bool TryGetTile(Vector2Int coordinates, out Tile tile)
        {
            if (_tiles.ContainsKey(coordinates) == true)
            {
                tile = _tiles[coordinates];
                return true;
            }

            tile = null;
            return false;
        }
        
        public List<Tile> GetNeighbors(Vector2Int coordinates)
        {
            List<Tile> neighbors = new(MaxNeighborsCount);

            foreach (Vector2Int direction in _neighborsDirections)
            {
                Vector2Int neighborCoordinates = coordinates + direction;

                if (TryGetTile(neighborCoordinates, out Tile tile)) 
                    neighbors.Add(tile);
            }

            return neighbors;
        }
    }
}