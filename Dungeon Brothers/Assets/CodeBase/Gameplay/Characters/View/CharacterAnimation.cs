using CodeBase.Gameplay.Animations.Move;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Characters.View
{
    public class CharacterAnimation
    {
        private readonly MoveAnimation _moveAnimation;

        private const float _animationDuration = 1;
        
        public CharacterAnimation(MoveAnimation moveAnimation)
        {
            _moveAnimation = moveAnimation;
        }

        public void PlayMoveAnimation(Vector3 endCoordinate) => 
            _moveAnimation.Move(endCoordinate, _animationDuration, false, Ease.InQuart);
    }
}