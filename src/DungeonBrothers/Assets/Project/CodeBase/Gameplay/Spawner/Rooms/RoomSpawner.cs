using System;
using Project.CodeBase.Gameplay.Rooms;

namespace Project.CodeBase.Gameplay.Spawner.Rooms
{
    public class RoomSpawner : IRoomSpawner
    {


        public Room CreateRoom(RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.Start:
                    break;
                case RoomType.Boss:
                    break;
                case RoomType.Common:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roomType), roomType, null);
            }
        }

        private Room Create()
        {
            
        }
    }
}