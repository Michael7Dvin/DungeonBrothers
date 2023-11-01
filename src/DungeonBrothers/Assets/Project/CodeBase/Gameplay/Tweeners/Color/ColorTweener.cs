using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Tweeners.Color
{
    public class ColorTweener 
    {
        private readonly SpriteRenderer _spriteRenderer;

        private Tween _currentTween;
        private UnityEngine.Color _lastColor;
        
        public ColorTweener(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }

        
        public Tween DoColorWithReset(ColorTweenerConfig config)
        {
            _currentTween = GetColorSequence(config);
            return _currentTween.Play();
        } 
        
        private Sequence GetColorSequence(ColorTweenerConfig config)
        {
            if (_currentTween.IsActive())
                _currentTween.Kill();

            _lastColor = _spriteRenderer.material.color;

            UnityEngine.Color color = config.EndColor;
            float duration = config.Duration;
            Ease ease = config.Ease;
            
            Sequence sequence = DOTween.Sequence();

            return sequence
                .Append(GetColorTween(color, duration, ease))
                .Append(GetColorTween(_lastColor, duration, ease));
        }

        private Tween GetColorTween(UnityEngine.Color color, float duration, Ease ease) =>
            _spriteRenderer.material
                .DOColor(color, duration)
                .SetEase(ease)
                .SetUpdate(true);
    }
}