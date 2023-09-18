using CodeBase.Infrastructure.Addressable;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
    }
}