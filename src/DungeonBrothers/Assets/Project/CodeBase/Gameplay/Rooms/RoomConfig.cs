using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.CodeBase.Gameplay.Rooms
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Room", fileName = "RoomConfig")]
    public class RoomConfig : ScriptableObject
    {
        public AssetReferenceGameObject Room;
    }
}