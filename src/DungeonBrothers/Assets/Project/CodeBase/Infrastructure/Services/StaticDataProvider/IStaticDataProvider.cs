using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Characters.View.Outline;
using _Project.CodeBase.Gameplay.Services.Visualizers.ActiveCharacter;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;

namespace _Project.CodeBase.Infrastructure.Services.StaticDataProvider
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