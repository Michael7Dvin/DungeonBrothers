﻿using UniRx;
using UnityEngine;

namespace Project.CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        public IReadOnlyReactiveProperty<Vector2> PositionTouched { get; }

        public void Initialization();

        public void EnableInput();

        public void DisableInput();
    }
}