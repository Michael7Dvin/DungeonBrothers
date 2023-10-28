using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Animations.Movement
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Movement", fileName = "MovementAnimationConfig")]
    public class MovementAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}