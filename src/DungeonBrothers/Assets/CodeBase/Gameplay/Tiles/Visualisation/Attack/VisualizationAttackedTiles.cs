using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.Logic;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;

namespace CodeBase.Gameplay.Tiles.Visualisation.Attack
{
    public class VisualizationAttackedTiles : IVisualizationAttackedTiles
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMoverService _moverService;
        private readonly IMapService _mapService;
        private readonly IPathFinder _pathFinder;
        private readonly ICustomLogger _customLogger;
        private readonly TileColorConfig _tileColorConfig;

        private readonly CompositeDisposable _disposable = new();
        
        private readonly int _meleeRange;
        private readonly int _rangedRange;

        private readonly List<Tile> _lastTiles = new();

        public VisualizationAttackedTiles(ITurnQueue turnQueue,
            IMoverService moverService,
            IMapService mapService,
            IPathFinder pathFinder,
            ICustomLogger customLogger,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _moverService = moverService;
            _mapService = mapService;
            _pathFinder = pathFinder;
            _customLogger = customLogger;

            _tileColorConfig = staticDataProvider.TileColorConfig;
            _meleeRange = staticDataProvider.GameBalanceConfig.AttackRangeConfig.MeleeRange;
            _rangedRange = staticDataProvider.GameBalanceConfig.AttackRangeConfig.RangedRange;
        }

        public void Initialize()
        {
            _turnQueue.ActiveCharacter
                .Skip(1)
                .Subscribe(VisualizeAttackedCharacters)
                .AddTo(_disposable);

            _moverService.IsMoved
                .Subscribe(VisualizeAttackedCharacters)
                .AddTo(_disposable);
        }

        public void Disable() =>
            _disposable.Clear();
        
        private void VisualizeAttackedCharacters(ICharacter character)
        {
            ResetLastTiles();

            PathFindingResults pathFindingResults = new PathFindingResults();

            switch (character.CharacterDamage.CharacterAttackType)
            {
                case CharacterAttackType.Melee:
                    pathFindingResults = GetPathFindingResults(character, _meleeRange, false);
                    break;
                case CharacterAttackType.Ranged:
                    pathFindingResults = GetPathFindingResults(character, _rangedRange, true);
                    break;
                default:
                    _customLogger.LogError(new Exception($"{character.CharacterDamage.CharacterAttackType} doesnt exist"));
                    break;
            }
            
            foreach (var coordinate in pathFindingResults.NotWalkableCoordinates)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile))
                {
                    if (coordinate == character.Coordinate)
                        continue;

                    VisualizeTile(character, tile);
                }
            }
        }

        private PathFindingResults GetPathFindingResults(ICharacter character, int range, bool isAttackThroughEnemy) => 
            _pathFinder.CalculatePaths(character.Coordinate, range, isAttackThroughEnemy);

        private void VisualizeTile(ICharacter character, Tile tile)
        {
            if (tile.Logic.Character.CharacterTeam != character.CharacterTeam)
            {
                _lastTiles.Add(tile);
                
                tile.View.SwitchHighlight(true);
                tile.View.ChangeHighlightColor(_tileColorConfig.AttackedTile);
            }
        }

        private void ResetLastTiles()
        {
            foreach (var tile in _lastTiles)
            {
                if (tile.Logic.Coordinates == _turnQueue.ActiveCharacter.Value.Coordinate)
                    continue;
                
                tile.View.ResetTileView();
            }

            _lastTiles.Clear();
        }
    }
}