using CodeBase.Gameplay.Animations.Colors;
using CodeBase.Gameplay.Animations.Scale;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Gameplay.Animations.Hit
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Hit", fileName = "Hit")]
    public class HitAnimationConfig : ScriptableObject
    {
        [FormerlySerializedAs("ScaleInHit")] public ScaleAnimationConfig ScaleAnimationAtHit;
        [FormerlySerializedAs("ScaleOutHit")] public ScaleAnimationConfig ScaleAnimationAfterHit;
        
        [FormerlySerializedAs("ColorInHit")] public ColorAnimationConfig ColorAnimationAtHit;
        [FormerlySerializedAs("ColorOutHit")] public ColorAnimationConfig ColorAnimationAfterHit;
    }
}