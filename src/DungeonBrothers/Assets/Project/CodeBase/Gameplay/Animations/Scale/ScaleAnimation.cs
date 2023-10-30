using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Animations.Scale
{
    public class ScaleAnimation 
    {
        private readonly Transform _transform;
        
        private Tween _currentTween;

        public ScaleAnimation(Transform transform)
        {
            _transform = transform;
        }
        
        public Tween DoScale(ScaleAnimationConfig scaleAnimationConfig)
        {
            _currentTween = GetScale(scaleAnimationConfig);
            return _currentTween.Play();
        }

        private Tween GetScale(ScaleAnimationConfig scaleAnimationConfig)
        {
            if (_currentTween.IsActive())
                _currentTween.Kill();
            
            Vector3 scale = scaleAnimationConfig.Scale;
            float duration = scaleAnimationConfig.Duration;
            Ease ease = scaleAnimationConfig.Ease;
            
            return _transform
                .DOScale(scale, duration)
                .SetEase(ease)
                .SetUpdate(UpdateType.Normal, true);
        }
    }
}