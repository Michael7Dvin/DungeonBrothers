using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Move
{
    public interface IMoverService
    {
        public void Move(Tile tile);

        public void Enable();
        public void Disable();
        
        public event Action<Character> IsMoved; 
    }
}