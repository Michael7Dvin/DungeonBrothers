using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Configs.Character;

namespace CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public interface ICharactersProvider
    {
        event Action CharactersAmountChanged; 
        event Action<ICharacter> Spawned;
        event Action<ICharacter> Died;

        void Add(ICharacter character,
            CharacterConfig characterConfig);
    }
}