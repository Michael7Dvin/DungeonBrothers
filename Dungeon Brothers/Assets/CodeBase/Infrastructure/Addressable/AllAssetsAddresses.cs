using CodeBase.Infrastructure.Services.SceneLoading;
using UnityEngine;

namespace CodeBase.Infrastructure.Addressable
{
    [CreateAssetMenu(menuName = "Addresses/AllAssetsAddresses", fileName = "AllAssetsAddresses")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public ScenesData ScenesData { get; private set; }
    }
}