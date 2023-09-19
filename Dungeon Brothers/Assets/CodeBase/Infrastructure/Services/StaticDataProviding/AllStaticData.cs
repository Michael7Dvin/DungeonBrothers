using CodeBase.Infrastructure.Addressable.Addresses;
using CodeBase.Infrastructure.Configs.Character;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
{
    [CreateAssetMenu(menuName = "StaticData/All", fileName = "AllStaticData")]
    public class AllStaticData : ScriptableObject
    {
        public AllCharactersConfigs AllCharactersConfigs;
        public AllAssetsAddresses AssetsAddresses;
    }
}