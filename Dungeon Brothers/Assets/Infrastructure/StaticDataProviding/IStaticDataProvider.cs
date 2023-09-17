using Infrastructure.Addressable;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        
        ScenesData ScenesData { get; }
    }
}