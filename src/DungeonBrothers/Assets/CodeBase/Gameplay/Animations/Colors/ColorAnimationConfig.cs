using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Colors
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Color", fileName = "Color")]
    public class ColorAnimationConfig : ScriptableObject
    {
        public Color EndColor;
        public float Duration;
        public Ease Ease;
    }
}