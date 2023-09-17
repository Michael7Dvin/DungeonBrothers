namespace CodeBase.Common.FSM.States
{
    public interface IState : IExitableState
    { 
        void Enter();
    }
}