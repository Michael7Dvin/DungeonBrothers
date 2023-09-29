using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        public event Action<Vector2> PositionTouched;
        public event Action ContactTouched;
        
        public void Initialization();

        public void EnableInput();

        public void DisableInput();
    }
}