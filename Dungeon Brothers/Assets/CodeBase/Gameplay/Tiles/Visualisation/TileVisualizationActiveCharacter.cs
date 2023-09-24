using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.MapService;
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

        private Tile _lastTile;

        public TileVisualizationActiveCharacter(ITurnQueue turnQueue, 
            IMapService mapService,
            ICustomLogger customLogger)
        {
            _turnQueue = turnQueue;
            _mapService = mapService;
            _customLogger = customLogger;
        }

        public void Initialize()
        {
            _turnQueue.NewTurnStarted += HighlightOutlineTile;
            _turnQueue.FirstTurnStarted += HighlightOutlineTile;
        }

        public void CleanUp()
        {
            _turnQueue.NewTurnStarted -= HighlightOutlineTile;
            _turnQueue.FirstTurnStarted -= HighlightOutlineTile;
        }

        private void HighlightOutlineTile()
        {
            DisableHighlightOutline();
            
            Character character = GetActiveCharacter();
            Transform transform = character.transform;

            Vector2Int position = 
                new Vector2Int((int)transform.position.x, (int)transform.position.y);

            if (_mapService.TryGetTile(position, out Tile tile))
            {
                _lastTile = tile;
                
                switch (character.CharacterTeam)
                {
                    case CharacterTeam.Enemy:
                    {
                        tile.TileView.SwitchOutLine(true);
                        tile.TileView.ChangeOutLineColor(Color.red);
                        break;
                    }
                    case CharacterTeam.Ally:
                    {
                        tile.TileView.SwitchOutLine(true);
                        tile.TileView.ChangeOutLineColor(Color.green);
                        break;
                    }
                    default:
                        _customLogger.LogError(new Exception($"{nameof(character.CharacterTeam)}, does not exist"));
                        break;
                }
            }
        }

        private void DisableHighlightOutline()
        {
            if (_lastTile != null)
            {
                _lastTile.TileView.SwitchOutLine(false);
                _lastTile.TileView.ChangeOutLineColor(Color.white);
            }
        }
        private Character GetActiveCharacter() => _turnQueue.ActiveCharacter;
    }
}