using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Visualizers.ActiveCharacter;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        AllCharactersConfigs AllCharactersConfigs { get; }
        TileColorsConfig TileColors { get; }
        AllGameBalanceConfig GameBalanceConfig { get; }
        CharacterOutlineColors CharacterOutlineColors { get; }
    }
}