using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Gameplay
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/UI/Gameplay", fileName = "GameplayUIAddresses")]
    public class GameplayUIAddresses : ScriptableObject
    {
        public AssetReferenceGameObject TurnQueueView;
        public AssetReferenceGameObject SkipTurnButton;
        public AssetReferenceGameObject HealthBar;
    }
}