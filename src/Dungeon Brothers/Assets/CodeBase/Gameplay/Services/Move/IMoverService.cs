using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Move
{
    public interface IMoverService
    {
        public void Move(Tile tile);

        public void Enable();
        public void Disable();

        public PathFindingResults PathFindingResults { get; }

        public IObservable<ICharacter> IsMoved { get; }
    }
}