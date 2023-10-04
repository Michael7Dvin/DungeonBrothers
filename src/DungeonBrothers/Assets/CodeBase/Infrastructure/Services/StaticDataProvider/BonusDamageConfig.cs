using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Balance/BonusDamage", fileName = "BonusDamageConfig")]
    public class BonusDamageConfig : ScriptableObject
    {
        [field: SerializeField] public int TotalBonusDamagePerLevel { get; private set; }
        [field: SerializeField] public int TotalBonusDamagePerMainStat { get; private set; }
    }
}