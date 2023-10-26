using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Services.UIProvider
{
    public interface IUIProvider
    {
        public Canvas Canvas { get; }
        public EventSystem EventSystem { get; }
        
        public void SetCanvasToProvider(Canvas canvas);
        public void SetEventSystemToProvider(EventSystem eventSystem);
    }
}