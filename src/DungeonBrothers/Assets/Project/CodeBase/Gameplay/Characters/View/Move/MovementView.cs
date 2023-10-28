using _Project.CodeBase.Gameplay.Animations.Movement;
using _Project.CodeBase.Gameplay.Characters.View.Sounds;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.CodeBase.Gameplay.Characters.View.Move
{
    public class MovementView : IMovementView
    {
        private readonly MovementAnimation _movementAnimation;
        private readonly CharacterSounds _characterSounds;

        private MovementAnimationConfig _config;

        [Inject]
        public void Inject(IStaticDataProvider staticDataProvider)
        {
            _config = staticDataProvider.AllCharactersConfigs.MovementAnimationConfig;
        }
        
        public MovementView(MovementAnimation movementAnimation, 
            CharacterSounds characterSounds)
        {
            _movementAnimation = movementAnimation;
            _characterSounds = characterSounds;
        }

        public async UniTask Move(Vector3[] path)
        {
            _characterSounds.PlaySoundInLoop(CharacterSoundType.Walk);
            await _movementAnimation.Move(path, _config.Speed, _config.Ease);
            _characterSounds.StopPlaySound();
        }
    }
}