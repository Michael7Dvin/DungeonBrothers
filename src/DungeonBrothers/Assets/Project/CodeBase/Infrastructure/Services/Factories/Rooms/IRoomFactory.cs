using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Rooms;
using UnityEngine.AddressableAssets;

namespace Project.CodeBase.Gameplay.Spawner.Rooms
{
    public interface IRoomFactory
    {
        public UniTask<Room> Create(RoomConfig config);
    }
}