using Cysharp.Threading.Tasks;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Common;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using Project.CodeBase.UI.Services.UIProvider;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.CodeBase.Infrastructure.Services.Factories.UI
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
        }

        public async UniTask Create()
        {
            Canvas canvas = await CreateCanvas();
            _uiProvider.SetCanvasToProvider(canvas);
        }

        private async UniTask<Canvas> CreateCanvas()
        {
            Canvas canvas = await _addressablesLoader.LoadComponent<Canvas>(_commonUIAddresses.Canvas);
            
            return _objectResolver.Instantiate(canvas);
        }
    }
}