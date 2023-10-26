using _Project.CodeBase.Gameplay.Animations.Colors;
using _Project.CodeBase.Gameplay.Animations.Scale;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.CodeBase.Gameplay.Animations.Hit
{
    public class HitAnimation 
    {
        private readonly ScaleAnimation _scaleAnimation;
        private readonly ColorAnimation _colorAnimation;

        private HitAnimationConfig _config;

        [Inject]
        public void Inject(IStaticDataProvider staticDataProvider)
        {
            _config = staticDataProvider.AllCharactersConfigs.HitAnimationConfig;
        }
        
        public HitAnimation(ScaleAnimation scaleAnimation,
            ColorAnimation colorAnimation)
        {
            _scaleAnimation = scaleAnimation;
            _colorAnimation = colorAnimation;
        }

        public async UniTask DoHit()
        {
            UniTask scaleAtHit = _scaleAnimation.DoScale(_config.ScaleAnimationAtHit).ToUniTask();
            UniTask colorAtHit = _colorAnimation.DoColor(_config.ColorAnimationAtHit).ToUniTask();
            
            await UniTask.WhenAll(scaleAtHit, colorAtHit);

            UniTask scaleAfterHit = _colorAnimation.DoColor(_config.ColorAnimationAfterHit).ToUniTask();
            UniTask colorAfterHit = _scaleAnimation.DoScale(_config.ScaleAnimationAfterHit).ToUniTask();
            
            await UniTask.WhenAll(scaleAfterHit, colorAfterHit);
        }
    }
}