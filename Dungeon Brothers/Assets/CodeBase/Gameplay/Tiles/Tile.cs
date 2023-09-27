using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Services.Logger;
using UnityEngine;
using VContainer;

namespace CodeBase.Gameplay.Tiles
{
    public class Tile : MonoBehaviour
    {
        public TileLogic TileLogic { get; private set; }
        public TileView TileView { get; private set; }

        public void Construct(TileLogic tileLogic,
            TileView tileView)
        {
            TileView = tileView;
            TileLogic = tileLogic;
        }

        public void Release()
        {
            TileLogic.Release();
            TileView.ResetTileView();
        }
    }
}
