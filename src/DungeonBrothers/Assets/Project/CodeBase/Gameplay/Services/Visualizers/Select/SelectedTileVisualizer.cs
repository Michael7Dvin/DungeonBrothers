using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UniRx;

namespace Project.CodeBase.Gameplay.Services.Visualizers.Select
{
    public class SelectedTileVisualizer : ISelectedTileVisualizer
    {
        private readonly ITileSelector _tileSelector;
        private readonly ITurnQueue _turnQueue;
      
        
        private readonly CompositeDisposable _disposable = new();

        private readonly TileColorsConfig _tileColorsConfig;
        
        public SelectedTileVisualizer(ITileSelector tileSelector,
            ITurnQueue turnQueue,
            IStaticDataProvider staticDataProvider)
        {
            _tileSelector = tileSelector;
            _turnQueue = turnQueue;
            _tileColorsConfig = staticDataProvider.TileColors;
        }

        public void Initialize()
        {
            _tileSelector.CurrentTile
                .Skip(1)
                .Where(tile => tile != _tileSelector.PreviousTile.Value)
                .Where(tile => tile != null)
                .Subscribe(VisualizeSelectedTile)
                .AddTo(_disposable);
            
            _tileSelector.PreviousTile
                .Skip(1)
                .Where(tile => tile != null)
                .Subscribe(ResetTileView)
                .AddTo(_disposable);

            _turnQueue.NewTurnStarted
                .Where(_ => _tileSelector.CurrentTile.Value != null)
                .Subscribe(_ => ResetTileView(_tileSelector.CurrentTile.Value))
                .AddTo(_disposable);
        }

        public void Disable() => 
            _disposable.Clear();
        
        private void VisualizeSelectedTile(Tile tile)
        {
            tile.View.SwitchOutLine(true);
            tile.View.ChangeOutLineColor(_tileColorsConfig.SelectedTile);
        }

        private void ResetTileView(Tile tile) => 
            tile.View.SwitchOutLine(false);
    }
}