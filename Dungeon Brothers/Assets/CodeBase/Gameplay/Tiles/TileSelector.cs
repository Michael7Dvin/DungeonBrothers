using CodeBase.Common.Observables;
using CodeBase.Gameplay.Services.Raycast;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.Providers.CameraProvider;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class TileSelector : ITileSelector
    {
        private readonly IRaycastService _raycastService;
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;

        public TileSelector(IRaycastService raycastService,
            IInputService inputService,
            ICameraProvider cameraProvider)
        {
            _raycastService = raycastService;
            _inputService = inputService;
            _cameraProvider = cameraProvider;
        }

        private readonly Observable<Tile> _currentTile = new();
        private readonly Observable<Tile> _previousTile = new();

        public IReadOnlyObservable<Tile> CurrentTile => _currentTile;
        public IReadOnlyObservable<Tile> PreviousTile => _previousTile;

        public void Initialize()
        {
            _inputService.PositionTouched += SelectTile;
        }

        public void Disable()
        {
            _inputService.PositionTouched -= SelectTile;
        }

        private void SelectTile(Vector2 tilePosition)
        {
            Ray ray = _cameraProvider.Camera.ScreenPointToRay(tilePosition);
            
            if (_raycastService.TryRaycast(ray.origin, ray.direction, out Tile tile))
            {
                if (_currentTile.Value != null)
                    _previousTile.Value = _currentTile.Value;
                
                _currentTile.Value = tile;
            }
        }
    }
}