using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Balance/AttackRange", fileName = "AttackRangeConfig")]
    public class AttackRangeConfig : ScriptableObject
    {
        public int MeleeRange;
        public int RangedRange;
    }
}