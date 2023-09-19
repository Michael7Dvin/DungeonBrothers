using CodeBase.Infrastructure.Addressable.Addresses;
using CodeBase.Infrastructure.Configs.Character;

namespace CodeBase.Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        AllCharactersConfigs AllCharactersConfigs { get; }
    }
}