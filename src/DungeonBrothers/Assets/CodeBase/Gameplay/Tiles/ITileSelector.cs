using UniRx;

namespace CodeBase.Gameplay.Tiles
{
    public interface ITileSelector
    {
        public IReadOnlyReactiveProperty<Tile> CurrentTile { get; }
        public IReadOnlyReactiveProperty<Tile> PreviousTile { get; }

        public void Initialize();

        public void Disable();
    }
}