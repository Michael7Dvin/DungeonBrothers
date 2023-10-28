using _Project.CodeBase.Gameplay.Animations.Hit;
using _Project.CodeBase.Gameplay.Characters.View.Sounds;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.CodeBase.Gameplay.Characters.View.Hit
{
    public class HitView : IHitView
    {
        private readonly HitAnimation _hitAnimation;
        private readonly CharacterSounds _characterSounds;
        
        private HitAnimationConfig _config;

        [Inject]
        public void Inject(IStaticDataProvider staticDataProvider)
        {
            _config = staticDataProvider.AllCharactersConfigs.HitAnimationConfig;
        }
        
        public HitView(HitAnimation hitAnimation,
            CharacterSounds characterSounds)
        {
            _hitAnimation = hitAnimation;
            _characterSounds = characterSounds;
        }
        
        public async UniTask TakeHit()
        {
            _characterSounds.PlaySoundOneTime(CharacterSoundType.Hit);
            await _hitAnimation.DoHit(_config);
        }
    }
}