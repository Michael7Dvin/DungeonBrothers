using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters.View.Sounds;
using Project.CodeBase.Gameplay.Tweeners.Hit;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using VContainer;

namespace Project.CodeBase.Gameplay.Characters.View.Hit
{
    public class HitView : IHitView
    {
        private readonly HitTweener _hitTweener;
        private readonly CharacterSounds _characterSounds;
        
        private HitTweenerConfig _config;

        [Inject]
        public void Inject(IStaticDataProvider staticDataProvider)
        {
            _config = staticDataProvider.AllCharactersConfigs.HitTweenerConfig;
        }
        
        public HitView(HitTweener hitTweener,
            CharacterSounds characterSounds)
        {
            _hitTweener = hitTweener;
            _characterSounds = characterSounds;
        }
        
        public async UniTask TakeHit()
        {
            _characterSounds.PlaySoundOneTime(CharacterSoundType.Hit);
            await _hitTweener.DoHit(_config);
        }
    }
}