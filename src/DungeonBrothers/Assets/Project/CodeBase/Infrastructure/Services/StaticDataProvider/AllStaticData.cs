using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.View.Outline;
using Project.CodeBase.Gameplay.Spawner.Rooms;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;
using UnityEngine;

namespace Project.CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/All", fileName = "AllStaticData")]
    public class AllStaticData : ScriptableObject
    {
        public AllCharactersConfigs AllCharactersConfigs;
        public AllAssetsAddresses AssetsAddresses;
        public TileColorsConfig TileColorsConfig;
        public AllGameBalanceConfig GameBalanceConfig;
        public CharacterOutlineColors CharacterOutlineColors;
        public AllRoomsConfig AllRoomsConfig;
    }
}