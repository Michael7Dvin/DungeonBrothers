using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public class CharactersProvider : ICharactersProvider
    {
        private readonly Dictionary<ICharacter, CharacterInTurnQueueIcon> _characters = new();
        public IReadOnlyDictionary<ICharacter, CharacterInTurnQueueIcon> Characters => _characters;
        
        public event Action CharactersAmountChanged;
        public event Action<ICharacter, CharacterInTurnQueueIcon> Spawned;
        public event Action<ICharacter> Died;
        
        public void Add(ICharacter character,
            CharacterInTurnQueueIcon characterInTurnQueueIcon)
        {
            _characters.Add(character, characterInTurnQueueIcon);
            character.CharacterLogic.Died += OnUnitDied;
            Spawned?.Invoke(character, characterInTurnQueueIcon);
            CharactersAmountChanged?.Invoke();
            
            void OnUnitDied()
            {
                character.CharacterLogic.Died -= OnUnitDied;
                Remove(character);
            }
        }

        private void Remove(ICharacter character)
        {
            
            _characters.Remove(character);
            Died?.Invoke(character);
            CharactersAmountChanged?.Invoke();
        }
    }
}