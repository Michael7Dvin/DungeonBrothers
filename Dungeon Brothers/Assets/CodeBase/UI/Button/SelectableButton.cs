using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Button
{
    public class SelectableButton : BaseButton
    {
        [SerializeField] private SelectableButtonAnimation selectableButtonAnimation;
        
        protected override void OnEnable()
        {
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
            => selectableButtonAnimation.ScaleOnPointerUpped();

        private void OnPointerDowned(PointerEventData pointerEventData)
            => selectableButtonAnimation.ScaleOnPointerDowned();

        private void OnPointerEntered(PointerEventData pointerEventData)
            => selectableButtonAnimation.ScaleOnPointerEntered();

        private void OnPointerExited(PointerEventData pointerEventData)
            => selectableButtonAnimation.ScaleOnPointerExited();
    }
}