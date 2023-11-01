using Project.CodeBase.Gameplay.Tweeners.Color;
using Project.CodeBase.Gameplay.Tweeners.Scale;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.CodeBase.Gameplay.Tweeners.Hit
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Hit", fileName = "Hit")]
    public class 
        HitTweenerConfig : ScriptableObject
    {
        [FormerlySerializedAs("ScaleInHit")] public ScaleTweenerConfig ScaleTweenerAtHit;
        [FormerlySerializedAs("ColorInHit")] public ColorTweenerConfig ColorTweenerAtHit;
     
    }
}