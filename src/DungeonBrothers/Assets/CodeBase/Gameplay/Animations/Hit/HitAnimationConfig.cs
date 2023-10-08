using CodeBase.Gameplay.Animations.Color;
using CodeBase.Gameplay.Animations.Scale;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Hit
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Hit", fileName = "Hit")]
    public class HitAnimationConfig : ScriptableObject
    {
        public ScaleConfig ScaleInHit;
        public ScaleConfig ScaleOutHit;
        
        public ColorConfig ColorInHit;
        public ColorConfig ColorOutHit;
    }
}