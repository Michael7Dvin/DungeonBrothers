using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.Infrastructure.Services.SceneLoader
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/Scenes", fileName = "SceneAddresses")]
    public class ScenesAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReference Level { get; private set; }
    }
}