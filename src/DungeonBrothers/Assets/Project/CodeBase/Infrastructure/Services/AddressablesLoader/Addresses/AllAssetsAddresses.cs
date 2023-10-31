using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.Gameplay;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI;
using Project.CodeBase.Infrastructure.Services.SceneLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses
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