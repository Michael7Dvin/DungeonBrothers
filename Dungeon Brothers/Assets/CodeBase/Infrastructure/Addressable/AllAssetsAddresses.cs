using CodeBase.Infrastructure.Services.SceneLoading;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Addressable
{
    [CreateAssetMenu(menuName = "Addresses/AllAssetsAddresses", fileName = "AllAssetsAddresses")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public ScenesAddresses Scenes { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Tile { get; private set; }
    }
}