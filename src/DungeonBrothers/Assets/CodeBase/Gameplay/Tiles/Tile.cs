using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Services.Logger;
using UnityEngine;
using VContainer;

namespace CodeBase.Gameplay.Tiles
{
    public class Tile : MonoBehaviour
    {
        public TileLogic Logic { get; private set; }
        public TileView View { get; private set; }

        public void Construct(TileLogic tileLogic,
            TileView tileView)
        {
            View = tileView;
            Logic = tileLogic;
        }
    }
}
