using CodeBase.Infrastructure.Services.Providers.UIProvider;
using CodeBase.Infrastructure.Services.UnitsProvider;
using CodeBase.Infrastructure.StaticDataProviding;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services.Factories.TurnQueue
{
    public class TurnQueueViewFactory : ITurnQueueViewFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly IUIProvider _uiProvider;

        private GameObject _turnQueueViewPrefab;
        
        public TurnQueueViewFactory(IAddressablesLoader addressablesLoader,
            IObjectResolver objectResolver,
            IStaticDataProvider staticDataProvider,
            IUIProvider uiProvider)
        {
            _addressablesLoader = addressablesLoader;
            _objectResolver = objectResolver;
            _staticDataProvider = staticDataProvider;
            _uiProvider = uiProvider;
        }

        public async UniTask WarmUp()
        {
            await _addressablesLoader.LoadGameObject(_staticDataProvider.AssetsAddresses.AllUIAddresses
                .GameplayUIAddresses.TurnQueueView);
        }

        public async UniTask Create()
        {
            Canvas canvas = _uiProvider.Canvas.Value;
            Transform root = canvas.transform.root;
            
            if (!_turnQueueViewPrefab)
            {
                GameObject reference = await _addressablesLoader.LoadGameObject(_staticDataProvider.AssetsAddresses
                    .AllUIAddresses.GameplayUIAddresses.TurnQueueView);
                
                _turnQueueViewPrefab = _objectResolver.Instantiate(reference, root);
            }
        }
    }
}