using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllStaticData allStaticData)
        {
            AssetsAddresses = allStaticData.AssetsAddresses;
            AllCharactersConfigs = allStaticData.AllCharactersConfigs;
            TileColorConfig = allStaticData.TileColorConfig;
        }

        public AllAssetsAddresses AssetsAddresses { get; }
        public AllCharactersConfigs AllCharactersConfigs { get; }
        public TileColorConfig TileColorConfig { get; }
    }
}