using CodeBase.Infrastructure.Addressable.Addresses.UI;
using CodeBase.Infrastructure.Services.SceneLoading;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Addressable.Addresses
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/All", fileName = "AllAssetsAddresses")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public ScenesAddresses Scenes { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Tile { get; private set; }
        [field: SerializeField] public AllUIAddresses AllUIAddresses { get; private set; }
    }
}