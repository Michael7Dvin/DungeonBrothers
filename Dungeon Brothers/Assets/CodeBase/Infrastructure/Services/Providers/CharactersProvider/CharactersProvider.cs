using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Configs.Character;

namespace CodeBase.Infrastructure.Services.UnitsProvider
{
    public class CharactersProvider : ICharactersProvider
    {
        private readonly List<ICharacter> _characters = new();
        private readonly List<CharacterConfig> _characterConfigs = new();

        public List<CharacterConfig> CharacterConfigs => _characterConfigs;
        public event Action CharactersAmountChanged;
        public event Action<ICharacter> Spawned;
        public event Action<ICharacter> Died;
        
        public void Add(ICharacter character,
            CharacterConfig characterConfig)
        {
            _characters.Add(character);
            _characterConfigs.Add(characterConfig);
            character.CharacterLogic.Died += OnUnitDied;
            Spawned?.Invoke(character);
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