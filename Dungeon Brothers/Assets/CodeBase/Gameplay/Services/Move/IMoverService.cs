using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Move
{
    public interface IMoverService
    {
        public void Move(Tile tile, Character character);


        public void CalculatePaths(Character character);
    }
}