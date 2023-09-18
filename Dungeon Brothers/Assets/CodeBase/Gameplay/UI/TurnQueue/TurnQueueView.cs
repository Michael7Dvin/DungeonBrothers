using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Configs.Character;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
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
        private readonly ITurnQueueViewFactory _turnQueueViewFactory;
        private readonly AllCharactersConfigs _allCharactersConfigs;

        private List<CharacterInTurnQueueIcon> _iconQueue;
        private ICharacter _currentCharacter;
        
        private const int MAXVISUALIZEICONS = 5;
        
        public TurnQueueView(ITurnQueue turnQueue,
            AddressablesLoader addressablesLoader,
            ITurnQueueViewFactory turnQueueViewFactory,
            StaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _addressablesLoader = addressablesLoader;
            _turnQueueViewFactory = turnQueueViewFactory;
            _allCharactersConfigs = staticDataProvider.AllCharactersConfigs;
        }

        public void SubscribeToEvents()
        {
            _turnQueue.AddedToQueue += ReOrganizeIcons;
            _turnQueue.NewTurnStarted += ReOrganizeIcons;
        }
        
        public void UnSubscribeToEvents()
        {
            _turnQueue.AddedToQueue -= ReOrganizeIcons;
            _turnQueue.NewTurnStarted -= ReOrganizeIcons;
        }
        
        private async void ReOrganizeIcons(ICharacter character)
        {
            ResetIcons();

            foreach (var currentCharacter in _turnQueue.Characters)
            {
                foreach (var characterConfig in _allCharactersConfigs.CharacterConfigs)
                {
                    if (characterConfig.CharacterID == currentCharacter.CharacterID)
                    {
                        CharacterInTurnQueueIcon characterInTurnQueueIcon =
                            await _turnQueueViewFactory.Create(characterConfig.Image);

                        characterInTurnQueueIcon.gameObject.SetActive(false);
                        _iconQueue.Add(characterInTurnQueueIcon);
                    }
                }
            }
            
            VisualizeIcons();
        }

        private void ResetIcons()
        {
            foreach (var icon in _iconQueue) icon.Destroy();
            
            _iconQueue.Clear();
        }
        
        private void VisualizeIcons()
        {
            for (int i = _iconQueue.Count; i-MAXVISUALIZEICONS < i; i--)
            {
                _iconQueue[i].gameObject.SetActive(true);
            }
        }
    }
}