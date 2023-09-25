using System;
using System.Collections.Generic;
using CodeBase.Common.Observables;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<Character> Characters { get; }

        public IReadOnlyObservable<Character> ActiveCharacter { get; }
        
        event Action<Character> NewTurnStarted; 
       
        event Action<Character, CharacterInTurnQueueIcon> AddedToQueue;
        event Action Reseted;
        
        void Initialize();
        void CleanUp();

        void SetNextTurn();
        void SetFirstTurn();
    }
}