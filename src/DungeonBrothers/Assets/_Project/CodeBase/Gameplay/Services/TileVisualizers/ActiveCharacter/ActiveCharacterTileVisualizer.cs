﻿using System;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.Gameplay.Services.Map;
using _Project.CodeBase.Gameplay.Services.Move;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.Logger;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Services.TileVisualizers.ActiveCharacter
{
    public class ActiveCharacterTileVisualizer : IActiveCharacterTileVisualizer
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMapService _mapService;
        private readonly ICustomLogger _customLogger;
        private readonly IMoverService _moverService;
        private readonly TileColorsConfig _tileColorsConfig;

        private readonly CompositeDisposable _disposable = new();

        private Tile _lastTile;
        private ICharacter _lastActiveCharacter;

        public ActiveCharacterTileVisualizer(ITurnQueue turnQueue, 
            IMapService mapService,
            ICustomLogger customLogger,
            IMoverService moverService,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _mapService = mapService;
            _customLogger = customLogger;
            _moverService = moverService;
            _tileColorsConfig = staticDataProvider.TileColorsConfig;
        }

        public void Initialize()
        {
            _turnQueue.ActiveCharacter
                .Skip(1)
                .Subscribe(HighlightOutlineTile)
                .AddTo(_disposable);

            _moverService.IsMoved
                .Subscribe(HighlightOutlineTile)
                .AddTo(_disposable);
        }

        public void CleanUp() => 
            _disposable.Clear();

        private void HighlightOutlineTile(ICharacter character)
        {
            if (_lastTile != null)
                DisableHighlightOutline();
            
            if (_mapService.TryGetTile(character.Coordinate, out Tile tile))
            {
                _lastTile = tile;
                
                switch (character.Team)
                {
                    case CharacterTeam.Enemy:
                    {
                        ActiveHighlightOutline(_lastTile);
                        _lastTile.View.ChangeHighlightColor(_tileColorsConfig.EnemyTile);
                        _lastTile.View.ChangeOutLineColor(_tileColorsConfig.EnemyTile);
                        break;
                    }
                    case CharacterTeam.Ally:
                    {
                        ActiveHighlightOutline(_lastTile);
                        _lastTile.View.ChangeOutLineColor(_tileColorsConfig.AllyTile);
                        _lastTile.View.ChangeHighlightColor(_tileColorsConfig.AllyTile);
                        break;
                    }
                    default:
                        _customLogger.LogError(new Exception($"{nameof(character.Team)}, does not exist"));
                        break;
                
                    
                }
            }
        }

        private void ActiveHighlightOutline(Tile tile)
        {
            tile.View.SwitchHighlight(true);
            tile.View.SwitchOutLine(true);
        }
        
        private void DisableHighlightOutline()
        {
            _lastTile.View.SwitchHighlight(false);
            _lastTile.View.SwitchOutLine(false);
            _lastTile.View.ChangeOutLineColor(Color.white);
        }
    }
}