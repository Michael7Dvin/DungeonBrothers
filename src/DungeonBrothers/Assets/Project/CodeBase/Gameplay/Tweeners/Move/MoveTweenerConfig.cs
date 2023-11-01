using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Tweeners.Move
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Movement", fileName = "MovementAnimationConfig")]
    public class MoveTweenerConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}