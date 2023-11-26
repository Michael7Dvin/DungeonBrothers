using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.Factories.Characters;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Spawner.Character
{
    public class CharactersSpawner : ICharactersSpawner
    {
        private readonly IMapService _mapService;
        private readonly ICharacterFactory _characterFactory;

        public CharactersSpawner(IMapService mapService, ICharacterFactory characterFactory)
        {
            _mapService = mapService;
            _characterFactory = characterFactory;
        }

        public async UniTask Spawn(Dictionary<Vector2Int, CharacterConfig> charactersSpawnData)
        {
            foreach (var characterSpawnData in charactersSpawnData)
            {
                if (_mapService.TryGetTile(characterSpawnData.Key, out Tile tile))
                {
                    Characters.Character character = await _characterFactory.Create(characterSpawnData.Value);
                    Transform transform = tile.transform;

                    Vector3 position = transform.position;
                    
                    character.transform.position = new Vector3(position.x, position.y, 0);
                    character.Logic.Movement.Teleport(tile);
                }
            }
        }
    }
}