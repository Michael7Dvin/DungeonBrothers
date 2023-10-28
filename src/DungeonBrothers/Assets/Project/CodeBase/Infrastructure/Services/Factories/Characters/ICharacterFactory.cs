using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Characters;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.Factories.Characters
{
    public interface ICharacterFactory
    {
        public UniTask WarmUp(List<CharacterConfig> characterConfigs);

        public UniTask<Character> Create(CharacterConfig config);
    }
}