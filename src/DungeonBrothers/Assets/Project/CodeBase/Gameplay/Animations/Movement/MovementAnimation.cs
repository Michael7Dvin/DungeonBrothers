﻿using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Animations.Movement
{
    public class MovementAnimation 
    {
        private readonly Transform _transform;

        private Tween _currentTween;
        
        public MovementAnimation(Transform transform)
        {
            _transform = transform;
        }

        public async UniTask Move(Vector3 point, float speed, Ease ease)
        {
            if (_currentTween.IsActive())
                _currentTween.Kill();
            
            _currentTween = _transform
                .DOMove(point, speed)
                .SetSpeedBased()
                .Play()
                .SetEase(ease);

            await _currentTween.Play();
        }
    }
}