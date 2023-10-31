using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.View.Animators
{
    public class CharacterAnimator : ICharacterAnimator
    {
        private readonly int _idleHash = Animator.StringToHash("Idle");
        private readonly int _walkHash = Animator.StringToHash("Walk");
        
        private readonly Animator _animator;

        public CharacterAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void PlayIdle() =>
            _animator.SetTrigger(_idleHash);
        
        public void PlayWalk() =>
            _animator.SetTrigger(_walkHash);
    }
}