using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.UI.TurnQueue;
using UniRx;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IReadOnlyReactiveCollection<ICharacter> Characters { get; }

        public IReadOnlyReactiveProperty<ICharacter> ActiveCharacter { get; }

        public IObservable<Unit> Reseted { get; }
        public IObservable<Unit> NewTurnStarted { get; }
        
        void Initialize();
        void CleanUp();

        void SetNextTurn();
        void SetFirstTurn();
    }
}