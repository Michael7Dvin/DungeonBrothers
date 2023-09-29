using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Move
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Move", fileName = "MoveAnimationConfig")]
    public class MoveAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 EndCoordinate { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public bool IsSnapping { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}