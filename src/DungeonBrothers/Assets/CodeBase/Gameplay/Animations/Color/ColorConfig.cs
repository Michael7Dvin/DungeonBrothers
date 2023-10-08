using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Color
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Animations/Color", fileName = "Color")]
    public class ColorConfig : ScriptableObject
    {
        public UnityEngine.Color EndColor;
        public float Duration;
        public Ease Ease;
    }
}