using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Addressable.Loader;
using CodeBase.Infrastructure.Configs.Character;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.StaticDataProviding;

namespace CodeBase.UI.TurnQueue
{
    public class TurnQueueView : ITurnQueueView
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly ITurnQueueViewFactory _turnQueueViewFactory;
        private readonly AllCharactersConfigs _allCharactersConfigs;

        private readonly List<CharacterInTurnQueueIcon> _charactersIconsQueue = new();
        private ICharacter _currentCharacter;
        
        private const int MaxVisualizedIcons = 5;
        
        public TurnQueueView(ITurnQueue turnQueue,
            IAddressablesLoader addressablesLoader,
            ITurnQueueViewFactory turnQueueViewFactory,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _addressablesLoader = addressablesLoader;
            _turnQueueViewFactory = turnQueueViewFactory;
            _allCharactersConfigs = staticDataProvider.AllCharactersConfigs;
        }

        public void SubscribeToEvents()
        {
            _turnQueue.AddedToQueue += ReorganizeIcons;
            _turnQueue.NewTurnStarted += ReorganizeIcons;
        }
        
        public void UnSubscribeToEvents()
        {
            _turnQueue.AddedToQueue -= ReorganizeIcons;
            _turnQueue.NewTurnStarted -= ReorganizeIcons;
        }
        
        public async void ReorganizeIcons(ICharacter character)
        {
            ResetIcons();

            foreach (var currentCharacter in _turnQueue.Characters)
            {
                if (_allCharactersConfigs.CharacterConfigs.TryGetValue(currentCharacter.CharacterID,
                        out CharacterConfig characterConfig))
                {
                    CharacterInTurnQueueIcon characterInTurnQueueIcon =
                        await _turnQueueViewFactory.Create(characterConfig.Image, characterConfig.CharacterID);

                    characterInTurnQueueIcon.gameObject.SetActive(false);
                    _charactersIconsQueue.Add(characterInTurnQueueIcon);
                }
            }
            
            EnableIcons();
        }

        private void ResetIcons()
        {
            foreach (var icon in _charactersIconsQueue)
                icon.Destroy();
            
            _charactersIconsQueue.Clear();
        }
        
        private void EnableIcons()
        {
            if (_charactersIconsQueue.Count <= MaxVisualizedIcons)
            {
                for (int i = _charactersIconsQueue.Count - 1; i >= 0; i--)
                {
                    _charactersIconsQueue[i].gameObject.SetActive(true);
                }
                return;
            }

            int maxVisualizeIcons = _charactersIconsQueue.Count - 1 - MaxVisualizedIcons;
            
            for (int i = _charactersIconsQueue.Count - 1; i > maxVisualizeIcons; i--)
            {
                _charactersIconsQueue[i].gameObject.SetActive(true);
            }
        }
    }
}