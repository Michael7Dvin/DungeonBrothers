using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.AddressablesLoader.Loader
{
    public interface IAddressablesLoader
    {
        UniTask<GameObject> LoadGameObject(AssetReferenceGameObject assetReference);
        UniTask<T> LoadComponent<T>(AssetReferenceGameObject assetReference) where T : Component;
        void ClearCache();
    }
}