using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using Project.CodeBase.UI.Services.UIProvider;
using Project.CodeBase.UI.TurnQueue;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Project.CodeBase.Infrastructure.Services.Factories.TurnQueue
{
    public class TurnQueueViewFactory : ITurnQueueViewFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly AssetReferenceGameObject _turnQueueView;
        private readonly IObjectResolver _objectResolver;
        private readonly IUIProvider _uiProvider;

        private GameObject _turnQueueViewGameObject;

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

        public async UniTask<CharacterTurnQueueIcon> CreateIcon(AssetReferenceGameObject iconReference,
            CharacterID characterID)
        {
            GameObject iconLoaded = await _addressablesLoader.LoadGameObject(iconReference);

            GameObject gameObject = _objectResolver.Instantiate(iconLoaded, _turnQueueViewGameObject.transform);
            
            return ConstructCharacterInTurnQueueIcon(gameObject, characterID);
        }

        private void ConstructTurnQueueView(TurnQueueViewModel turnQueuePresenter)
        {
            TurnQueueView turnQueueView = _turnQueueViewGameObject.GetComponent<TurnQueueView>();
            turnQueueView.Construct(turnQueuePresenter); 
        }
        
        private CharacterTurnQueueIcon ConstructCharacterInTurnQueueIcon(GameObject prefab, CharacterID characterID)
        {
            CharacterTurnQueueIcon icon = prefab.GetComponent<CharacterTurnQueueIcon>();
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
                
            _turnQueueViewGameObject = _objectResolver.Instantiate(reference, root);
        }
    }
}