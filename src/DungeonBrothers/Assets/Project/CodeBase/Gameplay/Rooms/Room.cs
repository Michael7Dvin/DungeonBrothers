using System;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.Factories.TileFactory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Rooms
{
    public class Room : SerializedMonoBehaviour
    {
        [SerializeField] private Tile[] _doors;

        private ITileFactory _tileFactory;

        private void Start()
        {
           
        }
    }
}