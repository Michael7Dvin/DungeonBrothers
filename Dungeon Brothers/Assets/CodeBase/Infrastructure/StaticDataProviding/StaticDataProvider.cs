using CodeBase.Infrastructure.Addressable;
using CodeBase.Infrastructure.Configs.Character;

namespace CodeBase.Infrastructure.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllAssetsAddresses allAssetsAddresses,
            AllCharactersConfigs allCharactersConfigs)
        {
            AssetsAddresses = allAssetsAddresses;
            AllCharactersConfigs = allCharactersConfigs;
        }

        public AllCharactersConfigs AllCharactersConfigs { get; }
        public AllAssetsAddresses AssetsAddresses { get; }
    }
}