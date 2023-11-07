using System.Collections.Generic;
using System.Linq;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Services.Random;
using Project.CodeBase.Gameplay.Spawner.Rooms;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;

namespace Project.CodeBase.Gameplay.Spawner.Dungeon
{
    public class DungeonSpawner : IDungeonSpawner
    {
        private readonly IRandomService _randomService;
        private readonly IRoomSpawner _roomSpawner;
        private readonly Dictionary<Room, RoomInfo> _leftBranchRooms = new();
        private readonly Dictionary<Room, RoomInfo> _rightBranchRooms = new();
        private readonly Dictionary<Room, RoomInfo> _downBranchRooms = new();
        private readonly Dictionary<Room, RoomInfo> _topBranchRooms = new();

        private int _lenghtDungeon;
        private int _maxLenghtBranch;
        
        private int _maxRooms;
        private int _maxRoomsInBranch;

        public DungeonSpawner(IRandomService randomService,
            IStaticDataProvider staticDataProvider)
        {
            _randomService = randomService;
        }

        public Room SpawnDungeon(out Dictionary<Room, RoomInfo> leftBranchRooms, 
            out Dictionary<Room, RoomInfo> rightBranchRooms,
            out Dictionary<Room, RoomInfo> downBranchRooms,
            out Dictionary<Room, RoomInfo> topBranchRooms)
        {
            
            _lenghtDungeon = _randomService.DoRandomInRange(1, _maxRooms + 1);
            _maxLenghtBranch = _randomService.DoRandomInRange(1, _maxRoomsInBranch + 1);

            while (_lenghtDungeon <= 0)
            {
                if (TrySpawnInLeftBranch())
                {
                    _lenghtDungeon--;
                    continue;
                }
                
                
            }

            leftBranchRooms = _leftBranchRooms;
            rightBranchRooms = _rightBranchRooms;
            downBranchRooms = _downBranchRooms;
            topBranchRooms = _topBranchRooms;
            return CreateStartRoom();
        }

        private Room CreateStartRoom() => 
            _roomSpawner.CreateStartRoom();

        private bool TrySpawnInLeftBranch()
        {
            if (_leftBranchRooms.Count != _maxLenghtBranch)
                return false;

            if (_leftBranchRooms.Last().Value.IsHaveAnyExit() == false)
                return false;

            _roomSpawner.CreateWithLeftExit();
            return true;
        }
    }
}