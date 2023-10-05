using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Balance/AttackRange", fileName = "AttackRangeConfig")]
    public class AttackRangeConfig : ScriptableObject
    {
        public int MeleeRange;
        public int RangedRange;
    }
}