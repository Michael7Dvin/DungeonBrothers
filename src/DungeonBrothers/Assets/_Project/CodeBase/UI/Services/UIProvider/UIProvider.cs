using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Services.UIProvider
{
    public class UIProvider : IUIProvider
    {
        public Canvas Canvas { get; private set; }
        public EventSystem EventSystem { get; private set; }
        
        public void SetCanvasToProvider(Canvas canvas) => Canvas = canvas;
        public void SetEventSystemToProvider(EventSystem eventSystem) => EventSystem = eventSystem;
    }
}