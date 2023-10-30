using Project.CodeBase.Infrastructure.StateMachines.Common.States;

namespace Project.CodeBase.Infrastructure.StateMachines.Gameplay.FSM
{
    public interface IGameplayStateMachine
    {
        void Enter<TState>() where TState : IState;
        void Enter<TState, TArgument>(TArgument argument) where TState : IStateWithArgument<TArgument>;
        void Add<TState>(TState state) where TState : IExitableState;
    }
}