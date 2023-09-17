using CodeBase.Infrastructure.Addressable;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllAssetsAddresses allAssetsAddresses, ScenesData scenesData)
        {
            AssetsAddresses = allAssetsAddresses;
            ScenesData = scenesData;
        }

        public AllAssetsAddresses AssetsAddresses { get; }
        public ScenesData ScenesData { get; }
    }
}