using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Random;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using CodeBase.UI.TurnQueue;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public class TurnQueue : ITurnQueue
    {
        private readonly IRandomService _randomService;
        private readonly ICharactersProvider _charactersProvider;
        private readonly ICustomLogger _logger;

        private readonly CompositeDisposable _disposable = new();
        
        private readonly LinkedList<Character> _characters = new();
        private LinkedListNode<Character> _activeCharacterNode;

        private readonly ReactiveProperty<Character> _activeCharacter = new();
        
        private readonly ReactiveCommand<(Character, CharacterInTurnQueueIcon)> _addedToQueue = new();
        private readonly ReactiveCommand _reseted = new();
        private readonly ReactiveCommand<Character> _newTurnStarted = new();

        public IObservable<(Character, CharacterInTurnQueueIcon)> AddedToQueue => _addedToQueue;
        public IObservable<Unit> Reseted => _reseted;
        public IObservable<Character> NewTurnStarted => _newTurnStarted;
        
        public IReadOnlyReactiveProperty<Character> ActiveCharacter => _activeCharacter;
        public IEnumerable<Character> Characters => _characters;
        
        public TurnQueue(IRandomService randomService, 
            ICharactersProvider charactersProvider,
            ICustomLogger logger)
        {
            _randomService = randomService;
            _charactersProvider = charactersProvider;
            _logger = logger;
        }
        
        public void Initialize()
        {
            _charactersProvider.Spawned
                .Subscribe(character => Add(character.Item1, character.Item2))
                .AddTo(_disposable);


            _charactersProvider.Died
                .Subscribe(Remove)
                .AddTo(_disposable);
        }
        
        public void CleanUp()
        {
            _disposable.Clear();

            _reseted.Execute();
            
            _characters.Clear();
            _activeCharacterNode = null;
            UpdateActiveCharacter();
        }

        public void SetNextTurn()
        {
            if (_activeCharacterNode == _characters.First)
            {
                _activeCharacterNode = _characters.Last;
                UpdateActiveCharacter();
            }
            else
            {
                _activeCharacterNode = _activeCharacterNode.Previous;
                UpdateActiveCharacter();
            }
            
            Debug.Log(_activeCharacter.Value);
            
            _newTurnStarted.Execute(_activeCharacterNode.Value);
        }

        public void SetFirstTurn()
        {
            _activeCharacterNode = _characters.Last;
            UpdateActiveCharacter();
        }

        private void UpdateActiveCharacter() =>
            _activeCharacter.Value = _activeCharacterNode.Value;
        
        private void Add(Character character,
            CharacterInTurnQueueIcon characterInTurnQueueIcon)
        {
            if (_characters.Count == 0)
            {
                _characters.AddFirst(character);
                
                _addedToQueue.Execute((character, characterInTurnQueueIcon));
                return;
            }

            int newCharacterInitiative = character.CharacterStats.Initiative;
            
            LinkedListNode<Character> currentCharacter = _characters.First;

            while (currentCharacter != null)
            {
                int currentCharacterInitiative = currentCharacter.Value.CharacterStats.Initiative;

                if (newCharacterInitiative == currentCharacterInitiative)
                {
                    if (_randomService.DoFiftyFifty())
                    {
                        _characters.AddBefore(currentCharacter, character);
                        _addedToQueue.Execute((character, characterInTurnQueueIcon));
                        return;
                    }
                }

                if (newCharacterInitiative < currentCharacterInitiative)
                {
                    _characters.AddBefore(currentCharacter, character);
                    _addedToQueue.Execute((character, characterInTurnQueueIcon));
                    return;
                }

                if (currentCharacter == _characters.Last)
                {
                    _characters.AddLast(character);
                    _addedToQueue.Execute((character, characterInTurnQueueIcon));
                    return;
                }

                currentCharacter = currentCharacter.Next;
            }
        }

        private void Remove(Character character)
        {
            if (character == _activeCharacterNode.Value)
                _logger.LogError(new Exception($"Unable to remove {nameof(ActiveCharacter)}. Feature not implemented"));

            _characters.Remove(character);
        }
    }
}