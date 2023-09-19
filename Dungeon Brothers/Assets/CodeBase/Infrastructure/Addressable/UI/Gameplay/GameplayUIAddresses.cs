using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Addressable.UI.Gameplay
{
    [CreateAssetMenu(menuName = "Addresses/UI/GameplayUiAddresses", fileName = "GameplayUIAddresses")]
    public class GameplayUIAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject TurnQueueView { get; private set; }
    }
}