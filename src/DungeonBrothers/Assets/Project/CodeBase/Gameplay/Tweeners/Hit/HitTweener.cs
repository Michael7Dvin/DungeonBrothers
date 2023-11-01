using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Tweeners.Color;
using Project.CodeBase.Gameplay.Tweeners.Scale;

namespace Project.CodeBase.Gameplay.Tweeners.Hit
{
    public class HitTweener
    {
        private readonly ScaleTweener _scaleTweener;
        private readonly ColorTweener _colorTweener;

        public HitTweener(ScaleTweener scaleTweener, 
            ColorTweener colorTweener)
        {
            _scaleTweener = scaleTweener;
            _colorTweener = colorTweener;
        }

        public async UniTask DoHit(HitTweenerConfig config)
        {
            ScaleTweenerConfig scaleConfig = config.ScaleTweenerAtHit;
            ColorTweenerConfig colorConfig = config.ColorTweenerAtHit;

            UniTask scaleAtHit = _scaleTweener.DoScaleWithReset(scaleConfig).ToUniTask();
            UniTask colorAtHit = _colorTweener.DoColorWithReset(colorConfig).ToUniTask();
            
            await UniTask.WhenAll(scaleAtHit, colorAtHit);
        }
    }
}