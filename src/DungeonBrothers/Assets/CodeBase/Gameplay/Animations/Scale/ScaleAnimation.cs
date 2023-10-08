using Cysharp.Threading.Tasks;
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
        
        public void Scale(ScaleConfig scaleConfig)
        {
            _currentTween = GetScale(scaleConfig);
            
            _currentTween.Play();
        }
        
        public async UniTask ScaleUniTask(ScaleConfig scaleConfig)
        {
            _currentTween = GetScale(scaleConfig);
            
            await _currentTween.Play().ToUniTask();
        }

        private Tween GetScale(ScaleConfig scaleConfig)
        {
            if (_currentTween.IsActive())
                _currentTween.Kill();
            
            Transform transform = this.transform;
            
            Vector3 Scale = scaleConfig.Scale;
            float Duration = scaleConfig.Duration;
            Ease ease = scaleConfig.Ease;
            
            return transform
                .DOScale(Scale, Duration)
                .SetEase(ease)
                .SetUpdate(UpdateType.Normal, true);
        }
    }
}