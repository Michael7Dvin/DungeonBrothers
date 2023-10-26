using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;

namespace _Project.CodeBase.Infrastructure.Services.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        AllCharactersConfigs AllCharactersConfigs { get; }
        TileColorsConfig TileColorsConfig { get; }
        AllGameBalanceConfig GameBalanceConfig { get; }
    }
}