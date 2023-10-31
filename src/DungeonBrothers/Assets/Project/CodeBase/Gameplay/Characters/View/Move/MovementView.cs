using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters.View.Animators;
using Project.CodeBase.Gameplay.Characters.View.Sounds;
using Project.CodeBase.Gameplay.Characters.View.SpriteFlip;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Gameplay.Tweeners.Move;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;
using VContainer;

namespace Project.CodeBase.Gameplay.Characters.View.Move
{
    public class MovementView : IMovementView
    {
        private readonly MoveTweener _moveTweener;
        private readonly CharacterSounds _characterSounds;
        private readonly ISpriteFlip _spriteFlip;
        private readonly ICharacterAnimator _animator;
        
        private MoveTweenerConfig _config;

        [Inject]
        public void Inject(IStaticDataProvider staticDataProvider)
        {
            _config = staticDataProvider.AllCharactersConfigs.MoveTweenerConfig;
        }

        public MovementView(MoveTweener moveTweener,
            CharacterSounds characterSounds,
            ISpriteFlip spriteFlip,
            ICharacterAnimator animator)
        {
            _moveTweener = moveTweener;
            _characterSounds = characterSounds;
            _spriteFlip = spriteFlip;
            _animator = animator;
        }

        public async UniTask Move(Vector2Int characterCoordinates, Tile destinationTile)
        {
            Vector2Int firstPathTileCoordinates = destinationTile.Logic.Coordinates;
            _spriteFlip.FlipToCoordinates(characterCoordinates, firstPathTileCoordinates);

            Vector3 tileWorldPosition = destinationTile.transform.position;

            await _moveTweener.Move(tileWorldPosition, _config.Speed, _config.Ease);
        }

        public void StartMovement()
        {
            _animator.PlayWalk();
            _characterSounds.PlaySoundInLoop(CharacterSoundType.Walk);
        }

        public void StopMovement()
        {
            _animator.PlayIdle();
            _characterSounds.StopPlaySound();
        }
    }
}