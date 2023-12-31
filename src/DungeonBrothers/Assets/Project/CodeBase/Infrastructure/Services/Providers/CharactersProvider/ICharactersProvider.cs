﻿using System;
using System.Collections.Generic;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.UI.TurnQueue;

namespace Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider
{
    public interface ICharactersProvider
    {
        
        IObservable<ICharacter> Spawned { get; }
        IObservable<ICharacter> Died { get; }
        
        IReadOnlyDictionary<ICharacter, CharacterTurnQueueIcon> Characters { get; }

        void Add(ICharacter character, CharacterTurnQueueIcon characterTurnQueueIcon);
        List<ICharacter> GetAllCharacterFromID(CharacterID id);
    }
}