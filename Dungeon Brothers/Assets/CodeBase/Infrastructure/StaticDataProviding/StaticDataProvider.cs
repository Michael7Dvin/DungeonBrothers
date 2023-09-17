using CodeBase.Infrastructure.Addressable;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllAssetsAddresses allAssetsAddresses)
        {
            AssetsAddresses = allAssetsAddresses;
        }

        public AllAssetsAddresses AssetsAddresses { get; }
    }
}