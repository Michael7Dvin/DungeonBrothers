﻿using System;
using System.Collections.Generic;
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

        private readonly LinkedList<ICharacter> _characters = new();
        private LinkedListNode<ICharacter> _activeCharacterNode;

        public event Action<ICharacter, CharacterInTurnQueueIcon> AddedToQueue;
        public event Action Reseted;

        public TurnQueue(IRandomService randomService, 
            ICharactersProvider charactersProvider,
            ICustomLogger logger)
        {
            _randomService = randomService;
            _charactersProvider = charactersProvider;
            _logger = logger;
        }

        public IEnumerable<ICharacter> Characters => _characters;
        public ICharacter ActiveCharacter => _activeCharacterNode.Value;
        public event Action NewTurnStarted;

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
        }

        public void SetNextTurn()
        {
            if (_activeCharacterNode == _characters.First)
                _activeCharacterNode = _characters.Last;
            else
                _activeCharacterNode = _activeCharacterNode.Previous;


            NewTurnStarted?.Invoke();
        }
        
        public void SetFirstTurn() => _activeCharacterNode = _characters.Last;

        private void Add(ICharacter character,
            CharacterInTurnQueueIcon characterInTurnQueueIcon)
        {
            if (_characters.Count == 0)
            {
                _characters.AddFirst(character);
                
                AddedToQueue?.Invoke(character, characterInTurnQueueIcon);
                return;
            }

            int newCharacterInitiative = character.CharacterStats.Initiative;
            
            LinkedListNode<ICharacter> currentCharacter = _characters.First;

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

        private void Remove(ICharacter character)
        {
            if (character == _activeCharacterNode.Value)
                _logger.LogError(new Exception($"Unable to remove {nameof(ActiveCharacter)}. Feature not implemented"));

            _characters.Remove(character);
        }
    }
}