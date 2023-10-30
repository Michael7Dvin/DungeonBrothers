using Project.CodeBase.Infrastructure.StateMachines.Common.States;

namespace Project.CodeBase.Infrastructure.StateMachines.App.FSM
{
    public interface IAppStateMachine
    {
        void Enter<TState>() where TState : IState;
        void Add<TState>(TState state) where TState : IExitableState;
    }
}