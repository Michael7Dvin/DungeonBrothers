using System.Collections.Generic;
using Project.CodeBase.Gameplay.Rooms;

namespace Project.CodeBase.Gameplay.Spawner.Dungeon
{
    public interface IDungeonSpawner
    {
        public Dictionary<Direction, Dictionary<Room, RoomInfo>> SpawnDungeon(out Room startRoom);
    }
}