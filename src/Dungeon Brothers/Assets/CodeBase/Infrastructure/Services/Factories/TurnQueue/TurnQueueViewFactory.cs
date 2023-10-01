

using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using CodeBase.UI.Services.UIProvider;
using CodeBase.UI.TurnQueue;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services.Factories.TurnQueue
{
    public class TurnQueueViewFactory : ITurnQueueViewFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly AssetReferenceGameObject _turnQueueView;
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
            _turnQueueView = staticDataProvider.AssetsAddresses.AllUIAddresses.GameplayUIAddresses.TurnQueueView;
            _uiProvider = uiProvider;
        }

        public async UniTask WarmUp()
        {
            await _addressablesLoader.LoadGameObject(_turnQueueView);
        }


        public async UniTask CreateTurnQueueView()
        {
            Transform root = _uiProvider.Canvas.transform;
            
            await CreateTurnQueueViewPrefab(root);
                
            TurnQueueViewModel turnQueueViewModel = CreateTurnQueueViewModel();

            ConstructTurnQueueView(turnQueueViewModel);
        }

        public async UniTask<CharacterInTurnQueueIcon> CreateIcon(AssetReferenceGameObject iconReference,
            CharacterID characterID)
        {
            GameObject iconLoaded = await _addressablesLoader.LoadGameObject(iconReference);

            GameObject prefab = _objectResolver.Instantiate(iconLoaded, _turnQueueViewPrefab.transform);
            
            return ConstructCharacterInTurnQueueIcon(prefab, characterID);
        }

        private void ConstructTurnQueueView(TurnQueueViewModel turnQueuePresenter)
        {
            TurnQueueView turnQueueView = _turnQueueViewPrefab.AddComponent<TurnQueueView>();
            
            turnQueueView.Construct(turnQueuePresenter); 
        }
        
        private CharacterInTurnQueueIcon ConstructCharacterInTurnQueueIcon(GameObject prefab,
            CharacterID characterID)
        {
            CharacterInTurnQueueIcon icon = prefab.GetComponent<CharacterInTurnQueueIcon>();
            icon.Construct(characterID);
            return icon;
        }

        private TurnQueueViewModel CreateTurnQueueViewModel()
        {
            TurnQueueViewModel turnQueuePresenter = new TurnQueueViewModel();
            
            _objectResolver.Inject(turnQueuePresenter);

            return turnQueuePresenter;
        }

        private async UniTask CreateTurnQueueViewPrefab(Transform root)
        {
            GameObject reference = await _addressablesLoader.LoadGameObject(_turnQueueView);
                
            _turnQueueViewPrefab = _objectResolver.Instantiate(reference, root);
        }
    }
}