using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Rooms;

namespace Project.CodeBase.Gameplay.Spawner.Rooms
{
    public interface IRoomSpawner
    {
        public UniTask<Room> CreateRoom(Direction direction);
        public UniTask<Room> CreateStartRoom();
    }
}