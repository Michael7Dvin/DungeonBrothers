using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Tweeners.Scale
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Scale", fileName = "Scale")]
    public class ScaleTweenerConfig : ScriptableObject
    {
        [field: SerializeField] public float Multiplier { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}