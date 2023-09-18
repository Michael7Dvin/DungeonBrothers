using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Addressable.UI.Common
{
    [CreateAssetMenu(menuName = "Addresses/UI/CommonUIAddresses", fileName = "CommonUIAddresses")]
    public class CommonUIAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Canvas { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject EventSystem { get; private set; }
    }
}