using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Spawner.CharacterSpawner
{
    public interface ICharactersSpawner
    {
        public UniTask Spawn(Dictionary<Vector2Int, CharacterConfig> charactersSpawnData);
    }
}