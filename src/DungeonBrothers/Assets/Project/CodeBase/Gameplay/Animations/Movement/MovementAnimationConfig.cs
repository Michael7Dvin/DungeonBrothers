using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Animations.Movement
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Movement", fileName = "MovementAnimationConfig")]
    public class MovementAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}