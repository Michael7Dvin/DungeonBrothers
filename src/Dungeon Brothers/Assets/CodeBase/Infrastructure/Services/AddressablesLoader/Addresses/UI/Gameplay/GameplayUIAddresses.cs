using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Gameplay
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/UI/Gameplay", fileName = "GameplayUIAddresses")]
    public class GameplayUIAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject TurnQueueView { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject SkipTurnButton { get; private set; }
    }
}