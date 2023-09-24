using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.Characters
{
    public interface ICharacterFactory
    {
        public UniTask WarmUp(List<CharacterConfig> characterConfigs);

        public UniTask<Character> Create(CharacterConfig config);
    }
}