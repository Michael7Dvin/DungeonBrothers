using System.Collections.Generic;
using Project.CodeBase.Gameplay.Rooms;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Spawner.Rooms
{
    [CreateAssetMenu(menuName = "StaticData/Configs/AllRooms", fileName = "AllRoomsConfig")]
    public class AllRoomsConfig : SerializedScriptableObject
    {
        public Dictionary<Direction, List<RoomConfig>> Rooms;
        public RoomConfig StartRoom;
    }
}