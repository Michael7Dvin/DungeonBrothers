using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Rooms;
using Project.CodeBase.Gameplay.Spawner.Rooms;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Project.CodeBase.Infrastructure.Services.Factories.Rooms
{
    public class RoomFactory : IRoomFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IObjectResolver _objectResolver;

        private readonly AllRoomsConfig _allRoomsConfig;

        public RoomFactory(IAddressablesLoader addressablesLoader, 
            IObjectResolver objectResolver,
            IStaticDataProvider staticDataProvider)
        {
            _addressablesLoader = addressablesLoader;
            _objectResolver = objectResolver;

            _allRoomsConfig = staticDataProvider.AllRoomsConfig;
        }

        public async UniTask WarmUp()
        {
            foreach (var config in _allRoomsConfig.Rooms.Values)
                foreach (var room in config)
                    await _addressablesLoader.LoadGameObject(room.Room);
        }

        public async UniTask<Room> Create(RoomConfig config)
        {
            Room prefab = await _addressablesLoader.LoadComponent<Room>(config.Room);
            Room gameObject = _objectResolver.Instantiate(prefab);

            return gameObject;
        }
    }
}