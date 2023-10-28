using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Characters.View.Outline;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;

namespace _Project.CodeBase.Infrastructure.Services.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllStaticData allStaticData)
        {
            AssetsAddresses = allStaticData.AssetsAddresses;
            AllCharactersConfigs = allStaticData.AllCharactersConfigs;
            TileColors = allStaticData.TileColorsConfig;
            GameBalanceConfig = allStaticData.GameBalanceConfig;
            CharacterOutlineColors = allStaticData.CharacterOutlineColors;
        }

        public AllAssetsAddresses AssetsAddresses { get; }
        public AllCharactersConfigs AllCharactersConfigs { get; }
        public TileColorsConfig TileColors { get; }
        public AllGameBalanceConfig GameBalanceConfig { get; }
        public CharacterOutlineColors CharacterOutlineColors { get; }
    }
}