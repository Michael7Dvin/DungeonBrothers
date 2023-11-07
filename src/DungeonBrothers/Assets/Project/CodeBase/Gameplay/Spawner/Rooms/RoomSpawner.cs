using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Services.Random;

namespace Project.CodeBase.Gameplay.Spawner.Rooms
{
    public class RoomSpawner : IRoomSpawner
    {
        private IRandomService _randomService;
        private AllRoomsConfig _roomsConfig;
        private AllCharactersConfigs _allCharactersConfigs;

        public Room CreateWithLeftExit()
        {
            int randomRoom = _randomService.DoRandomInRange(0, _roomsConfig.LeftExit.Count);

            RoomConfig roomConfig = _roomsConfig.LeftExit[randomRoom];
            return null;
        }

        public Room CreateStartRoom()
        {
            return null;
        }
        
        public Room CreateWithRightExit()
        {
            return null;
        }

        public Room CreateWithUpExit()
        {
            return null;
        }

        public Room CreateWithDownExit()
        {
            return null;
        }
    }
}