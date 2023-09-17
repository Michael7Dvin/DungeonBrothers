using CodeBase.Infrastructure.Addressable;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        
        ScenesData ScenesData { get; }
    }
}