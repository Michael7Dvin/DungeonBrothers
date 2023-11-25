using System;
using System.Collections.Generic;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Infrastructure.Services.Logger;
using Project.CodeBase.UI.TurnQueue;
using UniRx;

namespace Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public class CharactersProvider : ICharactersProvider
    {
        private readonly Dictionary<ICharacter, CharacterTurnQueueIcon> _characters = new();
        private readonly ReactiveCommand<ICharacter> _spawned = new();
        private readonly ReactiveCommand<ICharacter> _died = new();

        private readonly ICustomLogger _customLogger;

        public CharactersProvider(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        public IObservable<ICharacter> Spawned => _spawned;
        public IObservable<ICharacter> Died => _died;
        public IReadOnlyDictionary<ICharacter, CharacterTurnQueueIcon> Characters => _characters;
        
        public void Add(ICharacter character,
            CharacterTurnQueueIcon characterTurnQueueIcon)
        {
            _characters.Add(character, characterTurnQueueIcon);

            CompositeDisposable disposable = new CompositeDisposable();
            
            character.Logic.Death.Died
                .Subscribe(_ => OnUnitDied())
                .AddTo(disposable);
            
            _spawned.Execute(character);

            void OnUnitDied()
            {
                disposable.Clear();
                Remove(character);
            }
        }

        public List<ICharacter> GetAllCharacterFromID(CharacterID id)
        {
            List<ICharacter> targetCharacter = new();

            foreach (var character in _characters.Keys)
            {
                if (character.ID == id)
                    targetCharacter.Add(character);
            }
            
            if (targetCharacter.Count == 0)
                _customLogger.LogError(new Exception($"Characters with id {id}, not found"));
            
            return targetCharacter;
        }

        private void Remove(ICharacter character)
        {
            _characters.Remove(character);
            _died?.Execute(character);
        }
    }
}