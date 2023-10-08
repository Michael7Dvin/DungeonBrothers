using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Color
{
    public class ColorAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Tween _currentTween;
        
        public void DoColor(ColorConfig colorConfig)
        {
            _currentTween = GetColorTween(colorConfig);

            _currentTween.Play();
        } 
        
        public async UniTask DoColorTask(ColorConfig colorConfig)
        {
            _currentTween = GetColorTween(colorConfig);

            await _currentTween.Play().ToUniTask();
        }
        
        private Tween GetColorTween(ColorConfig colorConfig)
        {
            if (_currentTween.IsActive())
                _currentTween.Kill();

            UnityEngine.Color endColor = colorConfig.EndColor;
            float duration = colorConfig.Duration;
            Ease ease = colorConfig.Ease;
            
            return _spriteRenderer.material
                .DOColor(endColor, duration)
                .SetEase(ease)
                .SetUpdate(true);
        }
    }
}