using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Animations.Scale
{
    public class ScaleAnimation 
    {
        private readonly Transform _transform;
        
        private Tween _currentTween;
        private ScaleAnimationConfig _currentConfig;

        private Vector3 _lastScale;
        private readonly Vector3 _startScale;
        
        public ScaleAnimation(Transform transform)
        {
            _transform = transform;

            _startScale = transform.localScale;
        }
        
        public Tween DoScaleWithReset(ScaleAnimationConfig config)
        {
            TryKillActiveTween();
            
            _currentTween = GetScaleSequence(config);
            return _currentTween.Play();
        }

        public Tween DoScaleWithoutReset(ScaleAnimationConfig config)
        {
            TryKillActiveTween();
            
            Vector3 scale = config.Multiplier * _startScale;
            float duration = config.Duration;
            Ease ease = config.Ease;
            
            _currentTween = GetScaleTween(scale, duration, ease);
            return _currentTween.Play();
        }

        private Sequence GetScaleSequence(ScaleAnimationConfig config)
        {
            _lastScale = _transform.localScale;
            
            Vector3 scale = _lastScale * config.Multiplier;
            float duration = config.Duration;
            Ease ease = config.Ease;
            
            Sequence sequence = DOTween.Sequence();

            return sequence
                .Append(GetScaleTween(scale, duration, ease))
                .Append(GetScaleTween(_lastScale, duration, ease));
        }

        private void TryKillActiveTween()
        {
            if (_currentTween.IsActive())
                _currentTween.Kill();
        }

        private Tween GetScaleTween(Vector3 scale, float duration, Ease ease) =>
            _transform.DOScale(scale, duration)
                .SetEase(ease)
                .SetUpdate(UpdateType.Normal, true);
    }
}