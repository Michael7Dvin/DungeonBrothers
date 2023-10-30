using UniRx;
using UnityEngine;

namespace Project.CodeBase.UI.TurnQueue
{
    public class TurnQueueView : MonoBehaviour
    {
        private const int MaxVisualizedIcons = 5;
        
        private TurnQueueViewModel _turnQueueViewModel;
        private readonly CompositeDisposable _disposable = new();
        
        public void Construct(TurnQueueViewModel turnQueueViewModel)
        {
            _turnQueueViewModel = turnQueueViewModel;
            
           Enable();
        }
        
        private void Enable()
        {
            _turnQueueViewModel.CharacterIconsQueue
                .ObserveAdd()
                .Subscribe(_ => ReorganizeChildPosition())
                .AddTo(_disposable);  
            
            _turnQueueViewModel.CharacterIconsQueue
                .ObserveMove()
                .Subscribe(ShiftChildPosition)
                .AddTo(_disposable);

            _turnQueueViewModel.EnableIcons
                .Subscribe(_ => EnableIcons())
                .AddTo(_disposable); 
            
            _turnQueueViewModel.DisableIcons
                .Subscribe()
                .AddTo(_disposable);

            _turnQueueViewModel.OnEnable();
        }

        private void Disable()
        {
            _disposable.Clear();
            
            _turnQueueViewModel.OnDisable();
        }

        private void DisableIcons()
        {
            foreach (var icon in _turnQueueViewModel.CharacterIconsQueue) 
                icon.gameObject.SetActive(false);
        }

        private void ShiftChildPosition(CollectionMoveEvent<CharacterInTurnQueueIcon> charactersIcons)
        {
            int oldIndex = charactersIcons.OldIndex;
            int newIndex = charactersIcons.NewIndex;

            _turnQueueViewModel.CharacterIconsQueue[charactersIcons.OldIndex].transform.SetSiblingIndex(newIndex);
            _turnQueueViewModel.CharacterIconsQueue[charactersIcons.OldIndex].transform.SetSiblingIndex(oldIndex);
        }

        private void ReorganizeChildPosition()
        {
            for (int i = 0; i < _turnQueueViewModel.CharacterIconsQueue.Count - 1; i++)
                _turnQueueViewModel.CharacterIconsQueue[i].transform.SetSiblingIndex(i);
        }


        private void EnableIcons()
        {
            IReadOnlyReactiveCollection<CharacterInTurnQueueIcon> characterInTurnQueueIcons =
                _turnQueueViewModel.CharacterIconsQueue;

            if (characterInTurnQueueIcons.Count <= MaxVisualizedIcons)
            {
                for (int i = characterInTurnQueueIcons.Count - 1; i >= 0; i--)
                {
                    characterInTurnQueueIcons[i].gameObject.SetActive(true);
                }
                return;
            }

            int maxVisualizeIcons = characterInTurnQueueIcons.Count - 1 - MaxVisualizedIcons;
            
            for (int i = characterInTurnQueueIcons.Count - 1; i > maxVisualizeIcons; i--)
                characterInTurnQueueIcons[i].gameObject.SetActive(true);
        }
    }
}