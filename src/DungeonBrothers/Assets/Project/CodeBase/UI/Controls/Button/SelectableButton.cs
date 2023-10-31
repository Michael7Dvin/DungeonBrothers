using _Project.CodeBase.Gameplay.Animations.Scale;
using _Project.CodeBase.UI.Controls.Button;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.CodeBase.UI.Controls.Button
{
    public class SelectableButton : BaseButton
    {
        private ScaleAnimation _scaleAnimation;
        [SerializeField] private SelectableButtonAnimationConfig _config;

        protected override void OnEnable()
        {
            _scaleAnimation = new ScaleAnimation(transform);
            
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
            => _scaleAnimation.DoScale(_config.ScaleAnimationOnUpped);

        private void OnPointerDowned(PointerEventData pointerEventData)
            => _scaleAnimation.DoScale(_config.ScaleAnimationOnDowned);

        private void OnPointerEntered(PointerEventData pointerEventData)
            => _scaleAnimation.DoScale(_config.ScaleAnimationOnEntered);

        private void OnPointerExited(PointerEventData pointerEventData)
            => _scaleAnimation.DoScale(_config.ScaleAnimationOnExited);
    }
}