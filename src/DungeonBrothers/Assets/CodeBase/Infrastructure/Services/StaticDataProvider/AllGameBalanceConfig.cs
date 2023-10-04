using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Balance/AllGameBalance", fileName = "AllGameBalanceConfig")]
    public class AllGameBalanceConfig : ScriptableObject
    {
        [field: SerializeField] public BonusDamageConfig BonusDamageConfig { get; private set; }
        [field: SerializeField] public AttackRangeConfig AttackRangeConfig { get; private set; }
    }
}