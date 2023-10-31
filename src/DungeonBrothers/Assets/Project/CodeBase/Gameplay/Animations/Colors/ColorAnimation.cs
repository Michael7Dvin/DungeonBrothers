using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Animations.Colors
{
    public class ColorAnimation 
    {
        private readonly SpriteRenderer _spriteRenderer;

        private Tween _currentTween;
        private Color _lastColor;
        
        public ColorAnimation(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }

        
        public Tween DoColorWithReset(ColorAnimationConfig config)
        {
            _currentTween = GetColorSequence(config);
            return _currentTween.Play();
        } 
        
        private Sequence GetColorSequence(ColorAnimationConfig config)
        {
            if (_currentTween.IsActive())
                _currentTween.Kill();

            _lastColor = _spriteRenderer.material.color;

            Color color = config.EndColor;
            float duration = config.Duration;
            Ease ease = config.Ease;
            
            Sequence sequence = DOTween.Sequence();

            return sequence
                .Append(GetColorTween(color, duration, ease))
                .Append(GetColorTween(_lastColor, duration, ease));
        }

        private Tween GetColorTween(Color color, float duration, Ease ease) =>
            _spriteRenderer.material
                .DOColor(color, duration)
                .SetEase(ease)
                .SetUpdate(true);
    }
}