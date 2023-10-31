using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Tweeners.Color;
using Project.CodeBase.Gameplay.Tweeners.Scale;

namespace Project.CodeBase.Gameplay.Tweeners.Hit
{
    public class HitTweener
    {
        private readonly ScaleAnimation _scaleAnimation;
        private readonly ColorTweener _colorTweener;

        public HitTweener(ScaleAnimation scaleAnimation, 
            ColorTweener colorTweener)
        {
            _scaleAnimation = scaleAnimation;
            _colorTweener = colorTweener;
        }

        public async UniTask DoHit(HitTweenerConfig config)
        {
            ScaleAnimationConfig scaleConfig = config.ScaleAnimationAtHit;
            ColorTweenerConfig colorConfig = config.ColorTweenerAtHit;

            UniTask scaleAtHit = _scaleAnimation.DoScaleWithReset(scaleConfig).ToUniTask();
            UniTask colorAtHit = _colorTweener.DoColorWithReset(colorConfig).ToUniTask();
            
            await UniTask.WhenAll(scaleAtHit, colorAtHit);
        }
    }
}