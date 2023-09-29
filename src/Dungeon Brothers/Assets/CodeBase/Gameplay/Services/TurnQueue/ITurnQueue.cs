using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;
using UniRx;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<Character> Characters { get; }

        public IReadOnlyReactiveProperty<Character> ActiveCharacter { get; }
        
        public IObservable<(Character, CharacterInTurnQueueIcon)> AddedToQueue { get; }
        public IObservable<Unit> Reseted { get; }
        public IObservable<Character> NewTurnStarted { get; }
      
        
        void Initialize();
        void CleanUp();

        void SetNextTurn();
        void SetFirstTurn();
    }
}