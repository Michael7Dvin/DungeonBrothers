using Project.CodeBase.UI.Controls.Button;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.UI.TurnQueue.Button
{
    public class SkipTurnView : MonoBehaviour
    {
        [SerializeField] private SelectableButton _selectableButton;
        
        private readonly CompositeDisposable _disposable = new();
        private SkipTurnViewModel _skipTurnViewModel;

        public void Construct(SkipTurnViewModel skipTurnViewModel)
        {
            _skipTurnViewModel = skipTurnViewModel;
        }
        
        private void OnClick()
            => _skipTurnViewModel.SkipTurn();

        private void Disable() =>
            gameObject.SetActive(false);

        private void Enable() =>
            gameObject.SetActive(true);
        
        private void OnEnable()
        {
            _selectableButton.Clicked
                .Subscribe(_ => OnClick())
                .AddTo(_disposable);

        }
        private void OnDisable() => 
            _disposable.Clear();
    }
}