using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Configs.Character;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.StaticDataProviding;
using Infrastructure.Services.ResourcesLoading;
using UnityEngine;

namespace CodeBase.Gameplay.UI.TurnQueue
{
    public class TurnQueueView : ITurnQueueView
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly ITurnQueueViewFactory _turnQueueViewFactory;
        private readonly AllCharactersConfigs _allCharactersConfigs;

        private readonly List<CharacterInTurnQueueIcon> _iconQueue = new();
        private ICharacter _currentCharacter;
        
        private const int MAXVISUALIZEICONS = 5;
        
        public TurnQueueView(ITurnQueue turnQueue,
            IAddressablesLoader addressablesLoader,
            ITurnQueueViewFactory turnQueueViewFactory,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _addressablesLoader = addressablesLoader;
            _turnQueueViewFactory = turnQueueViewFactory;
            _allCharactersConfigs = staticDataProvider.AllStaticData.AllCharactersConfigs;
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
        
        public async void ReOrganizeIcons(ICharacter character)
        {
            ResetIcons();

            foreach (var currentCharacter in _turnQueue.Characters)
            {
                foreach (var characterConfig in _allCharactersConfigs.CharacterConfigs)
                {
                    if (characterConfig.CharacterID == currentCharacter.CharacterID)
                    {
                        CharacterInTurnQueueIcon characterInTurnQueueIcon =
                            await _turnQueueViewFactory.Create(characterConfig.Image, characterConfig.CharacterID);

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
            if (_iconQueue.Count <= MAXVISUALIZEICONS)
            {
                for (int i = _iconQueue.Count - 1; i >= 0; i--)
                {
                    _iconQueue[i].gameObject.SetActive(true);
                }
                return;
            }

            int maxVisualizeIcons = _iconQueue.Count - 1 - MAXVISUALIZEICONS;
            
            for (int i = _iconQueue.Count - 1; i > maxVisualizeIcons; i--)
            {
                _iconQueue[i].gameObject.SetActive(true);
            }
        }
    }
}