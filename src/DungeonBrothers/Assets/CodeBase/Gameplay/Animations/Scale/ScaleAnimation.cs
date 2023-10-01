using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Scale
{
    public class ScaleAnimation : MonoBehaviour
    {
        private Tweener _currentTweener;
        
        private void OnDisable()
        {
            if (_currentTweener.IsActive()) 
                _currentTweener.Kill();
        }
        
        public void Scale(ScaleConfig scaleConfig)
        {
            _currentTweener = GetScale(scaleConfig);
            
            _currentTweener.Play();
        }
        
        public async UniTask ScaleUniTask(ScaleConfig scaleConfig)
        {
            _currentTweener = GetScale(scaleConfig);
            
            await _currentTweener.Play().ToUniTask();
        }

        private Tweener GetScale(ScaleConfig scaleConfig)
        {
            if (_currentTweener.IsActive())
                _currentTweener.Kill();
            
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