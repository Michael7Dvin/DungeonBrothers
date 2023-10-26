using System;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.PathFinder;
using _Project.CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Gameplay.Services.Move
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