using System;
using System.Collections.Generic;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.UI.TurnQueue;
using UniRx;

namespace Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public class CharactersProvider : ICharactersProvider
    {
        private readonly Dictionary<ICharacter, CharacterTurnQueueIcon> _characters = new();
        private readonly ReactiveCommand<ICharacter> _spawned = new();
        private readonly ReactiveCommand<ICharacter> _died = new();

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

        private void Remove(ICharacter character)
        {
            _characters.Remove(character);
            _died?.Execute(character);
        }
    }
}