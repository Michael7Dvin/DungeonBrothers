using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public interface ICharactersProvider
    {
        
        IObservable<(ICharacter, CharacterInTurnQueueIcon)> Spawned { get; }
        IObservable<ICharacter> Died { get; }
        
        IReadOnlyDictionary<ICharacter, CharacterInTurnQueueIcon> Characters { get; }

        void Add(ICharacter character, CharacterInTurnQueueIcon characterInTurnQueueIcon);
    }
}