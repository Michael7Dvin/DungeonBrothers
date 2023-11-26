using System;
using Project.CodeBase.Gameplay.Services.Raycast;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Infrastructure.Services.InputService;
using Project.CodeBase.Infrastructure.Services.Providers.CameraProvider;
using Project.CodeBase.Infrastructure.StateMachines.Gameplay.FSM;
using Project.CodeBase.Infrastructure.StateMachines.Gameplay.States;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Rooms.Doors
{
    public class DoorSelector : IDoorSelector
    {
        private readonly IInputService _inputService;
        private readonly IRaycastService _raycastService;
        private readonly ICameraProvider _cameraProvider;
        private readonly IGameplayStateMachine _gameplayStateMachine;

        private Door _previousDoor;
        
        private readonly ReactiveCommand<Door> _isOpened = new();
        public IObservable<Door> IsOpened => _isOpened;
        
        private readonly CompositeDisposable _disposable = new();

        public DoorSelector(IInputService inputService,
            IRaycastService raycastService,
            ICameraProvider cameraProvider,
            IGameplayStateMachine gameplayStateMachine)
        {
            _inputService = inputService;
            _raycastService = raycastService;
            _cameraProvider = cameraProvider;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void Initialize()
        {
            _inputService.PositionTouched
                .Skip(1)
                .Subscribe(position =>
                {
                    if (_gameplayStateMachine.ActiveState is IdleState) 
                        GetDoor(position);
                })
                .AddTo(_disposable);
        }

        private void GetDoor(Vector2 position)
        {
            var door = GetDoorOnClick(position);

            if (door == null)
                return;

            if (IsChooseSameDoor(door))
            {
                door.Enter();
                _previousDoor = null;
            }
        }

        private bool IsChooseSameDoor(Door door)
        {
            if (_previousDoor == null)
            {
                _previousDoor = door;
                return false;
            }

            if (_previousDoor != door)
            {
                _previousDoor = door;
                return false;
            }

            return true;
        }

        private Door GetDoorOnClick(Vector2 position)
        {
            Ray ray = _cameraProvider.Camera.ScreenPointToRay(new Vector3(position.x, position.y, 0));
            _raycastService.TryRaycast(ray.origin, ray.direction, out Door door);
            return door;
        }

        public void ClearUp() => 
            _disposable.Clear();
    }
}