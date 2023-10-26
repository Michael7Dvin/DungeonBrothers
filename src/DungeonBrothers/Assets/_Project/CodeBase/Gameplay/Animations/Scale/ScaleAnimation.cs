using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Scale
{
    public class ScaleAnimation : MonoBehaviour
    {
        private Tween _currentTween;
        
        private void OnDisable()
        {
            if (_currentTween.IsActive()) 
                _currentTween.Kill();
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
            
            return transform
                .DOScale(scale, duration)
                .SetEase(ease)
                .SetUpdate(UpdateType.Normal, true);
        }
    }
}