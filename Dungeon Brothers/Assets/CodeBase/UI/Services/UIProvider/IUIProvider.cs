using CodeBase.Common.Observables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Services.UIProvider
{
    public interface IUIProvider
    {
        public IReadOnlyObservable<Canvas> Canvas { get; }
        public IReadOnlyObservable<EventSystem> EventSystem { get; }
        
        public void SetCanvasToProvider(Canvas canvas);
        public void SetEventSystemToProvider(EventSystem eventSystem);
    }
}