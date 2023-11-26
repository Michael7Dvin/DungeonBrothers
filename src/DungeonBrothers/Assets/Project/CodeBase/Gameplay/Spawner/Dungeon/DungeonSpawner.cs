using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Spawner.Rooms;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.Logger;
using Project.CodeBase.Infrastructure.Services.Providers.LevelSpawner;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;

namespace Project.CodeBase.Gameplay.Spawner.Dungeon
{
    public class DungeonSpawner : IDungeonSpawner
    {
        private readonly IRoomSpawner _roomSpawner;
        private readonly ILevelSpawner _levelSpawner;

        private readonly Dictionary<Direction, List<Room>> _branches = new();

        private readonly int _maxRooms;
        private readonly int _maxRoomsInBranch;
        
        private int _currentMaxLenghtDungeon;
        private int _currentMaxRoomsInBranch;

        private Room _currentRoom;
        
        public List<Tile> Tiles { get; private set; }
        public Room StartRoom { get; private set; }

        public DungeonSpawner(IRoomSpawner roomSpawner,
            ILevelSpawner levelSpawner,
            IStaticDataProvider staticDataProvider)
        {
            _roomSpawner = roomSpawner;
            _levelSpawner = levelSpawner;

            _maxRooms = staticDataProvider.DungeonConfig.MaxRoomsInDungeon;
            _maxRoomsInBranch = staticDataProvider.DungeonConfig.MaxRoomsInBranch;
        }

        private void InitializeBranches()
        {
            List<Room> leftBranchRooms = new();
            List<Room> rightBranchRooms = new();
            List<Room> downBranchRooms = new();
            List<Room> topBranchRooms = new();
            
            _branches.Add(Direction.Left, leftBranchRooms);
            _branches.Add(Direction.Right, rightBranchRooms);
            _branches.Add(Direction.Down, downBranchRooms);
            _branches.Add(Direction.Top, topBranchRooms);
        }

        public async UniTask<Dictionary<Direction, List<Room>>> SpawnDungeon()
        {
            InitializeBranches();

            _currentMaxLenghtDungeon = _maxRooms;
            _currentMaxRoomsInBranch = _maxRoomsInBranch;

            while (_currentMaxLenghtDungeon > 0)
            {
                if (await TrySpawnInBranch(Direction.Left))
                {
                    _branches[Direction.Left].Add(_currentRoom);
                    continue;
                }

                if (await TrySpawnInBranch(Direction.Right))
                {
                   _branches[Direction.Right].Add(_currentRoom);
                    continue;
                }

                if (await TrySpawnInBranch(Direction.Down))
                {
                   _branches[Direction.Down].Add(_currentRoom);
                    continue;
                }

                if (await TrySpawnInBranch(Direction.Top)) 
                    _branches[Direction.Top].Add(_currentRoom);
                
                _currentMaxLenghtDungeon--;
            }
            
            await _levelSpawner.Spawn();

            StartRoom = await CreateStartRoom();

            return _branches;
        }

        private async UniTask<Room> CreateStartRoom() => 
            await _roomSpawner.CreateStartRoom();

        private async UniTask<bool> TrySpawnInBranch(Direction direction)
        {
            if (IsBranchComplete(direction))
            {
                _currentRoom = null;
                return false;
            }

            _currentRoom = await _roomSpawner.CreateRoom(direction);
            
            _currentMaxLenghtDungeon--;
            
            return true;
        }

        private bool IsBranchComplete(Direction direction) => 
            _branches[direction].Count >= _currentMaxRoomsInBranch;
    }
}