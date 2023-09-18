using CodeBase.Common.Observables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Infrastructure.Services.Providers.UIProvider
{
    public class UIProvider : IUIProvider
    {
        private Observable<Canvas> _canvas;
        private Observable<EventSystem> _eventSystem;

        public IReadOnlyObservable<Canvas> Canvas => _canvas;
        public IReadOnlyObservable<EventSystem> EventSystem => _eventSystem;

        public void SetCanvasToProvider(Canvas canvas) => _canvas.Value = canvas;
        public void SetEventSystemToProvider(EventSystem eventSystem) => _eventSystem.Value = eventSystem;
    }
}