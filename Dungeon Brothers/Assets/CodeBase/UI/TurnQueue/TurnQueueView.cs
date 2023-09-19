using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Configs.Character;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Gameplay.UI.TurnQueue
{
    public class TurnQueueView : ITurnQueueView
    {
        private readonly ITurnQueue _turnQueue;
        private readonly ITurnQueueViewFactory _turnQueueViewFactory;
        private readonly AllCharactersConfigs _allCharactersConfigs;

        private List<CharacterInTurnQueueIcon> _charactersIconsQueue = new();
        private ICharacter _currentCharacter;
        
        private const int MaxVisualizedIcons = 5;
        
        public TurnQueueView(ITurnQueue turnQueue,
            ITurnQueueViewFactory turnQueueViewFactory,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _turnQueueViewFactory = turnQueueViewFactory;
            _allCharactersConfigs = staticDataProvider.AllCharactersConfigs;
        }

        public void SubscribeToEvents()
        {
            _turnQueue.AddedToQueue += ReorganizeIcons;
            _turnQueue.NewTurnStarted += ShiftIcons;
        }
        
        public void UnSubscribeToEvents()
        {
            _turnQueue.AddedToQueue -= ReorganizeIcons;
            _turnQueue.NewTurnStarted -= ShiftIcons;
        }
        
        public async void ReorganizeIcons(ICharacter character)
        {
            if (_allCharactersConfigs.CharacterConfigs.TryGetValue(character.CharacterID,
                    out CharacterConfig characterConfig))
            {
                CharacterInTurnQueueIcon characterInTurnQueueIcon =
                    await _turnQueueViewFactory.Create(characterConfig.Image, characterConfig.CharacterID);

                characterInTurnQueueIcon.gameObject.SetActive(false);

                for (int i = 0; i < _turnQueue.Characters.Count(); i++)
                {
                    List<ICharacter> characters = _turnQueue.Characters.ToList();

                    if (characters[i] == character)
                    {
                        int positionInList = i;
                        
                        _charactersIconsQueue.Insert(positionInList, characterInTurnQueueIcon);
                        EnableIcons();
                        ReorganizeChildPosition();
                    }
                }
            }
        }

        private void ResetIcons()
        {
            foreach (var icon in _charactersIconsQueue)
                icon.Destroy();
            
            _charactersIconsQueue.Clear();
        }

        private void DisableIcons()
        {
            foreach (var icon in _charactersIconsQueue) 
                icon.gameObject.SetActive(false);
        }

        private void ShiftIcons()
        {
            if (_charactersIconsQueue != null)
            {
                DisableIcons();
                
                int shift = 1;
                
                _charactersIconsQueue = _charactersIconsQueue
                    .Skip(_charactersIconsQueue.Count - shift)
                    .Take(shift)
                    .Concat(_charactersIconsQueue
                        .Take(_charactersIconsQueue.Count - shift))
                    .ToList();

                ReorganizeChildPosition();

                EnableIcons();
            }
        }

        private void ReorganizeChildPosition()
        {
            for (int i = 0; i < _charactersIconsQueue.Count; i++)
                _charactersIconsQueue[i].transform.SetSiblingIndex(i);
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