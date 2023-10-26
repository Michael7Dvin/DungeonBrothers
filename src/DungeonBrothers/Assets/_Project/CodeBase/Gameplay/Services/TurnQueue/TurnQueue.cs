using System;
using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Random;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using UniRx;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public class TurnQueue : ITurnQueue
    {
        private readonly IRandomService _randomService;
        private readonly ICharactersProvider _charactersProvider;

        private readonly CompositeDisposable _disposable = new();
        
        private readonly ReactiveCollection<ICharacter> _characters = new();
        private readonly ReactiveProperty<ICharacter> _activeCharacter = new();
        private readonly ReactiveCommand _newTurnStarted = new();

        private readonly ReactiveCommand _reseted = new();

        public TurnQueue(IRandomService randomService, 
            ICharactersProvider charactersProvider)
        {
            _randomService = randomService;
            _charactersProvider = charactersProvider;
        }
        
        public IReadOnlyReactiveCollection<ICharacter> Characters => _characters;
        public IObservable<Unit> Reseted => _reseted;
        public IReadOnlyReactiveProperty<ICharacter> ActiveCharacter => _activeCharacter;
        public IObservable<Unit> NewTurnStarted => _newTurnStarted;

        public void Initialize()
        {
            _charactersProvider.Spawned
                .Subscribe(Add)
                .AddTo(_disposable);
            
            _charactersProvider.Died
                .Subscribe(Remove)
                .AddTo(_disposable);
        }
        
        public void CleanUp()
        {
            _disposable.Clear();

            _reseted.Execute();
            
            _characters.Clear();
            _activeCharacter.Value = null;
        }

        public void SetNextTurn()
        {
            if (_activeCharacter.Value == _characters.First())
                _activeCharacter.Value = _characters.Last();
            else
                _activeCharacter.Value = _characters[_characters.IndexOf(_activeCharacter.Value) - 1];
           
            _newTurnStarted.Execute();
        }

        public void SetFirstTurn()
        {
            _activeCharacter.Value = _characters.Last();
        }

        private void Add(ICharacter character)
        {
            if (_characters.Count == 0)
            {
                _characters.Add(character);
                return;
            }

            int newCharacterInitiative = character.Stats.Initiative;

            ICharacter currentCharacter = _characters.First();

            while (currentCharacter != null)
            {
                int currentCharacterInitiative = currentCharacter.Stats.Initiative;

                if (newCharacterInitiative == currentCharacterInitiative)
                {
                    if (_randomService.DoFiftyFifty())
                    {
                        _characters.Insert(_characters.IndexOf(currentCharacter), character);
                        return;
                    }
                }

                if (newCharacterInitiative < currentCharacterInitiative)
                {
                    _characters.Insert(_characters.IndexOf(currentCharacter), character);
                    return;
                }

                if (currentCharacter == _characters.Last())
                {
                    _characters.Add(character);
                    return;
                }
                
                currentCharacter = _characters[_characters.IndexOf(currentCharacter) + 1];
            }
        }

        private void Remove(ICharacter character) => 
            _characters.Remove(character);
    }
}