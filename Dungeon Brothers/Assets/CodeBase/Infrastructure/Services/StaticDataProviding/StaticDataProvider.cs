using CodeBase.Infrastructure.Addressable;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
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