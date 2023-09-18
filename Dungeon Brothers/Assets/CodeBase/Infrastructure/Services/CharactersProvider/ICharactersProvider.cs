using System;
using CodeBase.Gameplay.Characters;

namespace CodeBase.Infrastructure.Services.UnitsProvider
{
    public interface ICharactersProvider
    {
        event Action CharactersAmountChanged; 
        event Action<ICharacter> Spawned;
        event Action<ICharacter> Died;

        void Add(ICharacter character);
    }
}