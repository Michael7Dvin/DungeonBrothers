using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.View.Outline;
using Project.CodeBase.Gameplay.Spawner.Rooms;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;

namespace Project.CodeBase.Infrastructure.Services.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        AllCharactersConfigs AllCharactersConfigs { get; }
        TileColorsConfig TileColors { get; }
        AllGameBalanceConfig GameBalanceConfig { get; }
        CharacterOutlineColors CharacterOutlineColors { get; }
        AllRoomsConfig AllRoomsConfig { get; }
    }
}