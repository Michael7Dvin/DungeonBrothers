using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Randomise;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.UnitsProvider;
using UnityEngine;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public class TurnQueue : ITurnQueue
    {
        private readonly IRandomService _randomService;
        private readonly ICharactersProvider _charactersProvider;
        private readonly ICustomLogger _logger;

        private readonly LinkedList<ICharacter> _characters = new();
        private LinkedListNode<ICharacter> _activeCharacterNode;

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
        public event Action<ICharacter> NewTurnStarted;

        public void Initialize()
        {
            _charactersProvider.Spawned += Add;
            _charactersProvider.Died += Remove;
        }
        
        public void CleanUp()
        {
            _charactersProvider.Spawned -= Add;
            _charactersProvider.Died -= Remove;
            
            _characters.Clear();
            _activeCharacterNode = null;
        }

        public void SetNextTurn()
        {
            if (_activeCharacterNode == _characters.First)
                _activeCharacterNode = _characters.Last;
            else
                _activeCharacterNode = _activeCharacterNode.Previous;
            
            NewTurnStarted?.Invoke(ActiveCharacter);
        }
        
        public void SetFirstTurn()
        {
            _activeCharacterNode = _characters.Last;
            NewTurnStarted?.Invoke(ActiveCharacter);
        }

        private void Add(ICharacter character)
        {
            if (_characters.Count == 0)
            {
                _characters.AddFirst(character);
                return;
            }

            int newCharacterInitiative = character.CharacterStats.Initiative;
            
            LinkedListNode<ICharacter> currentCharacter = _characters.First;

            while (currentCharacter != null)
            {
                int currentCharacterInitiative = currentCharacter.Value.CharacterStats.Initiative;

                if (newCharacterInitiative == currentCharacterInitiative)
                {
                    CompareByStats(currentCharacter, character);
                    return;
                }

                if (newCharacterInitiative < currentCharacterInitiative)
                {
                    _characters.AddBefore(currentCharacter, character);
                    return;
                }

                if (currentCharacter == _characters.Last)
                {
                    _characters.AddLast(character);
                    return;
                }

                currentCharacter = currentCharacter.Next;
            }
        }

        private void CompareByStats(LinkedListNode<ICharacter> currentCharacter, ICharacter newCharacter)
        {
            if (currentCharacter.Value.CharacterStats.Level == newCharacter.CharacterStats.Level)
            {
                if (currentCharacter.Value.CharacterStats.TotalStats == newCharacter.CharacterStats.TotalStats)
                {
                    if (_randomService.DoFiftyFifty()) _characters.AddBefore(currentCharacter, newCharacter);
                    return;
                }
                
                if (currentCharacter.Value.CharacterStats.TotalStats > newCharacter.CharacterStats.TotalStats)
                {
                    _characters.AddBefore(currentCharacter, newCharacter);
                    return;
                }
            }

            if (currentCharacter.Value.CharacterStats.Level > newCharacter.CharacterStats.Level)
            {
                _characters.AddBefore(currentCharacter, newCharacter);
                return;
            }
            
            if (currentCharacter == _characters.Last)
            {
                _characters.AddLast(newCharacter);
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