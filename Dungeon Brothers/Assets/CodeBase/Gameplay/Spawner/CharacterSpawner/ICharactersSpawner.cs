using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Spawner.CharacterSpawner
{
    public interface ICharactersSpawner
    {
        public UniTask Spawn(Dictionary<Vector2Int, CharacterConfig> spawnCharacter);
    }
}