using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;

namespace Project.CodeBase.Infrastructure.Services.Factories.Characters
{
    public interface ICharacterFactory
    {
        public UniTask WarmUp(List<CharacterConfig> characterConfigs);

        public UniTask<Character> Create(CharacterConfig config);
    }
}