using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Services.Random;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;

namespace Project.CodeBase.Gameplay.Spawner.Rooms
{
    public class RoomSpawner : IRoomSpawner
    {
        private readonly IRoomFactory _roomFactory;
        private readonly IRandomService _randomService;
        
        private readonly AllRoomsConfig _roomsConfig;
        private readonly AllCharactersConfigs _allCharactersConfigs;

        public RoomSpawner(IRandomService randomService, 
            IRoomFactory roomFactory,
            IStaticDataProvider staticDataProvider)
        {
            _randomService = randomService;
            _roomsConfig = staticDataProvider.AllRoomsConfig;
            _allCharactersConfigs = staticDataProvider.AllCharactersConfigs;
            _roomFactory = roomFactory;
        }

        public async UniTask<Room> CreateRoom(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top:
                    await CreateWithTopExit();
                    break;
                case Direction.Down:
                    await CreateWithDownExit();
                    break;
                case Direction.Right:
                    await CreateWithRightExit();
                    break;
                case Direction.Left:
                    await CreateWithLeftExit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return null;
        }

        public async UniTask<Room> CreateStartRoom()
        {
            Room room = await _roomFactory.Create(_roomsConfig.StartRoom);
            return room;
        }

        private async UniTask<Room> CreateWithLeftExit()
        {
            var roomConfig = GetRandomConfig(_roomsConfig.Rooms[Direction.Left]);

            Room room = await _roomFactory.Create(roomConfig);
            room.RoomInfo.IsHaveLeftExit = true;
            
            return null;
        }

        private async UniTask<Room> CreateWithRightExit()
        {
            var roomConfig = GetRandomConfig(_roomsConfig.Rooms[Direction.Right]);

            Room room = await _roomFactory.Create(roomConfig);
            room.RoomInfo.IsHaveRightExit = true;
            
            return null;
        }

        private async UniTask<Room> CreateWithTopExit()
        {
            var roomConfig = GetRandomConfig(_roomsConfig.Rooms[Direction.Top]);

            Room room = await _roomFactory.Create(roomConfig);
            room.RoomInfo.IsHaveTopExit = true;
            
            return null;
        }

        private async UniTask<Room> CreateWithDownExit()
        {
            var roomConfig = GetRandomConfig(_roomsConfig.Rooms[Direction.Down]);

            Room room = await _roomFactory.Create(roomConfig);
            room.RoomInfo.IsHaveDownExit = true;
            
            return null;
        }

        private RoomConfig GetRandomConfig(List<RoomConfig> roomConfigs)
        {
            int randomRoom = _randomService.DoRandomInRange(0, roomConfigs.Count);
            RoomConfig roomConfig = roomConfigs[randomRoom];
            return roomConfig;
        }
    }
}