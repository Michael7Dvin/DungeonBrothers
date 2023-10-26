using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI;
using _Project.CodeBase.Infrastructure.Services.SceneLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/All", fileName = "AllAssetsAddresses")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public ScenesAddresses Scenes { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Tile { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Camera { get; private set; }
        [field: SerializeField] public AllUIAddresses AllUIAddresses { get; private set; }
    }
}