using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Animations.Scale
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Scale", fileName = "Scale")]
    public class ScaleAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public float Multiplier { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}