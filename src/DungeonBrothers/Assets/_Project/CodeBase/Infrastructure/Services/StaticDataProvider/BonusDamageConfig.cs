using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Balance/BonusDamage", fileName = "BonusDamageConfig")]
    public class BonusDamageConfig : ScriptableObject
    {
        public int TotalBonusDamagePerLevel;
        public int TotalBonusDamagePerMainStat;
    }
}