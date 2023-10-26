using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Balance/AllGameBalance", fileName = "AllGameBalanceConfig")]
    public class AllGameBalanceConfig : ScriptableObject
    {
        public BonusDamageConfig BonusDamageConfig;
        public AttackRangeConfig AttackRangeConfig;
    }
}