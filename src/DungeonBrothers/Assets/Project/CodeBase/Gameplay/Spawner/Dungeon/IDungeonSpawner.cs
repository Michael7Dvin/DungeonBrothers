using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Tiles;

namespace Project.CodeBase.Gameplay.Spawner.Dungeon
{
    public interface IDungeonSpawner
    {
        public UniTask<Dictionary<Direction, List<Room>>> SpawnDungeon();
        Room StartRoom { get; }
        public List<Tile> Tiles { get; }
    }
}