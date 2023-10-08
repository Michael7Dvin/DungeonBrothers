using CodeBase.Gameplay.Animations.Color;
using CodeBase.Gameplay.Animations.Scale;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Gameplay.Animations.Hit
{
    public class HitAnimation 
    {
        private readonly ScaleAnimation _scaleAnimation;
        private readonly ColorAnimation _colorAnimation;

        private HitAnimationConfig _config;

        [Inject]
        public void Inject(IStaticDataProvider staticDataProvider)
        {
            _config = staticDataProvider.AllCharactersConfigs.hitAnimationConfig;
        }
        
        public HitAnimation(ScaleAnimation scaleAnimation,
            ColorAnimation colorAnimation)
        {
            _scaleAnimation = scaleAnimation;
            _colorAnimation = colorAnimation;
        }

        public async UniTask TakeHitAnimate()
        {
            _colorAnimation.DoColor(_config.ColorInHit);
            await _scaleAnimation.ScaleUniTask(_config.ScaleInHit);
            
            _colorAnimation.DoColor(_config.ColorOutHit);
            await _scaleAnimation.ScaleUniTask(_config.ScaleOutHit);
        }
    }
}