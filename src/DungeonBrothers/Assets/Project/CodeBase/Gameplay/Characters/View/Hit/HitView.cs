using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Animations.Hit;
using Project.CodeBase.Gameplay.Characters.View.Sounds;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using VContainer;

namespace Project.CodeBase.Gameplay.Characters.View.Hit
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