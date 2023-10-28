using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.View.Outline;
using CodeBase.Gameplay.Services.Visualizers.ActiveCharacter;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/All", fileName = "AllStaticData")]
    public class AllStaticData : ScriptableObject
    {
        public AllCharactersConfigs AllCharactersConfigs;
        public AllAssetsAddresses AssetsAddresses;
        public TileColorsConfig TileColorsConfig;
        public AllGameBalanceConfig GameBalanceConfig;
        public CharacterOutlineColors CharacterOutlineColors;
    }
}