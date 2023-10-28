using System;
using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.UI.TurnQueue;
using UniRx;

namespace _Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public class CharactersProvider : ICharactersProvider
    {
        private readonly Dictionary<ICharacter, CharacterInTurnQueueIcon> _characters = new();
        private readonly ReactiveCommand<ICharacter> _spawned = new();
        private readonly ReactiveCommand<ICharacter> _died = new();

        public IObservable<ICharacter> Spawned => _spawned;
        public IObservable<ICharacter> Died => _died;
        public IReadOnlyDictionary<ICharacter, CharacterInTurnQueueIcon> Characters => _characters;
        
        public void Add(ICharacter character,
            CharacterInTurnQueueIcon characterInTurnQueueIcon)
        {
            _characters.Add(character, characterInTurnQueueIcon);

            CompositeDisposable disposable = new CompositeDisposable();
            
            character.Logic.Health.Died
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