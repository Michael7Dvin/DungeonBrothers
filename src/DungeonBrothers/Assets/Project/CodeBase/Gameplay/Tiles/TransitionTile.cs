using System;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Tiles
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TransitionTile : Tile
    {
        private ReactiveCommand _characterEnter;

        public IObservable<Unit> CharacterEnter => _characterEnter; 

        private void OnTriggerEnter(Collider other) => 
            _characterEnter.Execute();
    }
}