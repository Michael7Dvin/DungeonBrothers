using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Scale
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Scale", fileName = "Scale")]
    public class ScaleConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 Scale { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}