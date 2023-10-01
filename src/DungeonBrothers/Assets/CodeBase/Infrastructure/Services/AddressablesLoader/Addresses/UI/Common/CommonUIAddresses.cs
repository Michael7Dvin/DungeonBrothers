using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Common
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/UI/Common", fileName = "CommonUIAddresses")]
    public class CommonUIAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Canvas { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject EventSystem { get; private set; }
    }
}