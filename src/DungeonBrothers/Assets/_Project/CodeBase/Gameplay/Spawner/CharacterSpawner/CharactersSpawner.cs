﻿using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Services.Map;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.Factories.Characters;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Spawner.CharacterSpawner
{
    public class CharactersSpawner : ICharactersSpawner
    {
        private readonly IMapService _mapService;
        private readonly ICharacterFactory _characterFactory;

        public CharactersSpawner(IMapService mapService,
            ICharacterFactory characterFactory)
        {
            _mapService = mapService;
            _characterFactory = characterFactory;
        }

        public async UniTask Spawn(Dictionary<Vector2Int, CharacterConfig> spawnCharacter)
        {
            foreach (var character in spawnCharacter)
            {
                if (_mapService.TryGetTile(character.Key, out Tile tile))
                {
                    Character prefab = await _characterFactory.Create(character.Value);
                    Transform transform = tile.transform;

                    Vector3 position = transform.position;
                    
                    prefab.transform.position = new Vector3(position.x, position.y, 0);
                    prefab.UpdateCoordinate(tile.Logic.Coordinates);
                    
                    tile.Logic.Occupy(prefab);
                }
            }
        }
    }
}