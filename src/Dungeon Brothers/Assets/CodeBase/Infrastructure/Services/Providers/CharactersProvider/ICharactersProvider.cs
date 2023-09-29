using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public interface ICharactersProvider
    {
        event Action CharactersAmountChanged; 
        event Action<Character, CharacterInTurnQueueIcon> Spawned;
        event Action<Character> Died;
        
        public IReadOnlyDictionary<Character, CharacterInTurnQueueIcon> Characters { get; }

        void Add(Character character,
            CharacterInTurnQueueIcon characterInTurnQueueIcon);
    }
}