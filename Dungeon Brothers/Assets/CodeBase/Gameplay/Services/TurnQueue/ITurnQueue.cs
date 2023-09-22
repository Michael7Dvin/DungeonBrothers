using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<ICharacter> Characters { get; }
        ICharacter ActiveCharacter { get; }

        event Action NewTurnStarted; 
        event Action<ICharacter, CharacterInTurnQueueIcon> AddedToQueue;
        event Action Reseted;
        
        void Initialize();
        void CleanUp();

        void SetNextTurn();
        void SetFirstTurn();
    }
}