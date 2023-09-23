using CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Common;
using CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using CodeBase.UI.Services.UIProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services.Factories.UI
{
    public class CommonUIFactory : ICommonUIFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IUIProvider _uiProvider;

        private CommonUIAddresses _commonUIAddresses;

        public CommonUIFactory(IObjectResolver objectResolver,
            IAddressablesLoader addressablesLoader,
            IUIProvider uiProvider,
            IStaticDataProvider staticDataProvider)
        {
            _objectResolver = objectResolver;
            _addressablesLoader = addressablesLoader;
            _uiProvider = uiProvider;
            _commonUIAddresses = staticDataProvider.AssetsAddresses.AllUIAddresses.CommonUiAddresses;
        }

        public async UniTask WarmUp()
        {
            await _addressablesLoader.LoadGameObject(_commonUIAddresses.Canvas);
            await _addressablesLoader.LoadGameObject(_commonUIAddresses.EventSystem);
        }

        public async UniTask Create()
        {
            Canvas canvas = await CreateCanvas();
            _uiProvider.SetCanvasToProvider(canvas);

            EventSystem eventSystem = await CreateEventSystem();
            _uiProvider.SetEventSystemToProvider(eventSystem);
        }

        private async UniTask<Canvas> CreateCanvas()
        {
            Canvas canvas = await _addressablesLoader.LoadComponent<Canvas>(_commonUIAddresses.Canvas);
            
            return _objectResolver.Instantiate(canvas);
        }

        private async UniTask<EventSystem> CreateEventSystem()
        {
            EventSystem eventSystem = await _addressablesLoader.LoadComponent<EventSystem>(_commonUIAddresses.EventSystem);

            return _objectResolver.Instantiate(eventSystem);
        }
    }
}