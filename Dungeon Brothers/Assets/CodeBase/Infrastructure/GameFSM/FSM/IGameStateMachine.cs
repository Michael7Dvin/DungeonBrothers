using CodeBase.Common.FSM.States;

namespace CodeBase.Infrastructure.GameFSM.FSM
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : IState;
        void Enter<TState, TArgument>(TArgument argument) where TState : IStateWithArgument<TArgument>;
        void Add<TState>(TState state) where TState : IExitableState;
    }
}