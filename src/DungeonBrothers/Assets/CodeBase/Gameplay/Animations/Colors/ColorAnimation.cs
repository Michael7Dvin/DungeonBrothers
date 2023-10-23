using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Colors
{
    public class ColorAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Tween _currentTween;
        
        public Tween DoColor(ColorAnimationConfig colorAnimationConfig)
        {
            _currentTween = GetColorTween(colorAnimationConfig);
            return _currentTween.Play();
        } 
        
        private Tween GetColorTween(ColorAnimationConfig colorAnimationConfig)
        {
            if (_currentTween.IsActive())
                _currentTween.Kill();

            Color endColor = colorAnimationConfig.EndColor;
            float duration = colorAnimationConfig.Duration;
            Ease ease = colorAnimationConfig.Ease;
            
            return _spriteRenderer.material
                .DOColor(endColor, duration)
                .SetEase(ease)
                .SetUpdate(true);
        }
    }
}