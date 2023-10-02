using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Gameplay.PathFinder
{
    public struct PathFindingResults
    {
        private readonly IReadOnlyDictionary<Vector2Int, Tile> _obstacles;
        private readonly Dictionary<Vector2Int, Vector2Int?> _paths;

        public PathFindingResults(Dictionary<Vector2Int, Vector2Int?> paths,
            IReadOnlyDictionary<Vector2Int, Tile> obstacles)
        {
            _paths = paths;
            _obstacles = obstacles;
        }

        public IEnumerable<Vector2Int> WalkableCoordinates => 
            _paths
                .Keys
                .Except(_obstacles.Keys)
                .Skip(1);

        public bool IsMovableAt(Vector2Int coordinates) =>
            WalkableCoordinates.Contains(coordinates);
        
        public List<Vector2Int> GetPathTo(Vector2Int coordinates)
        {
            if (IsMovableAt(coordinates) == false)
                return null;

            Vector2Int current = coordinates;
            List<Vector2Int> path = new List<Vector2Int> { current };

            while (_paths[current] != null)
            {
                path.Add(_paths[current].Value);
                current = _paths[current].Value;
            }
            
            path.Reverse();
            path.RemoveAt(0);
            return path;
        }
    }
}