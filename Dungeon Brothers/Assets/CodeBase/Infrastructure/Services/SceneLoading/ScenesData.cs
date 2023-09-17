using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.SceneLoading
{
    [CreateAssetMenu(fileName = "Scenes Data", menuName = "Scenes Data")]
    public class ScenesData : ScriptableObject
    {
        [field: SerializeField] public AssetReference MainMenu { get; private set; }
        [field: SerializeField] public AssetReference Island { get; private set; }
    }
}