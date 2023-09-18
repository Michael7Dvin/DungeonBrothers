using CodeBase.Infrastructure.Addressable;
using CodeBase.Infrastructure.Configs.Character;
using CodeBase.Infrastructure.Services.SceneLoading;

namespace CodeBase.Infrastructure.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        AllCharactersConfigs AllCharactersConfigs { get; }
    }
}