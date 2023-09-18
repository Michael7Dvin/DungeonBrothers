using CodeBase.Infrastructure.Addressable;
using CodeBase.Infrastructure.Configs.Character;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticDataProviding
{
    [CreateAssetMenu(menuName = "StaticData/All", fileName = "AllStaticData")]
    public class AllStaticData : ScriptableObject
    {
        public AllCharactersConfigs AllCharactersConfigs;
        public AllAssetsAddresses AssetsAddresses;
    }
}