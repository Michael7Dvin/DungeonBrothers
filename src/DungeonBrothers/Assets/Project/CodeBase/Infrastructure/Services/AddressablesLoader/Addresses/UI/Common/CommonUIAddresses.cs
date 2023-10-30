using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Common
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/UI/Common", fileName = "CommonUIAddresses")]
    public class CommonUIAddresses : ScriptableObject
    {
        public AssetReferenceGameObject Canvas;
    }
}