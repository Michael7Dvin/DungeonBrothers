using System;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Services.PathFinder;
using Project.CodeBase.Gameplay.Tiles;

namespace Project.CodeBase.Gameplay.Services.Move
{
    public interface IMoverService
    {
        public UniTask Move(Tile tile);

        public void Enable();
        public void Disable();

        public PathFindingResults PathFindingResults { get; }

        public IObservable<ICharacter> IsMoved { get; }
    }
}