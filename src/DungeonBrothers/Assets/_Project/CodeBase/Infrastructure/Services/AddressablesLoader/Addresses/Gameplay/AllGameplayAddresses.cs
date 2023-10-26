using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/Gameplay/AllGameplayAddresses", fileName = "AllGameplayAddresses")]
    public class AllGameplayAddresses : ScriptableObject
    {
        public TilesAddresses TilesAddresses;
        public SoundAddresses SoundAddresses;
    }
}