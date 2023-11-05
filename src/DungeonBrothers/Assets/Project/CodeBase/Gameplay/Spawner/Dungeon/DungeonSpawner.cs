using System.Collections.Generic;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Services.Random;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;

namespace Project.CodeBase.Gameplay.Spawner.Dungeon
{
    public class DungeonSpawner : IDungeonSpawner
    {
        private readonly IRandomService _randomService;
        private readonly LinkedList<Room> _rooms = new();

        private float _lenghtDungeon;
        
        private int _maxRooms;

        public DungeonSpawner(IRandomService randomService,
            IStaticDataProvider staticDataProvider)
        {
            _randomService = randomService;
        }

        private void SpawnDungeon()
        {
            _lenghtDungeon = _randomService.DoRandomInRange(1, _maxRooms + 1);

            
            
            
            for (int i = 0; i < _lenghtDungeon; i++)
            {
                
            }
        }
    }
}