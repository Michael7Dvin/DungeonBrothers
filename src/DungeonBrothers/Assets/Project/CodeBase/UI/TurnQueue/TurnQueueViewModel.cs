using System;
using System.Linq;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using UniRx;
using VContainer;

namespace Project.CodeBase.UI.TurnQueue
{
    public class TurnQueueViewModel
    {
        private ITurnQueue _turnQueue;
        private readonly CompositeDisposable _disposable = new();

        private readonly ReactiveCollection<CharacterInTurnQueueIcon> _charactersIconsQueue = new();
        
        private readonly ReactiveCommand _disableIcons = new();
        private readonly ReactiveCommand _enableIcons = new();

        [Inject]
        public void Inject(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }
        public IReadOnlyReactiveCollection<CharacterInTurnQueueIcon> CharacterIconsQueue => _charactersIconsQueue;
        public IObservable<Unit> DisableIcons => _disableIcons;
        public IObservable<Unit> EnableIcons => _enableIcons;
        
        public void OnEnable()
        {
            _turnQueue.Characters
                .ObserveAdd()
                .Subscribe(_ => ReorganizeIcons())
                .AddTo(_disposable);

            _turnQueue.Reseted
                .Subscribe(_ => ClearIcons())
                .AddTo(_disposable);

            _turnQueue.Characters
                .ObserveRemove()
                .Subscribe(RemoveIconFromList)
                .AddTo(_disposable);

            _turnQueue.NewTurnStarted
                .Subscribe(_ => ShiftIcons())
                .AddTo(_disposable);
        }
        
        public void OnDisable() => 
            _disposable.Clear();

        private void ClearIcons()
        {
            foreach (var icon in _charactersIconsQueue)
                icon.Destroy();
            
            _charactersIconsQueue.Clear();
        }

        private void ReorganizeIcons()
        {
            _disableIcons.Execute();
            
            _charactersIconsQueue.Clear();

            for (int i = _turnQueue.Characters.Count - 1 ; i >= 0 ; i--)
                _charactersIconsQueue.Add(_turnQueue.Characters[i].View.Icon);

            _enableIcons.Execute();
        }

        private void RemoveIconFromList(CollectionRemoveEvent<ICharacter> character)
        {
            CharacterInTurnQueueIcon icon = character.Value.View.Icon;
            
            _charactersIconsQueue.Remove(character.Value.View.Icon);
            icon.Destroy();
        }
        
        private void ShiftIcons()
        {
            _disableIcons.Execute();
            
            for (int i = 0; i < _charactersIconsQueue.Count - 1; i++)
            {
                if (i == _charactersIconsQueue.Count() - 1)
                    return;
                
                if (i == 0) 
                    _charactersIconsQueue.Move(i, _charactersIconsQueue.Count() - 1);
                
                _charactersIconsQueue.Move(i, +i);
            }

            _enableIcons.Execute();
        }
    }
}