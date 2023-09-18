using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.SceneLoading
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "Addresses/SceneData")]
    public class ScenesData : ScriptableObject
    {
        [field: SerializeField] public AssetReference Level { get; private set; }
    }
}