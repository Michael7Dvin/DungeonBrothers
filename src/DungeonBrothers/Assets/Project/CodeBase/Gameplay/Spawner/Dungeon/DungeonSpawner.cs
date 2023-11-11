using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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
        
        private readonly Dictionary<Direction, List<Room>> _branches = new();

        private int _lenghtDungeon;
        private int _maxLenghtBranch;
        
        private int _maxRooms;
        private int _maxRoomsInBranch;

        private Room _currentRoom;
        public Room StartRoom { get; private set; }

        public DungeonSpawner(IRandomService randomService,
            IRoomSpawner roomSpawner)
        {
            _randomService = randomService;
            _roomSpawner = roomSpawner;
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
            
            _lenghtDungeon = _randomService.DoRandomInRange(1, _maxRooms + 1);
            _maxLenghtBranch = _randomService.DoRandomInRange(1, _maxRoomsInBranch + 1);

            while (_lenghtDungeon <= 0)
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
            }

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

            if (IsBranchFirst(direction))
            {
                _currentRoom = await _roomSpawner.CreateRoom(direction);
                return true;
            }

            FindNewExitInNewRoom(direction, out Direction newDirection);
            
            _currentRoom = await _roomSpawner.CreateRoom(newDirection);
            _lenghtDungeon--;
            
            return true;
        }

        private bool IsBranchFirst(Direction direction) => 
            _branches[direction].Count == 0;

        private bool IsBranchComplete(Direction direction) => 
            _branches[direction].Count >= _maxRoomsInBranch;

        private void FindNewExitInNewRoom(Direction currentDirection, out Direction newDirection)
        {
            Dictionary<Direction, Door> doors = _branches[currentDirection].Last().Doors;

            foreach (var direction in doors.Keys)
            {
                if (doors[direction].IsReturnExit == false)
                {
                    switch (direction)
                    {
                        case Direction.Top:
                            newDirection = Direction.Down;
                            break;
                        case Direction.Down:
                            newDirection = Direction.Top;
                            break;
                        case Direction.Right:
                            newDirection = Direction.Left;
                            break;
                        case Direction.Left:
                            newDirection = Direction.Right;
                            break;
                    }
                }
            }

            newDirection = currentDirection;
        }
    }
}