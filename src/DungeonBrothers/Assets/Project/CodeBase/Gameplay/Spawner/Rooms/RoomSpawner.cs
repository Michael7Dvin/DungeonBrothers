using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Services.Random;
using Project.CodeBase.Infrastructure.Services.Factories.Characters;
using Project.CodeBase.Infrastructure.Services.Logger;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;

namespace Project.CodeBase.Gameplay.Spawner.Rooms
{
    public class RoomSpawner : IRoomSpawner
    {
        private readonly IRoomFactory _roomFactory;
        private readonly IRandomService _randomService;
        private readonly ICharacterFactory _characterFactory;
        
        private readonly AllRoomsConfig _roomsConfig;
        private readonly AllCharactersConfigs _allCharactersConfigs;

        private Room _currentRoom;

        public RoomSpawner(IRandomService randomService, 
            IRoomFactory roomFactory,
            ICharacterFactory characterFactory,
            IStaticDataProvider staticDataProvider)
        {
            _randomService = randomService;
            _roomFactory = roomFactory;
            _characterFactory = characterFactory;
            
            _roomsConfig = staticDataProvider.AllRoomsConfig;
            _allCharactersConfigs = staticDataProvider.AllCharactersConfigs;
        }

        public async UniTask<Room> CreateRoom(Direction direction)
        {
            var roomConfig = GetRandomConfig(_roomsConfig.Rooms[direction]);

            Room room = await _roomFactory.Create(roomConfig);
            room.DisableRoom();

            return room;
        }

        public async UniTask<Room> CreateStartRoom()
        {
            Room room = await _roomFactory.Create(_roomsConfig.StartRoom);
            return room;
        }

        private RoomConfig GetRandomConfig(List<RoomConfig> roomConfigs)
        {
            int randomRoom = _randomService.DoRandomInRange(0, roomConfigs.Count);
            RoomConfig roomConfig = roomConfigs[randomRoom];
            return roomConfig;
        }
    }
}