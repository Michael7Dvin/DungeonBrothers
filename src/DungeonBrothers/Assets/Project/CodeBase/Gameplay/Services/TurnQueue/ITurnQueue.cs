using System;
using Project.CodeBase.Gameplay.Characters;
using UniRx;

namespace Project.CodeBase.Gameplay.Services.TurnQueue
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