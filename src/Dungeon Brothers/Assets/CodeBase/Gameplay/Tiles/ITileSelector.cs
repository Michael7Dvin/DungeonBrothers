using CodeBase.Common.Observables;

namespace CodeBase.Gameplay.Tiles
{
    public interface ITileSelector
    {
        public IReadOnlyObservable<Tile> CurrentTile { get; }
        public IReadOnlyObservable<Tile> PreviousTile { get; }

        public void Initialize();

        public void Disable();
    }
}