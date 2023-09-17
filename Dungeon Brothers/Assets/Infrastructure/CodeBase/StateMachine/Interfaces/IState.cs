namespace Infrastructure.CodeBase.StateMachine.Interfaces
{
    public interface IState : IExitableState
    { 
        void Enter();
    }
}