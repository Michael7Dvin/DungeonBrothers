using Project.CodeBase.Gameplay.Animations.Colors;
using Project.CodeBase.Gameplay.Animations.Scale;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.CodeBase.Gameplay.Animations.Hit
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Hit", fileName = "Hit")]
    public class HitAnimationConfig : ScriptableObject
    {
        [FormerlySerializedAs("ScaleInHit")] public ScaleAnimationConfig ScaleAnimationAtHit;
        [FormerlySerializedAs("ColorInHit")] public ColorAnimationConfig ColorAnimationAtHit;
     
    }
}