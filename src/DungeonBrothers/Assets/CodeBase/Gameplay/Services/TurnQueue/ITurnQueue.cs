using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;
using UniRx;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<ICharacter> Characters { get; }

        public IReadOnlyReactiveProperty<ICharacter> ActiveCharacter { get; }
        
        public IObservable<(ICharacter, CharacterInTurnQueueIcon)> AddedToQueue { get; }
        public IObservable<Unit> Reseted { get; }
        public IObservable<ICharacter> NewTurnStarted { get; }
      
        
        void Initialize();
        void CleanUp();

        void SetNextTurn();
        void SetFirstTurn();
    }
}