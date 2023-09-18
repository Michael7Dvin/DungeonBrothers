using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Configs.Character;
using CodeBase.Infrastructure.Services.UnitsProvider;
using CodeBase.Infrastructure.StaticDataProviding;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.ResourcesLoading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Gameplay.UI.TurnQueue
{
    public class TurnQueueView
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IObjectResolver _objectResolver;
        private readonly AllCharactersConfigs _allCharactersConfigs;

        private ICharacter _currentCharacter;
        
        public TurnQueueView(ITurnQueue turnQueue,
            AddressablesLoader addressablesLoader,
            IObjectResolver objectResolver,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _addressablesLoader = addressablesLoader;
            _objectResolver = objectResolver;
            _allCharactersConfigs = staticDataProvider.AllCharactersConfigs;
        }

        public async UniTask VisualizeIcons()
        {
            if (_currentCharacter == null)
            {
                _currentCharacter = _turnQueue.Characters.Last();
            }

            foreach (var characterConfig in _allCharactersConfigs.CharacterConfigs)
            {
                if (characterConfig.CharacterID == _currentCharacter.CharacterID)
                {
                    await LoadImage(characterConfig.Image);
                }
            }
        }

        private async UniTask LoadImage(AssetReferenceGameObject imageReference)
        {
            GameObject imageLoaded = await _addressablesLoader.LoadGameObject(imageReference);

            GameObject imagePrefab = _objectResolver.Instantiate(imageLoaded);
        }
    }
}