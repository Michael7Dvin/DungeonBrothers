using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/Gameplay/Tiles", fileName = "TilesAddresses")]
    public class TilesAddresses : ScriptableObject
    {
        public AssetReferenceGameObject DungeonGround;
    }
}