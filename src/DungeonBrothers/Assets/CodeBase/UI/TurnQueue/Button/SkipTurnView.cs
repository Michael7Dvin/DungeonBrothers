using System;
using CodeBase.UI.Button;
using UnityEngine;

namespace CodeBase.UI.TurnQueue.Button
{
    public class SkipTurnView : MonoBehaviour
    {
        [SerializeField] private SelectableButton _selectableButton;
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
            _selectableButton.Cliked += OnClick;
          
        }
        private void OnDisable()
        {
            _selectableButton.Cliked -= OnClick;
           
        }
            
    }
}