using System;
using UniRx;
using UnityEngine.EventSystems;

namespace Project.CodeBase.UI.Controls.Events
{
    public class ControlEvents : EventTrigger, IControlEvents
    {
        private readonly ReactiveCommand<PointerEventData> _pointerEntered = new();
        private readonly ReactiveCommand<PointerEventData> _pointerExited = new();
        private readonly ReactiveCommand<PointerEventData> _pointerDowned = new();
        private readonly ReactiveCommand<PointerEventData> _pointerUpped = new();
        private readonly ReactiveCommand<PointerEventData> _pointerClicked = new();

        public IObservable<PointerEventData> PointerEntered => _pointerEntered;
        public IObservable<PointerEventData> PointerExited => _pointerExited;
        public IObservable<PointerEventData> PointerDowned => _pointerDowned;
        public IObservable<PointerEventData> PointerUpped => _pointerUpped;
        public IObservable<PointerEventData> PointerClicked => _pointerClicked;

        public override void OnPointerEnter(PointerEventData eventData) => _pointerEntered.Execute(eventData);

        public override void OnPointerExit(PointerEventData eventData) => _pointerExited.Execute(eventData);

        public override void OnPointerDown(PointerEventData eventData) => _pointerDowned.Execute(eventData);

        public override void OnPointerUp(PointerEventData eventData) => _pointerUpped.Execute(eventData);

        public override void OnPointerClick(PointerEventData eventData) => _pointerClicked.Execute(eventData);
    }
}