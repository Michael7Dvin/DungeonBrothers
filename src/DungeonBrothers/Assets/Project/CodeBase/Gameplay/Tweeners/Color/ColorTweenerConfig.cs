using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Tweeners.Color
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Color", fileName = "Color")]
    public class ColorTweenerConfig : ScriptableObject
    {
        public UnityEngine.Color EndColor;
        public float Duration;
        public Ease Ease;
    }
}