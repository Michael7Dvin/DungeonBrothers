using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Common;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Gameplay;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/UI/All", fileName = "AllUIAddresses")]
    public class AllUIAddresses : ScriptableObject
    {
        public CommonUIAddresses CommonUiAddresses;
        public GameplayUIAddresses GameplayUIAddresses;
    }
}