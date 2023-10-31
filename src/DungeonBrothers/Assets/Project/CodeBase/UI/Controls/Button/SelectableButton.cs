using Project.CodeBase.Gameplay.Tweeners.Scale;
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
            Events.PointerUpped += OnPointerUpped;
            Events.PointerDowned += OnPointerDowned;
            Events.PointerEntered += OnPointerEntered;
            Events.PointerExited += OnPointerExited;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Events.PointerUpped -= OnPointerUpped;
            Events.PointerDowned -= OnPointerDowned;
            Events.PointerEntered -= OnPointerEntered;
            Events.PointerExited -= OnPointerExited;
        }

        private void OnPointerUpped(PointerEventData pointerEventData)
            => _scaleAnimation.DoScaleWithoutReset(_config.ScaleAnimationOnUpped);

        private void OnPointerDowned(PointerEventData pointerEventData)
            => _scaleAnimation.DoScaleWithoutReset(_config.ScaleAnimationOnDowned);

        private void OnPointerEntered(PointerEventData pointerEventData)
            => _scaleAnimation.DoScaleWithoutReset(_config.ScaleAnimationOnEntered);

        private void OnPointerExited(PointerEventData pointerEventData)
            => _scaleAnimation.DoScaleWithoutReset(_config.ScaleAnimationOnExited);
        
    }
}