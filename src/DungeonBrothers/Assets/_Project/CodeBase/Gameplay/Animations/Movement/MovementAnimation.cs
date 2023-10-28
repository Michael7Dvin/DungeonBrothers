using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Animations.Movement
{
    public class MovementAnimation 
    {
        private readonly Transform _transform;
        private readonly MovementAnimationConfig _config;
        
        private Tween _currentTween;
        
        public MovementAnimation(Transform transform,
            MovementAnimationConfig config)
        {
            _transform = transform;
            _config = config;
        }

        public async UniTask Move(Vector3[] path)
        {
            if (_currentTween.IsActive())
                _currentTween.Kill();
            
            _currentTween = _transform
                .DOPath(path, _config.Duration)
                .Play()
                .SetEase(_config.Ease);

            await _currentTween.Play();
        }
    }
}