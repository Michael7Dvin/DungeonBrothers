using CodeBase.Common.FSM.States;

namespace CodeBase.Common.FSM
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : IState;
        
        void Enter<TState, TArgs>(TArgs args) where TState : IStateWithArgument<TArgs>;
        
        void AddState<TState>(TState state) where TState : IExitableState;
    }
}