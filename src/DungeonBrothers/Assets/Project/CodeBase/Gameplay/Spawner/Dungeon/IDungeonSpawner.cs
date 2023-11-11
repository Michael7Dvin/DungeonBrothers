using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Rooms;

namespace Project.CodeBase.Gameplay.Spawner.Dungeon
{
    public interface IDungeonSpawner
    {
        public UniTask<Dictionary<Direction, List<Room>>> SpawnDungeon();
        Room StartRoom { get; }
    }
}