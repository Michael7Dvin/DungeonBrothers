using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Visualisation
{
    public class TileVisualizationActiveCharacter : ITileVisualizationActiveCharacter
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMapService _mapService;
        private readonly ICustomLogger _customLogger;
        private readonly IMoverService _moverService;
        private readonly TileColorConfig _tileColorConfig;

        private Tile _lastTile;
        private Character _lastActiveCharacter;

        public TileVisualizationActiveCharacter(ITurnQueue turnQueue, 
            IMapService mapService,
            ICustomLogger customLogger,
            IMoverService moverService,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _mapService = mapService;
            _customLogger = customLogger;
            _moverService = moverService;
            _tileColorConfig = staticDataProvider.TileColorConfig;
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

            Vector2Int position = character.Coordinate;

            if (_mapService.TryGetTile(position, out Tile tile))
            {
                _lastTile = tile;
                
                switch (character.CharacterTeam)
                {
                    case CharacterTeam.Enemy:
                    {
                        ActiveHighlightOutline(tile);
                        tile.TileView.ChangeHighlightColor(_tileColorConfig.EnemyTile);
                        tile.TileView.ChangeOutLineColor(_tileColorConfig.EnemyTile);
                        break;
                    }
                    case CharacterTeam.Ally:
                    {
                        ActiveHighlightOutline(tile);
                        tile.TileView.ChangeOutLineColor(_tileColorConfig.AllyTile);
                        tile.TileView.ChangeHighlightColor(_tileColorConfig.AllyTile);
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