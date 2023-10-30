using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Animations.Movement;
using Project.CodeBase.Gameplay.Characters.View.Sounds;
using Project.CodeBase.Gameplay.Services.Map;
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

        private MovementAnimationConfig _config;

        [Inject]
        public void Inject(IStaticDataProvider staticDataProvider)
        {
            _config = staticDataProvider.AllCharactersConfigs.MovementAnimationConfig;
        }
        
        public MovementView(MovementAnimation movementAnimation, CharacterSounds characterSounds)
        {
            _movementAnimation = movementAnimation;
            _characterSounds = characterSounds;
        }

        public async UniTask Move(IEnumerable<Tile> tilesPath)
        {
            Vector3[] worldPositionsPath = CalculateWorldPositionsPath(tilesPath);
            
            _characterSounds.PlaySoundInLoop(CharacterSoundType.Walk);
            await _movementAnimation.Move(worldPositionsPath, _config.Speed, _config.Ease);
            _characterSounds.StopPlaySound();
        }
        
        private Vector3[] CalculateWorldPositionsPath(IEnumerable<Tile> tilesPath) => 
            tilesPath.Select(tile => tile.transform.position).ToArray();
    }
}