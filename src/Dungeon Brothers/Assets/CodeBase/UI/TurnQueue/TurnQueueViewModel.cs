using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.UI.TurnQueue
{
    public class TurnQueueViewModel
    {
        private ITurnQueue _turnQueue;
        private readonly CompositeDisposable _disposable = new();

        private List<CharacterInTurnQueueIcon> _charactersIconsQueue = new();
        private IReadOnlyList<CharacterInTurnQueueIcon> CharacterIconsQueue => _charactersIconsQueue;

        private readonly ReactiveCommand<IReadOnlyList<CharacterInTurnQueueIcon>> _listChanged = new();
        private readonly ReactiveCommand<IReadOnlyList<CharacterInTurnQueueIcon>> _disableIcons = new();
        private readonly ReactiveCommand<IReadOnlyList<CharacterInTurnQueueIcon>> _enableIcons = new();

        private ICharacter _currentCharacter;
        
        public IObservable<IReadOnlyList<CharacterInTurnQueueIcon>> ListChanged => _listChanged;
        public IObservable<IReadOnlyList<CharacterInTurnQueueIcon>> DisableIcons => _disableIcons;
        public IObservable<IReadOnlyList<CharacterInTurnQueueIcon>> EnableIcons => _enableIcons;

        [Inject]
        public void Inject(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }
        
        public void OnEnable()
        {
            _turnQueue.AddedToQueue
                .Subscribe(character => ReorganizeIcons(character.Item1, character.Item2))
                .AddTo(_disposable);

            _turnQueue.NewTurnStarted
                .Subscribe(character =>
                {
                    if (_charactersIconsQueue != null)
                        ShiftIcons(character);
                })
                .AddTo(_disposable);

            _turnQueue.Reseted
                .Subscribe(unit => ClearIcons())
                .AddTo(_disposable);
        }
        
        public void OnDisable() => 
            _disposable.Clear();

        public void ClearIcons()
        {
            foreach (var icon in _charactersIconsQueue)
                icon.Destroy();
            
            _charactersIconsQueue.Clear();
        }
        
        private void ReorganizeIcons(ICharacter character, CharacterInTurnQueueIcon characterInTurnQueueIcon)
        {
            for (int i = 0; i < _turnQueue.Characters.Count(); i++)
            {
                List<ICharacter> characters = _turnQueue.Characters.ToList();

                if (characters[i] == character)
                {
                    int positionInList = i;
                        
                    _charactersIconsQueue.Insert(positionInList, characterInTurnQueueIcon);
                    
                    _listChanged.Execute(CharacterIconsQueue);
                    _enableIcons.Execute(CharacterIconsQueue); 
                }
            }
        }
        
        private void ShiftIcons(ICharacter character)
        {
            _disableIcons.Execute(CharacterIconsQueue);
                
            int shift = 1;
                
            _charactersIconsQueue = _charactersIconsQueue
                .Skip(_charactersIconsQueue.Count - shift)
                .Take(shift)
                .Concat(_charactersIconsQueue
                    .Take(_charactersIconsQueue.Count - shift))
                .ToList();
                
            _listChanged.Execute(CharacterIconsQueue);
            _enableIcons.Execute(CharacterIconsQueue); 
            
        }
    }
}