using System.Collections.Generic;
using Project.CodeBase.Gameplay.Rooms;

namespace Project.CodeBase.Gameplay.Spawner.Dungeon
{
    public interface IDungeonSpawner
    {
        public void SpawnDungeon(out Dictionary<Room, RoomInfo> leftBranchRooms,
            out Dictionary<Room, RoomInfo> rightBranchRooms,
            out Dictionary<Room, RoomInfo> downBranchRooms,
            out Dictionary<Room, RoomInfo> topBranchRooms);
    }
}