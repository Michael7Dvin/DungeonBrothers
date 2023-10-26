using System;
using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.UI.TurnQueue;

namespace _Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public interface ICharactersProvider
    {
        
        IObservable<ICharacter> Spawned { get; }
        IObservable<ICharacter> Died { get; }
        
        IReadOnlyDictionary<ICharacter, CharacterInTurnQueueIcon> Characters { get; }

        void Add(ICharacter character, CharacterInTurnQueueIcon characterInTurnQueueIcon);
    }
}