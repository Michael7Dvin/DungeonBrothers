using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        AllCharactersConfigs AllCharactersConfigs { get; }
        TileColorConfig TileColorConfig { get; }
    }
}