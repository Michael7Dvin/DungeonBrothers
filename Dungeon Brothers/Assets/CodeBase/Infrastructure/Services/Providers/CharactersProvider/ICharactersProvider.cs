using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;
using UniRx;

namespace CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public interface ICharactersProvider
    {
        
        IObservable<(Character, CharacterInTurnQueueIcon)> Spawned { get; }
        IObservable<Character> Died { get; }
        
        public IReadOnlyDictionary<Character, CharacterInTurnQueueIcon> Characters { get; }

        void Add(Character character,
            CharacterInTurnQueueIcon characterInTurnQueueIcon);
    }
}