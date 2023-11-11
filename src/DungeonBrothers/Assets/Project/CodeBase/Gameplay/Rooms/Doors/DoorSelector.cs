using System;
using Project.CodeBase.Gameplay.Services.Raycast;
using Project.CodeBase.Infrastructure.Services.InputService;
using Project.CodeBase.Infrastructure.Services.Providers.CameraProvider;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Rooms.Doors
{
    public class DoorSelector : IDoorSelector
    {
        private readonly IInputService _inputService;
        private readonly IRaycastService _raycastService;
        private readonly ICameraProvider _cameraProvider;

        private readonly ReactiveCommand<Door> _isOpened = new();
        public IObservable<Door> IsOpened => _isOpened;
        
        private readonly CompositeDisposable _disposable = new();

        public DoorSelector(IInputService inputService,
            IRaycastService raycastService,
            ICameraProvider cameraProvider)
        {
            _inputService = inputService;
            _raycastService = raycastService;
            _cameraProvider = cameraProvider;
        }

        public void Initialize()
        {
            _inputService.PositionTouched
                .Skip(1)
                .Subscribe(GetDoor)
                .AddTo(_disposable);
        }

        private void GetDoor(Vector2 position)
        {
            Ray ray = _cameraProvider.Camera.ScreenPointToRay(new Vector3(position.x, position.y, 0));

            _raycastService.TryRaycast(ray.origin, ray.direction, out Door door);

            if (door != null) 
                door.Enter();
        }

        public void ClearUp() => 
            _disposable.Clear();
    }
}