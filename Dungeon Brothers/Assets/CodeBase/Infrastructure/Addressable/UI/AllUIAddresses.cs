using CodeBase.Infrastructure.Addressable.UI.Common;
using CodeBase.Infrastructure.Addressable.UI.Gameplay;
using UnityEngine;

namespace CodeBase.Infrastructure.Addressable.UI
{
    [CreateAssetMenu(menuName = "Addresses/UI/AllUIAddresses", fileName = "AllUIAddresses")]
    public class AllUIAddresses : ScriptableObject
    {
        [field: SerializeField] public CommonUIAddresses CommonUiAddresses { get; private set; } 
        [field: SerializeField] public GameplayUIAddresses GameplayUIAddresses { get; private set; }
    }
}