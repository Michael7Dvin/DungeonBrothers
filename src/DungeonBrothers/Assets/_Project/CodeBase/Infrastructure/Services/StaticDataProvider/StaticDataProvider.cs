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
            TileColorsConfig = allStaticData.TileColorsConfig;
            GameBalanceConfig = allStaticData.GameBalanceConfig;
        }

        public AllAssetsAddresses AssetsAddresses { get; }
        public AllCharactersConfigs AllCharactersConfigs { get; }
        public TileColorsConfig TileColorsConfig { get; }
        public AllGameBalanceConfig GameBalanceConfig { get; }
    }
}