using CodeBase.Common.FSM.States;

namespace CodeBase.Infrastructure.StateMachines.App.FSM
{
    public interface IAppStateMachine
    {
        void Enter<TState>() where TState : IState;
        void Add<TState>(TState state) where TState : IExitableState;
    }
}