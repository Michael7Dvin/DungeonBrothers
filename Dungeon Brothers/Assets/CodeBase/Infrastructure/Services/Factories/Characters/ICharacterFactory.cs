using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Configs.Character;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Factories.Characters
{
    public interface ICharacterFactory
    {
        public UniTask WarmUp(List<CharacterConfig> characterConfigs);

        public UniTask<ICharacter> Create(CharacterConfig config);
    }
}