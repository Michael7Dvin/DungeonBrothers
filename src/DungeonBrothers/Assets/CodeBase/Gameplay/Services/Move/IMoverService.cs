using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.PathFinder;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Move
{
    public interface IMoverService
    {
        public UniTask Move(Vector2Int coordinates);

        public void Enable();
        public void Disable();

        public PathFindingResults PathFindingResults { get; }

        public IObservable<ICharacter> IsMoved { get; }
    }
}