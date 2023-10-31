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
            ScaleAnimationConfig scaleConfig = config.ScaleAnimationAtHit;
            ColorAnimationConfig colorConfig = config.ColorAnimationAtHit;

            UniTask scaleAtHit = _scaleAnimation.DoScaleWithReset(scaleConfig).ToUniTask();
            UniTask colorAtHit = _colorAnimation.DoColorWithReset(colorConfig).ToUniTask();
            
            await UniTask.WhenAll(scaleAtHit, colorAtHit);
        }
    }
}