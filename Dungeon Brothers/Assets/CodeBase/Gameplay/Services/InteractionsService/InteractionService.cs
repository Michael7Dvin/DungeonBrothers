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
        private readonly IMoverService _moverService;
        private readonly ITileSelector _tileSelector;
        
        public InteractionService(IMoverService moverService,
            ITileSelector tileSelector)
        {
            _moverService = moverService;
            _tileSelector = tileSelector;
        }

        private void GetTileOnTouch(Tile tile)
        {
            if (_tileSelector.CurrentTile.Value == _tileSelector.PreviousTile.Value)
                _moverService.Move(tile);
        }
        
        public void Initialize()
        {
            _tileSelector.CurrentTile.Changed += GetTileOnTouch;
        }

        public void Disable()
        {
            _tileSelector.CurrentTile.Changed -= GetTileOnTouch;
        } 
    }
}