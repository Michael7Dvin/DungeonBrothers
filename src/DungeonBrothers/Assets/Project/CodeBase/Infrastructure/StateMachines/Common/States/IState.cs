namespace Project.CodeBase.Infrastructure.StateMachines.Common.States
{
    public interface IState : IExitableState
    { 
        void Enter();
    }
}