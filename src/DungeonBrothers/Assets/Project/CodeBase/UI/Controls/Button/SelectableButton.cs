using Project.CodeBase.Gameplay.Animations.Scale;
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
            => _scaleAnimation.DoScale(_config.ScaleAnimationOnUpped);

        private void OnPointerDowned(PointerEventData pointerEventData)
            => _scaleAnimation.DoScale(_config.ScaleAnimationOnDowned);

        private void OnPointerEntered(PointerEventData pointerEventData)
            => _scaleAnimation.DoScale(_config.ScaleAnimationOnEntered);

        private void OnPointerExited(PointerEventData pointerEventData)
            => _scaleAnimation.DoScale(_config.ScaleAnimationOnExited);
    }
}