using System.Collections.Generic;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Spawner.Rooms
{
    [CreateAssetMenu(menuName = "StaticData/Configs/AllRooms", fileName = "AllRoomsConfig")]
    public class AllRoomsConfig : ScriptableObject
    {
        public List<RoomConfig> RightExit;
        public List<RoomConfig> LeftExit;
        public List<RoomConfig> TopExit;
        public List<RoomConfig> DownExit;
    }
}