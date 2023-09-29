using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;
using UniRx;

namespace CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public class CharactersProvider : ICharactersProvider
    {
        private readonly Dictionary<Character, CharacterInTurnQueueIcon> _characters = new();
        private readonly ReactiveCommand<(Character, CharacterInTurnQueueIcon)> _spawned = new();
        private readonly ReactiveCommand<Character> _died = new();
        
        public IObservable<(Character, CharacterInTurnQueueIcon)> Spawned => _spawned;
        public IObservable<Character> Died => _died;
        public IReadOnlyDictionary<Character, CharacterInTurnQueueIcon> Characters => _characters;
        
        public void Add(Character character,
            CharacterInTurnQueueIcon characterInTurnQueueIcon)
        {
            _characters.Add(character, characterInTurnQueueIcon);
            character.CharacterLogic.Died += OnUnitDied;
            _spawned.Execute((character, characterInTurnQueueIcon));

            void OnUnitDied()
            {
                character.CharacterLogic.Died -= OnUnitDied;
                Remove(character);
            }
        }

        private void Remove(Character character)
        {
            _characters.Remove(character);
            _died?.Execute(character);
        }
    }
}