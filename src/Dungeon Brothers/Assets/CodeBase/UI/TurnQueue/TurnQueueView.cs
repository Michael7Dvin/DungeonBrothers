using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace CodeBase.UI.TurnQueue
{
    public class TurnQueueView : MonoBehaviour
    {
        private TurnQueueViewModel _turnQueueViewModel;
        private readonly CompositeDisposable _disposable = new();
        
        private const int MaxVisualizedIcons = 5;
        
        public void Construct(TurnQueueViewModel turnQueueViewModel)
        {
            _turnQueueViewModel = turnQueueViewModel;
            
           Enable();
        }
        
        private void Enable()
        {
            _turnQueueViewModel.ListChanged
                .Subscribe(ReorganizeChildPosition)
                .AddTo(_disposable); 
            
            _turnQueueViewModel.EnableIcons
                .Subscribe(EnableIcons)
                .AddTo(_disposable); 
            
            _turnQueueViewModel.DisableIcons
                .Subscribe(DisableIcons)
                .AddTo(_disposable);

            _turnQueueViewModel.OnEnable();
        }

        private void Disable()
        {
            _disposable.Clear();
            
            _turnQueueViewModel.OnDisable();
        }

        private void DisableIcons(IReadOnlyList<CharacterInTurnQueueIcon> characterInTurnQueueIcons)
        {
            foreach (var icon in characterInTurnQueueIcons) 
                icon.gameObject.SetActive(false);
        }

        private void ReorganizeChildPosition(IReadOnlyList<CharacterInTurnQueueIcon> characterInTurnQueueIcons)
        {
            for (int i = 0; i < characterInTurnQueueIcons.Count; i++)
                characterInTurnQueueIcons[i].transform.SetSiblingIndex(i);
        }
        
        
        private void EnableIcons(IReadOnlyList<CharacterInTurnQueueIcon> characterInTurnQueueIcons)
        {
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
            {
                characterInTurnQueueIcons[i].gameObject.SetActive(true);
            }
        }
    }
}