using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Characters;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Spawner.CharacterSpawner
{
    public interface ICharactersSpawner
    {
        public UniTask Spawn(Dictionary<Vector2Int, CharacterConfig> spawnCharacter);
    }
}