using Project.CodeBase.Gameplay.Tweeners.Scale;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.CodeBase.UI.Controls.Button
{
    public class SelectableButton : BaseButton
    {
        private ScaleTweener _scaleTweener;
        [SerializeField] private SelectableButtonAnimationConfig _config;

        protected override void OnEnable()
        {
            _scaleTweener = new ScaleTweener(transform);
            
            base.OnEnable();
            Events.PointerUpped
                .Subscribe(OnPointerUpped)
                .AddTo(Disposable);
            
            Events.PointerDowned
                .Subscribe(OnPointerDowned)
                .AddTo(Disposable);
            
            Events.PointerEntered
                .Subscribe(OnPointerEntered)
                .AddTo(Disposable);
            
            Events.PointerExited
                .Subscribe(OnPointerExited)
                .AddTo(Disposable);
        }

        private void OnPointerUpped(PointerEventData pointerEventData)
            => _scaleTweener.DoScaleWithoutReset(_config.ScaleTweenerOnUpped);

        private void OnPointerDowned(PointerEventData pointerEventData)
            => _scaleTweener.DoScaleWithoutReset(_config.ScaleTweenerOnDowned);

        private void OnPointerEntered(PointerEventData pointerEventData)
            => _scaleTweener.DoScaleWithoutReset(_config.ScaleTweenerOnEntered);

        private void OnPointerExited(PointerEventData pointerEventData)
            => _scaleTweener.DoScaleWithoutReset(_config.ScaleTweenerOnExited);
    }
}