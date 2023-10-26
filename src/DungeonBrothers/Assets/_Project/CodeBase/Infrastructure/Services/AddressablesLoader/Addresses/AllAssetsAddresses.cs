using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI;
using _Project.CodeBase.Infrastructure.Services.SceneLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/All", fileName = "AllAssetsAddresses")]
    public class AllAssetsAddresses : ScriptableObject
    {
        public ScenesAddresses Scenes;
        public AssetReferenceGameObject Camera;
        public AllGameplayAddresses AllGameplayAddresses;
        public AllUIAddresses AllUIAddresses;
    }
}