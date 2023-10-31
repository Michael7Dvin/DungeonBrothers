using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Animations.Colors;
using Project.CodeBase.Gameplay.Animations.Scale;

namespace Project.CodeBase.Gameplay.Animations.Hit
{
    public class HitAnimation
    {
        private readonly ScaleAnimation _scaleAnimation;
        private readonly ColorAnimation _colorAnimation;

        public HitAnimation(ScaleAnimation scaleAnimation, 
            ColorAnimation colorAnimation)
        {
            _scaleAnimation = scaleAnimation;
            _colorAnimation = colorAnimation;
        }

        public async UniTask DoHit(HitAnimationConfig config)
        {
            UniTask scaleAtHit = _scaleAnimation.DoScale(config.ScaleAnimationAtHit).ToUniTask();
            UniTask colorAtHit = _colorAnimation.DoColor(config.ColorAnimationAtHit).ToUniTask();
            
            await UniTask.WhenAll(scaleAtHit, colorAtHit);

            UniTask scaleAfterHit = _colorAnimation.DoColor(config.ColorAnimationAfterHit).ToUniTask();
            UniTask colorAfterHit = _scaleAnimation.DoScale(config.ScaleAnimationAfterHit).ToUniTask();
            
            await UniTask.WhenAll(scaleAfterHit, colorAfterHit);
        }
    }
}