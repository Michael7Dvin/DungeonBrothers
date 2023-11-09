using System;
using System.Collections.Generic;
using System.Linq;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Spawner.Dungeon;
using Project.CodeBase.Infrastructure.Services.Logger;
using UniRx;

namespace Project.CodeBase.Gameplay.Services.Dungeon
{
    public class DungeonService : IDungeonService
    {
        private readonly IDungeonSpawner _dungeonSpawner;
        private readonly ICustomLogger _logger;
        
        private readonly CompositeDisposable _startRoomDisposable = new();
        private readonly CompositeDisposable _currentRoomDisposable = new();
        
        private Dictionary<Direction, Dictionary<Room, RoomInfo>> _branches;

        private Room _currentRoom;

        private Room _startRoom;

        private Room _previousRoom;

        public DungeonService(IDungeonSpawner dungeonSpawner, 
            ICustomLogger logger)
        {
            _dungeonSpawner = dungeonSpawner;
            _logger = logger;
        }

        public void CreateDungeon()
        {
            _branches = _dungeonSpawner.SpawnDungeon(out Room startRoom);

            _startRoom = startRoom;
            _currentRoom = _startRoom;
            
            SubscribeStartRoom();
        }

        private List<Room> FindCurrentBranch()
        {
            foreach (var branch in _branches.Values)
            {
                if (branch.Keys.Contains(_currentRoom))
                    return branch.Keys.ToList();
            }

            return null;
        }

        private bool TryFindNextRoom(out Room room)
        {
            List<Room> currentBranch = FindCurrentBranch();
            int index = currentBranch.IndexOf(_currentRoom);
            room = null;

            if (index == currentBranch.Count - 1)
                return false;

            room = currentBranch[index + 1];
            return true;
        }
        private bool TryFindPreviousRoom(out Room room)
        {
            List<Room> currentBranch = FindCurrentBranch();
            int index = currentBranch.IndexOf(_currentRoom);
            room = null;

            if (index == 0)
            {
                room = _startRoom;
                return false;
            }

            room = currentBranch[index - 1];
            return true;
        }

        private void SubscribeStartRoom()
        {
            foreach (var room in _startRoom.Doors.Keys)
            {
                _startRoom.Doors[room].Entered
                    .Subscribe(_ => GoToFirstRoomInBranch(room))
                    .AddTo(_startRoomDisposable);
            }
        }

        private void TryFindExitInNextRoom()
        {
            foreach (var direction in _previousRoom.Doors.Keys)
            {
                if (_previousRoom.Doors[direction].IsReturnExit == false)
                {
                    switch (direction)
                    {
                        case Direction.Top:
                            SubscribeCurrentRoomExits(Direction.Down);
                            break;
                        case Direction.Down:
                            SubscribeCurrentRoomExits(Direction.Top);
                            break;
                        case Direction.Right:
                            SubscribeCurrentRoomExits(Direction.Left);
                            break;
                        case Direction.Left:
                            SubscribeCurrentRoomExits(Direction.Top);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private void SubscribeCurrentRoomExits(Direction exitDirection)
        {
            _currentRoom.Doors[exitDirection].Entered
                .Subscribe(_ => GoToPreviousRoomInBranch())
                .AddTo(_currentRoomDisposable);

            foreach (var direction in _currentRoom.Doors.Keys)
            {
                if (direction == exitDirection)
                    return;

                _currentRoom.Doors[direction].Entered
                    .Subscribe(_ => GoToNextRoomInBranch())
                    .AddTo(_currentRoomDisposable);
            }
        }

        private void GoToFirstRoomInBranch(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top:
                {
                    EnterInRoom(_branches[Direction.Top].Keys.First());
                    SubscribeCurrentRoomExits(Direction.Down);
                }
                    break;
                case Direction.Down:
                {
                    EnterInRoom(_branches[Direction.Down].Keys.First());
                    SubscribeCurrentRoomExits(Direction.Top);
                }
                    break;
                case Direction.Right:
                {
                    EnterInRoom(_branches[Direction.Right].Keys.First());
                    SubscribeCurrentRoomExits(Direction.Left);
                }
                    break;
                case Direction.Left:
                {
                    EnterInRoom(_branches[Direction.Left].Keys.First());
                    SubscribeCurrentRoomExits(Direction.Right);
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void EnterInRoom(Room room)
        {
            _currentRoom.DisableRoom();

            _previousRoom = _currentRoom;
            _currentRoom = room;

            if (_previousRoom != _startRoom)
            {
                TryFindExitInNextRoom();
                _currentRoomDisposable.Clear();;
            }

            room.EnableRoom();
        }
        
        private void GoToNextRoomInBranch()
        {
            if (TryFindNextRoom(out Room room)) 
                EnterInRoom(room);
        }

        private void GoToPreviousRoomInBranch()
        {
            if (TryFindPreviousRoom(out Room room)) 
                EnterInRoom(room);
        }
    }
}