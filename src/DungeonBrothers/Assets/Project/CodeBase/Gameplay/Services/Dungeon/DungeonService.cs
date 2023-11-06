using System.Collections.Generic;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Spawner.Dungeon;

namespace Project.CodeBase.Gameplay.Services.Dungeon
{
    public class DungeonService : IDungeonService
    {
        private IDungeonSpawner _dungeonSpawner;
        
        private readonly List<Room> _leftBranchRooms = new();
        private readonly List<Room> _rightBranchRooms = new();
        private readonly List<Room> _downBranchRooms = new();
        private readonly List<Room> _topBranchRooms = new();

        private Room _currentRoom;

        private List<Room> FindCurrentBranch()
        {
            if (_leftBranchRooms.Contains(_currentRoom))
                return _leftBranchRooms;

            if (_rightBranchRooms.Contains(_currentRoom))
                return _rightBranchRooms;

            if (_downBranchRooms.Contains(_currentRoom))
                return _downBranchRooms;

            if (_topBranchRooms.Contains(_currentRoom))
                return _topBranchRooms;

            return null;
        }

        private bool TryFindPreviousRoom(out Room room)
        {
            List<Room> currentBranch = FindCurrentBranch();
            int index = currentBranch.IndexOf(_currentRoom);
            room = null;
            
            if (index == 0)
                return false;

            room = currentBranch[index - 1];
            return true;
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
        
        public void GoToNextRoomInBranch()
        {
            if (TryFindNextRoom(out Room room))
            {
                _currentRoom.DisableRoom();

                _currentRoom = room;
                
                room.EnableRoom();
            }
        }

        public void GoToPreviousRoomInBranch()
        {
            if (TryFindPreviousRoom(out Room room))
            {
                _currentRoom.DisableRoom();
                
                _currentRoom = room;
                
                room.EnableRoom();
                
            }
        }
        
        public void CreateDungeon()
        {
            _dungeonSpawner.SpawnDungeon(out Dictionary<Room, RoomInfo> leftBranchRooms,
                out Dictionary<Room, RoomInfo> rightBranchRooms,
                out Dictionary<Room, RoomInfo> downBranchRooms,
                out Dictionary<Room, RoomInfo> topBranchRooms);

            foreach (var room in leftBranchRooms) 
                _leftBranchRooms.Add(room.Key);

            foreach (var room in rightBranchRooms) 
                _rightBranchRooms.Add(room.Key);
            
            foreach (var room in downBranchRooms) 
                _downBranchRooms.Add(room.Key);
            
            foreach (var room in topBranchRooms) 
                _topBranchRooms.Add(room.Key);
        }
    }
}