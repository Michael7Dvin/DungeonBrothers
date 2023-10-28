using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Characters.View.Outline;
using _Project.CodeBase.Gameplay.Services.Visualizers.ActiveCharacter;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.StaticDataProvider
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