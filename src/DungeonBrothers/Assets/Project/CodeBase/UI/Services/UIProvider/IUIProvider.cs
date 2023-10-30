using UnityEngine;

namespace Project.CodeBase.UI.Services.UIProvider
{
    public interface IUIProvider
    {
        public Canvas Canvas { get; }
        public void SetCanvasToProvider(Canvas canvas);
    }
}