using System.Collections.Generic;
using System.Linq;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Services.Random;
using Project.CodeBase.Gameplay.Spawner.Rooms;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;

namespace Project.CodeBase.Gameplay.Spawner.Dungeon
{
    public class DungeonSpawner : IDungeonSpawner
    {
        private readonly IRandomService _randomService;
        private readonly IRoomSpawner _roomSpawner;
        
        private Dictionary<Direction, Dictionary<Room, RoomInfo>> _branches;

        private int _lenghtDungeon;
        private int _maxLenghtBranch;
        
        private int _maxRooms;
        private int _maxRoomsInBranch;

        public DungeonSpawner(IRandomService randomService,
            IStaticDataProvider staticDataProvider)
        {
            _randomService = randomService;
        }

        private void InitializeBranches()
        {
            Dictionary<Room, RoomInfo> leftBranchRooms = new();
            Dictionary<Room, RoomInfo> rightBranchRooms = new();
            Dictionary<Room, RoomInfo> downBranchRooms = new();
            Dictionary<Room, RoomInfo> topBranchRooms = new();
            
            _branches.Add(Direction.Left, leftBranchRooms);
            _branches.Add(Direction.Right, rightBranchRooms);
            _branches.Add(Direction.Down, downBranchRooms);
            _branches.Add(Direction.Top, topBranchRooms);
        }

        public Dictionary<Direction, Dictionary<Room, RoomInfo>> SpawnDungeon(out Room startRoom)
        {
            InitializeBranches();
            
            _lenghtDungeon = _randomService.DoRandomInRange(1, _maxRooms + 1);
            _maxLenghtBranch = _randomService.DoRandomInRange(1, _maxRoomsInBranch + 1);

            while (_lenghtDungeon <= 0)
            {
                if (TrySpawnInRandomBranch())
                {
                    _lenghtDungeon--;
                    continue;
                }
                
                
            }

            startRoom = CreateStartRoom();

            return _branches;
        }

        private Room CreateStartRoom() => 
            _roomSpawner.CreateStartRoom();

        private bool TrySpawnInRandomBranch()
        {
            

            _roomSpawner.CreateWithLeftExit();
            return true;
        }
    }
}