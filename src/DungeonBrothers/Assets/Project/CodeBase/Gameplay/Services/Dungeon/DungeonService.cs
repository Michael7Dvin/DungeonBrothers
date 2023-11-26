using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Spawner.Dungeon;
using Project.CodeBase.Infrastructure.Services.Logger;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Services.Dungeon
{
    public class DungeonService : IDungeonService
    {
        private readonly IDungeonSpawner _dungeonSpawner;
        private readonly IMapService _mapService;
        private readonly ICustomLogger _logger;
        
        private readonly CompositeDisposable _startRoomDisposable = new();
        private readonly CompositeDisposable _currentRoomDisposable = new();
        
        private Dictionary<Direction, List<Room>> _branches;

        private Room _currentRoom;
        private Room _startRoom;
        private Room _previousRoom;

        public DungeonService(IDungeonSpawner dungeonSpawner, 
            IMapService mapService, 
            ICustomLogger logger)
        {
            _dungeonSpawner = dungeonSpawner;
            _mapService = mapService;
            _logger = logger;
        }

        public async UniTask CreateDungeon()
        {
            _branches = await _dungeonSpawner.SpawnDungeon();

            _startRoom = _dungeonSpawner.StartRoom;
            _currentRoom = _startRoom;

            SubscribeStartRoom();
        }

        private List<Room> FindCurrentBranch()
        {
            foreach (var branch in _branches.Values)
            {
                if (branch.Contains(_currentRoom))
                    return branch;
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
                return true;
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
                            SubscribeCurrentRoomExits(Direction.Top);
                            break;
                        case Direction.Down:
                            SubscribeCurrentRoomExits(Direction.Down);
                            break;
                        case Direction.Right:
                            SubscribeCurrentRoomExits(Direction.Right);
                            break;
                        case Direction.Left:
                            SubscribeCurrentRoomExits(Direction.Left);
                            break;
                        default:
                            _logger.LogError(new Exception($"{direction} not found"));
                            break;
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
                    continue;
                
                _currentRoom.Doors[direction].SetIsReturnExit(true);
                SetTilePassages(exitDirection, direction);

                _currentRoom.Doors[direction].Entered
                    .Subscribe(_ => GoToNextRoomInBranch())
                    .AddTo(_currentRoomDisposable);
            }
        }

        private void SetTilePassages(Direction exitDirection, Direction direction)
        {
            foreach (var tile in _mapService.Tiles.Values)
            { 
                if (tile.Logic.IsPassage == false)
                    continue;
                
                tile.Logic.IsEnterInPreviousRoom = false;
                tile.Logic.IsEnterInNewRoom = false;

                if (tile.Logic.PassageDirection == exitDirection)
                {
                    tile.Logic.IsEnterInPreviousRoom = true;
                    continue;
                }

                if (tile.Logic.PassageDirection == direction)
                    tile.Logic.IsEnterInNewRoom = true;
            }
        }

        private void GoToFirstRoomInBranch(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top:
                {
                    EnterInRoom(_branches[Direction.Top].First());
                    SubscribeCurrentRoomExits(Direction.Down);
                    break;
                }
                case Direction.Down:
                {
                    EnterInRoom(_branches[Direction.Down].First());
                    SubscribeCurrentRoomExits(Direction.Top);
                    break;
                }
                case Direction.Right:
                {
                    EnterInRoom(_branches[Direction.Right].First());
                    SubscribeCurrentRoomExits(Direction.Left);
                    break;
                }
                case Direction.Left:
                {
                    EnterInRoom(_branches[Direction.Left].First());
                    SubscribeCurrentRoomExits(Direction.Right);
                    break;
                }
                default:
                    _logger.LogError(new Exception($"{direction} not found"));
                    break;;
            }
        }

        private void EnterInRoom(Room room)
        {
            _currentRoom.DisableRoom();

            _previousRoom = _currentRoom;
            _currentRoom = room;

            _currentRoomDisposable.Clear();
            
            if (IsNotStartRoom()) 
                TryFindExitInNextRoom();

            ResetLastDoors();
            
            room.EnableRoom();
        }

        private void ResetLastDoors()
        {
            foreach (var door in _previousRoom.Doors.Values) 
                door.SetIsReturnExit(false);
        }

        private bool IsNotStartRoom() => 
            _previousRoom != _startRoom && _currentRoom != _startRoom;

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