using System.Collections.Generic;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Services.Move;
using Project.CodeBase.Gameplay.Services.PathFinder;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Services.Visualizers.Walkable
{
    public class WalkableTilesVisualizer : IWalkableTilesVisualizer
    {
        private readonly IPathFinder _pathFinder;
        private readonly IMapService _mapService;
        private readonly IMoverService _moverService;
        private readonly ITurnQueue _turnQueue;
        private readonly TileColorsConfig _tileColorsConfig;

        private readonly CompositeDisposable _disposable = new();
        private readonly List<Tile> _visualizedTiles = new();

        public WalkableTilesVisualizer(IPathFinder pathFinder,
            IMapService mapService,
            ITurnQueue turnQueue,
            IStaticDataProvider staticDataProvider)
        {
            _pathFinder = pathFinder;
            _mapService = mapService;
            _turnQueue = turnQueue;
            _tileColorsConfig = staticDataProvider.TileColors;
        }

        public void Initialize()
        {
            _turnQueue.NewTurnStarted
                .Where(_ => _turnQueue.ActiveCharacter.Value.Team == CharacterTeam.Enemy)
                .Subscribe(_ => ResetLastTilesView())
                .AddTo(_disposable);
            
            _pathFinder.PathFindingResults
                .Skip(1)
                .Where(_ => _turnQueue.ActiveCharacter.Value.Team == CharacterTeam.Ally)
                .Subscribe(VisualizeWalkableTiles)
                .AddTo(_disposable);
        }

        public void Disable()
        {
            _disposable.Clear();
        }

        private void VisualizeWalkableTiles(PathFindingResults pathFindingResults)
        {
            ResetLastTilesView();
            
            foreach (Vector2Int coordinate in pathFindingResults.WalkableCoordinates)
            {
                if (_mapService.TryGetTile(coordinate, out Tile tile))
                {
                    _visualizedTiles.Add(tile);
                    
                    tile.View.SwitchHighlight(true);
                    tile.View.ChangeHighlightColor(_tileColorsConfig.WalkableColorTile);
                }
            }
        }

        private void ResetLastTilesView()
        {
            foreach (Tile tile in _visualizedTiles) 
                tile.View.ResetTileView();

            _visualizedTiles.Clear();
        }
    }
}