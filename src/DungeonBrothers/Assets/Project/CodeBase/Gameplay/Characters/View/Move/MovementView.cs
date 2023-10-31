using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Animations.Movement;
using Project.CodeBase.Gameplay.Characters.View.Sounds;
using Project.CodeBase.Gameplay.Characters.View.SpriteFlip;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;
using VContainer;

namespace Project.CodeBase.Gameplay.Characters.View.Move
{
    public class MovementView : IMovementView
    {
        private readonly MovementAnimation _movementAnimation;
        private readonly CharacterSounds _characterSounds;
        private readonly ISpriteFlip _spriteFlip;

        private MovementAnimationConfig _config;

        [Inject]
        public void Inject(IStaticDataProvider staticDataProvider)
        {
            _config = staticDataProvider.AllCharactersConfigs.MovementAnimationConfig;
        }

        public MovementView(MovementAnimation movementAnimation,
            CharacterSounds characterSounds,
            ISpriteFlip spriteFlip)
        {
            _movementAnimation = movementAnimation;
            _characterSounds = characterSounds;
            _spriteFlip = spriteFlip;
        }

        public async UniTask Move(Vector2Int characterCoordinates, Tile destinationTile)
        {
            Vector2Int firstPathTileCoordinates = destinationTile.Logic.Coordinates;
            _spriteFlip.FlipToCoordinates(characterCoordinates, firstPathTileCoordinates);

            Vector3 tileWorldPosition = destinationTile.transform.position;

            await _movementAnimation.Move(tileWorldPosition, _config.Speed, _config.Ease);
        }

        public void StopMovement() => 
            _characterSounds.StopPlaySound();

        public void StartMovement() => 
            _characterSounds.PlaySoundInLoop(CharacterSoundType.Walk);
    }
}