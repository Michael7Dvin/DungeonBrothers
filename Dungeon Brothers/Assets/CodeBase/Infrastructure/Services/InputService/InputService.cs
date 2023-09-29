using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService
    {
        private readonly PlayerInput _playerInput = new();

        private readonly ReactiveProperty<Vector2> _positionTouched = new();
        public IReadOnlyReactiveProperty<Vector2> PositionTouched => _positionTouched;

        private void OnClicked() => 
            _positionTouched.SetValueAndForceNotify(_playerInput.Interactions.PrimaryFingerPosition.ReadValue<Vector2>());

        public void Initialization() =>
            _playerInput.Interactions.PrimaryFingerTouch.performed += _ => OnClicked();

        public void DisableInput() => _playerInput.Disable();

        public void EnableInput() => _playerInput.Enable();
    }
}