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

        private readonly LinkedList<ICharacter> _units = new();
        private LinkedListNode<ICharacter> _activeUnitNode;

        public TurnQueue(IRandomService randomService, 
            ICharactersProvider charactersProvider,
            ICustomLogger logger)
        {
            _randomService = randomService;
            _charactersProvider = charactersProvider;
            _logger = logger;
        }

        public IEnumerable<ICharacter> Units => _units;
        public ICharacter ActiveUnit => _activeUnitNode.Value;
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
            
            _units.Clear();
            _activeUnitNode = null;
        }

        public void SetNextTurn()
        {
            if (_activeUnitNode == _units.First)
                _activeUnitNode = _units.Last;
            else
                _activeUnitNode = _activeUnitNode.Previous;
            
            NewTurnStarted?.Invoke(ActiveUnit);
        }
        
        public void SetFirstTurn()
        {
            _activeUnitNode = _units.Last;
            NewTurnStarted?.Invoke(ActiveUnit);
        }

        private void Add(ICharacter character)
        {
            if (_units.Count == 0)
            {
                _units.AddFirst(character);
                return;
            }

            int newCharacterInitiative = character.CharacterStats.Initiative;
            
            LinkedListNode<ICharacter> currentCharacter = _units.First;

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
                    _units.AddBefore(currentCharacter, character);
                    return;
                }

                if (currentCharacter == _units.Last)
                {
                    _units.AddLast(character);
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
                    if (_randomService.DoFiftyFifty()) _units.AddBefore(currentCharacter, newCharacter);
                    return;
                }
                
                if (currentCharacter.Value.CharacterStats.TotalStats > newCharacter.CharacterStats.TotalStats)
                {
                    _units.AddBefore(currentCharacter, newCharacter);
                    return;
                }
            }

            if (currentCharacter.Value.CharacterStats.Level > newCharacter.CharacterStats.Level)
            {
                _units.AddBefore(currentCharacter, newCharacter);
                return;
            }
            
            if (currentCharacter == _units.Last)
            {
                _units.AddLast(newCharacter);
            }
        }

        private void Remove(ICharacter character)
        {
            if (character == _activeUnitNode.Value)
                _logger.LogError(new Exception($"Unable to remove {nameof(ActiveUnit)}. Feature not implemented"));

            _units.Remove(character);
        }
    }
}