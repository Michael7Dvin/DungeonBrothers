using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public interface ICharactersProvider
    {
        event Action CharactersAmountChanged; 
        event Action<ICharacter, CharacterInTurnQueueIcon> Spawned;
        event Action<ICharacter> Died;
        
        public IReadOnlyDictionary<ICharacter, CharacterInTurnQueueIcon> Characters { get; }

        void Add(ICharacter character,
            CharacterInTurnQueueIcon characterInTurnQueueIcon);
    }
}