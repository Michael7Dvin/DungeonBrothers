using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Balance/AttackRange", fileName = "AttackRangeConfig")]
    public class AttackRangeConfig : ScriptableObject
    {
        [field:SerializeField] public int MeleeRange { get; private set; }
        [field:SerializeField] public int RangedRange { get; private set; }
    }
}