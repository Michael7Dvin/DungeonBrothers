using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.Gameplay.PathFinder;
using _Project.CodeBase.Gameplay.Services.Attack;
using _Project.CodeBase.Gameplay.Services.Map;
using _Project.CodeBase.Gameplay.Services.Move;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;

namespace _Project.CodeBase.Gameplay.Services.TileVisualizers.Attack
{
    public class AttackableTilesVisualizer : IAttackableTilesVisualizer
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMoverService _moverService;
        private readonly IAttackService _attackService;
        private readonly IMapService _mapService;
        private readonly TileColorsConfig _tileColorsConfig;

        private readonly CompositeDisposable _disposable = new();
        private readonly List<Tile> _lastTiles = new();

        public AttackableTilesVisualizer(ITurnQueue turnQueue,
            IMoverService moverService,
            IAttackService attackService,
            IMapService mapService,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _moverService = moverService;
            _attackService = attackService;
            _mapService = mapService;

            _tileColorsConfig = staticDataProvider.TileColorsConfig;
        }

        public void Initialize()
        {
            _turnQueue.ActiveCharacter
                .Skip(1)
                .Where(character => character.Team == CharacterTeam.Ally)
                .Subscribe(VisualizeAttackedCharacters)
                .AddTo(_disposable);

            _moverService.IsMoved
                .Where(character => character.Team == CharacterTeam.Ally)
                .Subscribe(VisualizeAttackedCharacters)
                .AddTo(_disposable);
        }

        public void Disable() =>
            _disposable.Clear();
        
        private void VisualizeAttackedCharacters(ICharacter character)
        {
            ResetLastTiles();

            PathFindingResults pathFindingResults = _attackService.GetPathFindingResults(character);
            
            foreach (var coordinate in pathFindingResults.NotWalkableCoordinates)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile)) 
                    VisualizeTile(character, tile);
            }
        }

        private void VisualizeTile(ICharacter character, Tile tile)
        {
            if (tile.Logic.Character.Team != character.Team)
            {
                _lastTiles.Add(tile);
                
                tile.View.SwitchHighlight(true);
                tile.View.ChangeHighlightColor(_tileColorsConfig.AttackedTile);
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