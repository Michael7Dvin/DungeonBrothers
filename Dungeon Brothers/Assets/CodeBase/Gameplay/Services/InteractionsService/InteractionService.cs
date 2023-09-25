using CodeBase.Common.Observables;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.Raycast;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.Providers.CameraProvider;
using UnityEngine;

namespace CodeBase.Gameplay.Services.InteractionsService
{
    public class InteractionService : IInteractionService
    {
        private readonly IInputService _inputService;
        private readonly IRaycastService _raycastService;
        private readonly ICameraProvider _cameraProvider;
        private readonly IMoverService _moverService;

        private readonly Observable<Tile> _currentTile = new();

        public IReadOnlyObservable<Tile> CurrentTile => _currentTile;

        public InteractionService(IInputService inputService,
            IRaycastService raycastService,
            ICameraProvider cameraProvider,
            IMoverService moverService)
        {
            _inputService = inputService;
            _raycastService = raycastService;
            _cameraProvider = cameraProvider;
            _moverService = moverService;
        }

        private void GetTileOnTouch(Vector2 touchPosition)
        {
            Ray ray = _cameraProvider.Camera.ScreenPointToRay(new Vector3(touchPosition.x, touchPosition.y, 0));

            if (_raycastService.TryRaycast(ray.origin, ray.direction, out Tile tile))
            {
                _moverService.Move(tile);
                _currentTile.Value = tile;
            }
        }
        
        public void Enable()
        {
            _inputService.PositionTouched += GetTileOnTouch;
        }

        public void Disable()
        {
            _inputService.PositionTouched -= GetTileOnTouch;
        } 
    }
}