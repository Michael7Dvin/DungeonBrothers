using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation
{
    public class TileVisualizationActiveCharacter : ITileVisualizationActiveCharacter
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMapService _mapService;
        private readonly ICustomLogger _customLogger;
        private readonly IMoverService _moverService;

        private Tile _lastTile;
        private Character _lastActiveCharacter;

        public TileVisualizationActiveCharacter(ITurnQueue turnQueue, 
            IMapService mapService,
            ICustomLogger customLogger,
            IMoverService moverService)
        {
            _turnQueue = turnQueue;
            _mapService = mapService;
            _customLogger = customLogger;
            _moverService = moverService;
        }

        public void Initialize()
        {
            _turnQueue.ActiveCharacter.Changed += HighlightOutlineTile;
            _moverService.IsMoved += HighlightOutlineTile;
        }

        public void CleanUp()
        {
            _moverService.IsMoved -= HighlightOutlineTile;
            _turnQueue.ActiveCharacter.Changed -= HighlightOutlineTile;
        }

        private void HighlightOutlineTile(Character character)
        {
            DisableHighlightOutline();

            Vector2Int position = character.Coordinate.Value;

            if (_mapService.TryGetTile(position, out Tile tile))
            {
                _lastTile = tile;
                
                switch (character.CharacterTeam)
                {
                    case CharacterTeam.Enemy:
                    {
                        ActiveHighlightOutline(tile);
                        tile.TileView.ChangeHighlightColor(Color.red);
                        tile.TileView.ChangeOutLineColor(Color.red);
                        break;
                    }
                    case CharacterTeam.Ally:
                    {
                        ActiveHighlightOutline(tile);
                        tile.TileView.ChangeOutLineColor(Color.green);
                        tile.TileView.ChangeHighlightColor(Color.green);
                        break;
                    }
                    default:
                        _customLogger.LogError(new Exception($"{nameof(character.CharacterTeam)}, does not exist"));
                        break;
                }
            }
        }

        private void ActiveHighlightOutline(Tile tile)
        {
            tile.TileView.SwitchHighlight(true);
            tile.TileView.SwitchOutLine(true);
        }
        
        private void DisableHighlightOutline()
        {
            if (_lastTile != null)
            {
                _lastTile.TileView.SwitchHighlight(false);
                _lastTile.TileView.SwitchOutLine(false);
                _lastTile.TileView.ChangeOutLineColor(Color.white);
            }
        }
    }
}