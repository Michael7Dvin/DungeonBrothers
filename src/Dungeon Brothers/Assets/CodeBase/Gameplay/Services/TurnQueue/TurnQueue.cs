﻿using System;
using System.Collections.Generic;
using CodeBase.Common.Observables;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Random;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public class TurnQueue : ITurnQueue
    {
        private readonly IRandomService _randomService;
        private readonly ICharactersProvider _charactersProvider;
        private readonly ICustomLogger _logger;

        private readonly LinkedList<Character> _characters = new();
        private LinkedListNode<Character> _activeCharacterNode;

        private readonly Observable<Character> _activeCharacter = new();
        
        public IReadOnlyObservable<Character> ActiveCharacter => _activeCharacter;
        public event Action<Character, CharacterInTurnQueueIcon> AddedToQueue;
        public event Action Reseted;
        public event Action<Character> NewTurnStarted;
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
            _charactersProvider.Spawned += Add;
            _charactersProvider.Died += Remove;
        }
        
        public void CleanUp()
        {
            _charactersProvider.Spawned -= Add;
            _charactersProvider.Died -= Remove;
            
            Reseted?.Invoke();
            
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
            
            NewTurnStarted?.Invoke(_activeCharacterNode.Value);
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
                
                AddedToQueue?.Invoke(character, characterInTurnQueueIcon);
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
                        AddedToQueue?.Invoke(character, characterInTurnQueueIcon);
                        return;
                    }
                }

                if (newCharacterInitiative < currentCharacterInitiative)
                {
                    _characters.AddBefore(currentCharacter, character);
                    AddedToQueue?.Invoke(character, characterInTurnQueueIcon);
                    return;
                }

                if (currentCharacter == _characters.Last)
                {
                    _characters.AddLast(character);
                    AddedToQueue?.Invoke(character, characterInTurnQueueIcon);
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