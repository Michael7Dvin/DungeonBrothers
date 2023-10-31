using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.Gameplay.Tiles
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/Gameplay/Tiles", fileName = "TilesAddresses")]
    public class TilesAddresses : ScriptableObject
    {
        public AssetReferenceGameObject DungeonGround;
    }
}