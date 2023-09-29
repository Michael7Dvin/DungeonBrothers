using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService
    {
        private readonly PlayerInput _playerInput = new();
        
        public event Action<Vector2> PositionTouched;
        public event Action ContactTouched;

        private void OnTouched()
        {
            PositionTouched?.Invoke(_playerInput.Interactions.PrimaryFingerPosition.ReadValue<Vector2>());
            ContactTouched?.Invoke();
        }

        public void Initialization() => 
            _playerInput.Interactions.PrimaryFingerTouch.started += _ => OnTouched();

        public void DisableInput() => _playerInput.Disable();

        public void EnableInput() => _playerInput.Enable();
    }
}