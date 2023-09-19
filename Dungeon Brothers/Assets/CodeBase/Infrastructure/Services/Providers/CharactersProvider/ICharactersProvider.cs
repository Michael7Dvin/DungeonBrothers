using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Configs.Character;

namespace CodeBase.Infrastructure.Services.UnitsProvider
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