using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;
using UniRx;

namespace CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public class CharactersProvider : ICharactersProvider
    {
        private readonly Dictionary<ICharacter, CharacterInTurnQueueIcon> _characters = new();
        private readonly ReactiveCommand<(ICharacter, CharacterInTurnQueueIcon)> _spawned = new();
        private readonly ReactiveCommand<ICharacter> _died = new();
        
        public IObservable<(ICharacter, CharacterInTurnQueueIcon)> Spawned => _spawned;
        public IObservable<ICharacter> Died => _died;
        public IReadOnlyDictionary<ICharacter, CharacterInTurnQueueIcon> Characters => _characters;
        
        public void Add(ICharacter character,
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

        private void Remove(ICharacter character)
        {
            _characters.Remove(character);
            _died?.Execute(character);
        }
    }
}