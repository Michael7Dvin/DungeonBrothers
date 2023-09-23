using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<Character> Characters { get; }
        Character ActiveCharacter { get; }

        event Action NewTurnStarted; 
        event Action FirstTurnStarted; 
        event Action<Character, CharacterInTurnQueueIcon> AddedToQueue;
        event Action Reseted;
        
        void Initialize();
        void CleanUp();

        void SetNextTurn();
        void SetFirstTurn();
    }
}