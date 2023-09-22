using System;
using CodeBase.UI.Button;
using UnityEngine;

namespace CodeBase.UI.TurnQueue.Button
{
    public class SkipTurnView : MonoBehaviour
    {
        [SerializeField] private BaseButton _button;
        private SkipTurnViewModel _skipTurnViewModel;
        
        private void OnClick()
            => _skipTurnViewModel.SkipTurn();
        
        private void OnEnable() =>
            _button.Cliked += OnClick;

        private void OnDisable() => 
            _button.Cliked -= OnClick;
    }
}