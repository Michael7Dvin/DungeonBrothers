namespace Infrastructure.CodeBase.StateMachine.Interfaces
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        
        void Enter<TState, TArgs>(TArgs args) where TState : IStateWithArgument<TArgs>;
        
        void AddState<TState>(TState state) where TState : IExitableState;
    }
}