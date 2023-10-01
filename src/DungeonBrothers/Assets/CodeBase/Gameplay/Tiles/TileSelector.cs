using CodeBase.Gameplay.Services.Raycast;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.Providers.CameraProvider;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class TileSelector : ITileSelector
    {
        private readonly IRaycastService _raycastService;
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;
        private readonly CompositeDisposable _disposable = new();

        public TileSelector(IRaycastService raycastService,
            IInputService inputService,
            ICameraProvider cameraProvider)
        {
            _raycastService = raycastService;
            _inputService = inputService;
            _cameraProvider = cameraProvider;
        }

        private readonly ReactiveProperty<Tile> _currentTile = new();
        private readonly ReactiveProperty<Tile> _previousTile = new();
        public IReadOnlyReactiveProperty<Tile> PreviousTile => _previousTile;
        public IReadOnlyReactiveProperty<Tile> CurrentTile => _currentTile;

        public void Initialize()
        {
            _inputService.PositionTouched
                .Skip(1)
                .Subscribe(SelectTile)
                .AddTo(_disposable);
        }

        public void Disable() =>
            _disposable.Clear();

        private void SelectTile(Vector2 tilePosition)
        {
            Ray ray = _cameraProvider.Camera.ScreenPointToRay(new Vector3(tilePosition.x, tilePosition.y, 0));
            
            if (_raycastService.TryRaycast(ray.origin, ray.direction, out Tile tile))
            {
                if (_currentTile.Value != null)
                    _previousTile.SetValueAndForceNotify(_currentTile.Value);

                _currentTile.SetValueAndForceNotify(tile);
            }
        }
    }
}