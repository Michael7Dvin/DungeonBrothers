using System;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Controls.Events
{
    public interface IControlEvents
    {
        public IObservable<PointerEventData> PointerEntered { get; }
        public IObservable<PointerEventData> PointerExited { get; }
        public IObservable<PointerEventData> PointerDowned { get; }
        public IObservable<PointerEventData> PointerUpped { get; }
        public IObservable<PointerEventData> PointerClicked { get; }
    }
}