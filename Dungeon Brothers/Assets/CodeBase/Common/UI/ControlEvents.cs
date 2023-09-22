using System;
using UnityEngine.EventSystems;

namespace CodeBase.Common.UI
{
    public class ControlEvents : EventTrigger, IControlEvents
    {
        public event Action<PointerEventData> PointerEntered;
        public event Action<PointerEventData> PointerExited;
        public event Action<PointerEventData> PointerDowned;
        public event Action<PointerEventData> PointerUpped;
        public event Action<PointerEventData> PointerClicked;
        
        public override void OnPointerEnter(PointerEventData eventData) => PointerEntered?.Invoke(eventData);

        public override void OnPointerExit(PointerEventData eventData) => PointerExited?.Invoke(eventData);

        public override void OnPointerDown(PointerEventData eventData) => PointerDowned?.Invoke(eventData);

        public override void OnPointerUp(PointerEventData eventData) => PointerUpped?.Invoke(eventData);

        public override void OnPointerClick(PointerEventData eventData) => PointerClicked?.Invoke(eventData);
    }
}