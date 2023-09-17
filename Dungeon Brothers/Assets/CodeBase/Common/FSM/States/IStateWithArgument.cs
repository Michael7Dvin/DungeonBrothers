namespace CodeBase.Common.FSM.States
{
    public interface IStateWithArgument<in TArgs> : IExitableState
    {
        void Enter(TArgs args);
    }
}