using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using VContainer;

namespace CodeBase.UI.TurnQueue
{
    public class TurnQueueViewModel
    {
        private ITurnQueue _turnQueue;

        private List<CharacterInTurnQueueIcon> _charactersIconsQueue = new();
        private IReadOnlyList<CharacterInTurnQueueIcon> CharacterIconsQueue => _charactersIconsQueue;

        public event Action<IReadOnlyList<CharacterInTurnQueueIcon>> ListChanged;
        public event Action<IReadOnlyList<CharacterInTurnQueueIcon>> DisableIcons;
        public event Action<IReadOnlyList<CharacterInTurnQueueIcon>> EnableIcons;
        
        private ICharacter _currentCharacter;
        
        [Inject]
        public void Inject(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }
        
        public void OnEnable()
        {
            _turnQueue.AddedToQueue += ReorganizeIcons;
            _turnQueue.NewTurnStarted += ShiftIcons;
            _turnQueue.Reseted += ClearIcons;
        }
        
        public void OnDisable()
        {
            _turnQueue.Reseted -= ClearIcons;
            _turnQueue.AddedToQueue -= ReorganizeIcons;
            _turnQueue.NewTurnStarted -= ShiftIcons;
        }

        public void ClearIcons()
        {
            foreach (var icon in _charactersIconsQueue)
                icon.Destroy();
            
            _charactersIconsQueue.Clear();
        }
        
        private void ReorganizeIcons(Character character, CharacterInTurnQueueIcon characterInTurnQueueIcon)
        {
            for (int i = 0; i < _turnQueue.Characters.Count(); i++)
            {
                List<Character> characters = _turnQueue.Characters.ToList();

                if (characters[i] == character)
                {
                    int positionInList = i;
                        
                    _charactersIconsQueue.Insert(positionInList, characterInTurnQueueIcon);
                    ListChanged?.Invoke(CharacterIconsQueue);
                    EnableIcons?.Invoke(CharacterIconsQueue); 
                }
            }
        }
        
        private void ShiftIcons()
        {
            if (_charactersIconsQueue != null)
            {
                DisableIcons?.Invoke(CharacterIconsQueue);
                
                int shift = 1;
                
                _charactersIconsQueue = _charactersIconsQueue
                    .Skip(_charactersIconsQueue.Count - shift)
                    .Take(shift)
                    .Concat(_charactersIconsQueue
                        .Take(_charactersIconsQueue.Count - shift))
                    .ToList();
                
                ListChanged?.Invoke(CharacterIconsQueue);
                EnableIcons?.Invoke(CharacterIconsQueue);
            }
        }
    }
}